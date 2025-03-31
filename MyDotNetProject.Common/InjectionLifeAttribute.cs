using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDotNetProject.Common.Abstracts;

namespace MyDotNetProject.Common
{
    public class InjectionLifeAttribute : Attribute
    {
        private readonly IInjectionLifeType _life;

        public InjectionLifeAttribute(IInjectionLifeType life)
        {
            _life = life;
        }

        public IInjectionLifeType Life => _life;
    }
}
