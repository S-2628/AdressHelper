using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdressHelper
{
    public class Data
    {
        public Data(string kenFurigana, string cityFurigana, string nameFurigana, string ken, string city, string name)
        {
            KenFurigana = kenFurigana;
            CityFurigana = cityFurigana;
            NameFurigana = nameFurigana;
            Ken = ken;
            City = city;
            Name = name;
        }
    

    public string KenFurigana
        {
            get;
            private set;
        }

        public string CityFurigana
        {
            get;
            private set;
        }

        public string NameFurigana
        {
            get;
            private set;
        }

        public string Ken
        {
            get;
            private set;
        }

        public string City
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }
    }
}
