using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public class Program
    {
        static bool HasPublication(Person ps)
        {
            return (ps is Researcher) && ( ( (Researcher)ps).number_of_pulications>0);
        }

        static void Main(string[] args)
        {
            //Team t1 = new Team("Lobby");
            //t1.AddDefaults();
            //Console.WriteLine(t1.ToString());

            //Console.WriteLine("__________________");
            //Team copy = (Team)t1.DeepCopy();
            //t1.member_list[0]= new Person("SERGAAAAAAAAAAAAAAY", "Petrov", new DateTime(1952, 1, 1));
            //Console.WriteLine(t1.ToString());
            //Console.WriteLine(copy.ToString());

            //Console.WriteLine("__________________");

            //foreach (Person p in t1.Subset(t1.IsProgrammer) )
            //{
            //    Console.WriteLine(p.ToString());
            //}

            //Console.WriteLine("__________________");
            ////Predicate<Person> p1 = Program.HasPublication;
            //foreach (Person p in t1.Subset(HasPublication))
            //{
            //    Console.WriteLine(p.ToString());
            //}

            TeamList teaml = new TeamList();
            //teaml.AddDefaults();

            Console.WriteLine("Исходный спиок команд:");
            Console.WriteLine(teaml.ToString());
            Console.WriteLine("\n \n \n");

            Console.WriteLine("1:");
            Console.WriteLine(teaml.MaxPublicationsAmongResearchers);
            Console.WriteLine("\n \n \n");

            Console.WriteLine("2:");
            Console.WriteLine(teaml.BestResearcher);
            Console.WriteLine("\n \n \n");

            Console.WriteLine("3:");
            foreach (var p in teaml.ProgrammersDescendingExperience)
                Console.WriteLine(p);
            Console.WriteLine("\n \n \n");

            Console.WriteLine("4:");
            IEnumerable<IGrouping<double, Programmer>> groups = teaml.ProgrammersGroupingExperience;
            foreach (var g in groups)
            {
                Console.WriteLine("KEY: "+ g.Key);
                foreach (var p in g)
                   Console.WriteLine(p.ToString()+"\n");                
            }                                    
            Console.WriteLine("\n \n \n");

            Console.WriteLine("5:");
            foreach (var p in teaml.MultiTeamPersons)
                Console.WriteLine(p.ToString());
            Console.WriteLine("\n \n \n");

            Console.WriteLine("Additional task");
            foreach (var p in teaml.PersonsBD)
                Console.WriteLine(p.ToString());
            Console.WriteLine("\n \n \n");
        }
    }
}
