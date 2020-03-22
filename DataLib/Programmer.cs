using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    [Serializable]
    public class Programmer : Person, IDeepCopy
    {
        public double experience { get; set; }
        public string research_theme { get; set; }

        public Programmer (double exp=0, string rt="Default", string name = "Ivanov", string surname="Ivan", DateTime bd= new DateTime())
            : base(name, surname, bd)
        {
            experience = exp;
            research_theme= rt;
        }

        public override string ToString()
        {
            return base.ToString()+' '+experience.ToString()+' '+research_theme;
        }
    }
}
