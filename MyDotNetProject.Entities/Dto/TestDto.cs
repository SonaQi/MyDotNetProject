using MyDotNetProject.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Entities.Dto
{
    public class TestDto
    {
        public int Id { get; set; }

        [Column("名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
