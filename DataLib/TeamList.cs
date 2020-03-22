using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public class TeamList
    {
        public System.Collections.Generic.List<Team> TL { get; set; }

        
        public int MaxPublicationsAmongResearchers
        {
            get
            {
                var all_pubs =
                    from t in TL
                    from p in t.member_list
                    where (p is Researcher)
                    select ((Researcher)p).number_of_pulications;

                if (all_pubs.Count() == 0)
                    return -1;
                return all_pubs.Max();
            }
        }

        public Researcher BestResearcher
        {
            get
            {
                var all_researchers =
                    from t in TL
                    from p in t.member_list
                    where (p is Researcher)
                    orderby ((Researcher)p).number_of_pulications descending
                    select (Researcher)p;

                if (all_researchers.Count() == 0)
                    return null;

                return all_researchers.First();              
            }
        }


        public IEnumerable<Programmer> ProgrammersDescendingExperience
        {
            get
            {
                return
                    from t in TL
                    from p in t.member_list
                    where (p is Programmer)
                    orderby ((Programmer)p).experience 
                    select (Programmer)p;
            }
        }

        public IEnumerable<IGrouping<double,Programmer>> ProgrammersGroupingExperience
        {
            get
            {
                return
                    from t in TL
                    from p in t.member_list
                    where (p is Programmer)                 
                    group (Programmer)p by ((Programmer)p).experience;
            }
        }

        public IEnumerable<Person> MultiTeamPersons
        {
            get
            {
                Func<IEnumerable<Person>> AllPersons = () =>
                    from t in TL
                    from p in t.member_list
                    select p;

                return
                    AllPersons().GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key); //&?????????????????????????????????????????????

            }
        }

        public IEnumerable<DateTime> PersonsBD
        {
            get
            {
                Func<IEnumerable<Person>> AllPersons = () =>
                    from t in TL
                    from p in t.member_list
                    select p;

                Func<IEnumerable<Person>> Persons = () =>
                    from p in AllPersons()
                    where (!((p is Programmer) || (p is Researcher)))
                    select p;

                return
                    Persons().GroupBy(x => x.birth_date).Where(g => g.Count() == 1).Select(g => g.Key);
            }
        }


        override public string ToString()
        {
            string res="";
            res += "______________________\n";
            foreach (Team t in TL)
            {
                res += t.ToString();
                res += '\n';
            }
            res += "______________________";
            return res;
        }

        public void AddDefaults() //ДОПИСАТЬ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            Team t = new Team("Первая команда");
            t.AddDefaults();
            TL.Add(t);

            t = new Team("Вторая команда");
            t.AddDefaults2();
            TL.Add(t);
        }




        public TeamList()
        {
            TL = new List<Team>();
        }
    }
}
