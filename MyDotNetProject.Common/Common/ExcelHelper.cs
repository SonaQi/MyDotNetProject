using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Common
{
    /// <summary>
    /// 导出帮助类
    /// </summary>
    public class ExcelHelper
    {
        private IWorkbook _workbook;

        public ExcelHelper()
        {

        }

        /// <summary>
        /// 从对象列表初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void InitFromObjectList<T>(IReadOnlyList<T> data)
        {
            _workbook = new XSSFWorkbook();
            var sheet = _workbook.CreateSheet();
            var row0 = sheet.CreateRow(0);
            var columnNames = GetColumnNameDict<T>().ToList();
            var columnIndexs = new List<(string, int)>();
            for (var i = 0; i < columnNames.Count; i++)
            {
                var column = columnNames[i];
                var cell = row0.CreateCell(i);
                cell.SetCellValue(column.Value);
                columnIndexs.Add((column.Key, i));
            }
            for (var i = 0; i < data.Count; i++)
            {
                var item = data[i];
                var row = sheet.CreateRow(i + 1);
                ObjectToRow(item, row, columnIndexs.ToDictionary(p => p.Item1, p => p.Item2));
            }
        }

        /// <summary>
        /// 添加Sheet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">内容</param>
        /// <param name="sheetName"></param>
        public void CreateSheet<T>(IReadOnlyList<T> data, string sheetName = "")
        {
            if (_workbook is null)
            {
                _workbook = new XSSFWorkbook();
            }

            var sheet = string.IsNullOrEmpty(sheetName) ? _workbook.CreateSheet() : _workbook.CreateSheet(sheetName);
            var row0 = sheet.CreateRow(0);
            var columnNames = GetColumnNameDict<T>().ToList();
            var columnIndexs = new List<(string, int)>();
            for (var i = 0; i < columnNames.Count; i++)
            {
                var column = columnNames[i];
                var cell = row0.CreateCell(i);
                cell.SetCellValue(column.Value);
                columnIndexs.Add((column.Key, i));
            }
            for (var i = 0; i < data.Count; i++)
            {
                var item = data[i];
                var row = sheet.CreateRow(i + 1);
                ObjectToRow(item, row, columnIndexs.ToDictionary(p => p.Item1, p => p.Item2));
            }
        }

        /// <summary>
        /// 从流初始化
        /// </summary>
        /// <param name="stream"></param>
        public void InitFromStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            using (stream)
                _workbook = WorkbookFactory.Create(stream);
        }

        /// <summary>
        /// 获取流(字节数组)
        /// </summary>
        /// <returns></returns>
        public byte[] GetByteArray()
        {
            if (_workbook == null) throw new Exception("未初始化表格");
            using var memoryStream = new MemoryStream();
            _workbook.Write(memoryStream, false);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheetIndex">以0开始</param>
        /// <param name="columnNameRowIndex">以0开始</param>
        /// <param name="dataBeginRowIndex">以0开始</param>
        /// <returns></returns>
        public IEnumerable<T> GetObjectList<T>(int sheetIndex, int columnNameRowIndex = 0, int dataBeginRowIndex = 1) where T : class
        {
            var sheet = _workbook.GetSheetAt(sheetIndex);
            if (sheet != null && sheet.PhysicalNumberOfRows >= 1)
            {
                var columnDict = GetColumnIndexDict<T>(sheet, columnNameRowIndex, GetColumnNameDict<T>());
                var rowCount = dataBeginRowIndex;
                var result = new List<T>();

                while (sheet.GetRow(rowCount) != null)
                {
                    var row = sheet.GetRow(rowCount);
                    result.Add(RowToObject<T>(row, columnDict));
                    rowCount++;
                }

                return result;
            }

            throw new Exception("无法读取sheet");
        }

        #region 内部方法

        /// <summary>
        /// 获取对象属性和列名的对应关系
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IReadOnlyDictionary<string, string> GetColumnNameDict<T>()
        {
            var result = new Dictionary<string, string>();
            foreach (var property in typeof(T).GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    var columnInfo = (ColumnAttribute)attribute;
                    if (null != columnInfo)
                    {
                        result.Add(property.Name, columnInfo.Name);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取对象属性和表头Index的对应关系
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="columnNameDict"></param>
        /// <returns></returns>
        private IReadOnlyDictionary<string, int> GetColumnIndexDict<T>(ISheet sheet, int columnNameRowIndex, IReadOnlyDictionary<string, string> columnNameDict)
        {
            var row = sheet.GetRow(columnNameRowIndex);
            var result = new Dictionary<string, int>();
            foreach (var property in typeof(T).GetProperties())
            {
                var cells = row.Where(p => GetCellStringValue(p) == columnNameDict[property.Name]);
                if (cells.Count() != 1) throw new Exception("列元信息错误");
                result.Add(property.Name, cells.First().ColumnIndex);
            }
            return result;
        }

        /// <summary>
        /// 表行转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        private T RowToObject<T>(IRow row, IReadOnlyDictionary<string, int> dict)
        {
            var result = new JObject();
            foreach (var item in dict)
            {
                var cell = row.GetCell(item.Value);
                object cellValue;
                if (cell == null)
                {
                    cellValue = "";
                }
                else if (cell.CellType == CellType.Numeric)
                {
                    if (DateUtil.IsCellDateFormatted(cell))
                        cellValue = DateTime.FromOADate(cell.NumericCellValue).ToString();
                    else
                        cellValue = cell.NumericCellValue;
                }
                else
                {
                    cellValue = cell.StringCellValue;
                }

                result.Add(item.Key, JToken.FromObject(cellValue));
            }
            return result.ToObject<T>();
        }

        /// <summary>
        /// 对象转为表行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        private void ObjectToRow<T>(T obj, IRow row, IReadOnlyDictionary<string, int> dict)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    var columnInfo = (ColumnAttribute)attribute;
                    if (null != columnInfo)
                    {
                        var cell = row.CreateCell(dict[property.Name]);
                        cell.SetCellValue((string)property.GetValue(obj));
                    }
                }
            }
        }

        /// <summary>
        /// 获取单元格文本
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private string GetCellStringValue(ICell cell)
        {
            if (cell.CellType == CellType.Numeric)
            {
                if (DateUtil.IsCellDateFormatted(cell))
                    return DateTime.FromOADate(cell.NumericCellValue).ToString();
                return cell.NumericCellValue.ToString();
            }
            else
                return cell.StringCellValue;
        }

        #endregion
    }

    /// <summary>
    /// 列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
