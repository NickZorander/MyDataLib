using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public class Team:IDeepCopy
    {
        public string group_name { get; set; }
        public List<Person> member_list { get; set; }

        public IEnumerable<Person> Subset(Predicate<Person> Filter)
        {
            foreach (Person p in member_list)
                if (Filter(p))
                    yield return p;
        }

        public Team()
        {
            group_name = "Default";
            member_list = new List<Person>();
        }

        public Team (string str="Default")
        {
            group_name = str;
            member_list= new List<Person>();
        }

        public Team (string name, List<Person> ml)
        {
            member_list= new List<Person>();
            foreach (Person p in ml)
            {
                member_list.Add(p);
            }
            group_name = name;
        }

        public void AddPerson (params Person[] persons)
        {
            for (int i=0; i<=persons.Length-1; i++)
            {
                if (!member_list.Contains(persons[i]))
                    member_list.Add(persons[i]);
            }
        }

        public void AddDefaults ()
        {
            member_list.Add(new Person("Ivan", "Petrov", new DateTime(1952, 1, 1)));
            member_list.Add(new Person("Ivan", "Sidorov", new DateTime(1962, 1, 1)));

            member_list.Add(new Person("Anna", "Petrova", new DateTime(1977, 1, 1)));
            member_list.Add(new Person("Svetlana", "Sidorov", new DateTime(1988, 1, 1)));

            member_list.Add(new Programmer(1, "Databases", "Sergey", "Sidorovich", new DateTime(1977, 1, 1)));
            member_list.Add(new Programmer(2, "Databases", "Ivan", "Sidorovich", new DateTime(1952, 1, 1)));

            member_list.Add(new Researcher("AI", 29, "Nikolay", "Shevchenko", new DateTime(1942, 1, 1)));
            member_list.Add(new Researcher("AI", 5, "Alexey", "Zaytsev", new DateTime(1972, 1, 1)));
        }

        public void AddDefaults2()
        {
            member_list.Add(new Person("Ivan", "Petrov", new DateTime(1952, 1, 1)));
            member_list.Add(new Person("Ivan", "Sidorov", new DateTime(1962, 1, 1)));

            member_list.Add(new Programmer(5, "Databases", "Nikolay", "Medvedev", new DateTime(1952, 1, 1)));
            member_list.Add(new Programmer(10, "Databases", "Maxim", "Alexeev", new DateTime(1952, 1, 1)));

            member_list.Add(new Researcher("AI", 44, "Alexandr", "Dyatlov", new DateTime(1942, 1, 1)));
            member_list.Add(new Researcher("AI", 19, "Vladimir", "Petrov", new DateTime(1972, 1, 1)));
        }

        public bool IsProgrammer (Person ps)
        {
            return ps is Programmer;
        }

        public override string ToString()
        {
            string str="___"+group_name+"___\n";
            foreach (Person p in member_list)
            {
                str+=p.ToString();
                str += '\n';
            }
            str += "///////////////";

            return str;
        }

       public object DeepCopy()
        {
            return new Team(group_name, member_list);
        }

    }
}
