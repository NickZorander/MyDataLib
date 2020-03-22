using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    [Serializable]
    public class Researcher : Person, IDeepCopy, IComparable<Researcher>
    {
        public string research_theme { get; set; }
        public int number_of_publications { get; set; }

        public Researcher(string rt = "Default" , int nop = 0, string name = "Ivanov", string surname = "Researcher", DateTime bd = new DateTime())
            : base(name, surname, bd)
        {
            research_theme = rt;
            number_of_publications = nop;
        }

        public override string ToString()
        {
            return base.ToString() + ' ' + research_theme + ' ' + number_of_publications.ToString();
        }

        override public object DeepCopy()
        {
            return new Researcher(research_theme, number_of_publications, base.name_and_surname[0], base.name_and_surname[1], base.birth_date);
        }

        public int CompareTo (Researcher R)
        {
            return number_of_publications.CompareTo(R.number_of_publications);
        }

    }
}
