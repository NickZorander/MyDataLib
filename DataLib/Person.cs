using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    [Serializable]
    public partial class Person : IDeepCopy
    {
        public string[] name_and_surname { get; set; }
        public System.DateTime birth_date { get; set; }


        public Person(string n = "Ivan", string sn = "Ivanov", DateTime bd = new DateTime())
        {
            name_and_surname = new string[2];
            name_and_surname[0] = n;
            name_and_surname[1] = sn;
            birth_date = bd;
        }

        public override string ToString()
        {
            return name_and_surname[0] + ' ' + name_and_surname[1] + ' ' + birth_date.ToString();
        }

        public override bool Equals(object obj)
        {

            return name_and_surname[0] == ((Person)obj).name_and_surname[0]
                    && name_and_surname[1] == ((Person)obj).name_and_surname[1]
                    && birth_date == ((Person)obj).birth_date;

        }

        public static bool operator ==(Person a, Person b)
        {
            //object.ReferenceEquals(a, null)
            if ((object)a != null && (object)b != null)
                return a.Equals(b);
            else if ((object)a == null && (object)b == null)
                return true;
            return false;
        }

        public static bool operator !=(Person a, Person b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return name_and_surname[0].GetHashCode() + name_and_surname[1].GetHashCode() + birth_date.GetHashCode();
        }

        virtual public object DeepCopy()
        {
            return new Person(name_and_surname[0], name_and_surname[1], birth_date);
        }


    }
}
