using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Entities.Enum
{
    public enum YesOrNoEnum
    {
        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        Yes = 1,
        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        No = 0
    }
}
