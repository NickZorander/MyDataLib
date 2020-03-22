using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataLib
{
    [Serializable]
    public class TeamObservable : System.Collections.ObjectModel.ObservableCollection<Person>, INotifyPropertyChanged
    {
        public TeamObservable(string name)
        {
            group_name = name;
            IfChanged = false;
            base.CollectionChanged += Handler;
            
            research_themes = new List<string>();

            research_themes.Add("Parallel Computations");
            research_themes.Add("Linear Algebra");
            research_themes.Add("Graphics");
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged; 
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool changed;

        public bool IfChanged
        {
            get => changed;
            set
            {
                changed = value;
                NotifyPropertyChanged("IfChanged");
            }

        }

        public string group_name { get; set; }

        private List<string> research_themes;

        public List<string> RT
        {
            get
            {
                return research_themes;
            }
        }

        public double researchers_percent {
            get
            {
                int count = Get_Researchers.Count();
                
                if (base.Count != 0)
                    return  100 * count / base.Count;

                return 0;
            }

        }


        

        public void AddPerson(params Person[] persons)
        {
            //тут добавляем новые параметры в коллекцию унаследованую от предка  
            for (int i = 0; i < persons.Length; i++)
            {
                base.Add((Person)persons[i].DeepCopy());
            }
        }

        public void RemovePersonAt(int index)
        {
            //стираем персону по индексу
            try
            {
                base.RemoveAt(index);
            }
            catch
            {

            }
        }

    

        public void AddDefaults()
        {
            //добавить дефолты 
            AddDefaultProgrammer();
            AddDefaultResearcher();
        }

        public void AddDefaultProgrammer()
        {
            //добавить прогера по умолчанию
            base.Add(new Programmer());
        }

        public void AddDefaultResearcher()
        {
            //добавить исследователя по уолчанию
            base.Add(new Researcher());
        }

        public override string ToString()
        {
            return base.ToString() + '\n' + group_name + '\n' + researchers_percent + '\n';
        }

        public static bool Save(string filename, TeamObservable obj)
        {
            // сохранить в файл 
            BinaryFormatter formatter = new BinaryFormatter();
 
            try
            {
                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);

                formatter.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ((TeamObservable)obj).IfChanged = false;
            return true;
        }

        public static bool Load(string filename, ref TeamObservable obj)
        {
            //загрузить из файла 

            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);

                obj = (TeamObservable)formatter.Deserialize(fs);

                obj.IfChanged = false;
                obj.CollectionChanged += Handler;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            return true;

        }


        public static void Handler(object source, NotifyCollectionChangedEventArgs args)
        {
            ((TeamObservable)source).IfChanged = true;
        }

        public IEnumerable<Person> Get_Researchers => from r in this where (r is Researcher) select (r as Researcher);
        public IEnumerable<Person> Get_Everyone => from r in this where (r is Person) select (r as Person);
    }
}
