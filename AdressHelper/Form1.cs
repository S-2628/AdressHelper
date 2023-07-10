using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using GrapeCity;

namespace AdressHelper
{
    public partial class Form1 : Form
    {
        List<Data> Datas = new List<Data>();
        List<DataFurigana> DataFuriganas = new List<DataFurigana>();
        public Form1()
        {
            InitializeComponent();

            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            listBox2.SelectedIndexChanged += ListBox2_SelectedIndexChanged;
            listBox3.SelectedIndexChanged += ListBox3_SelectedIndexChanged;

            ReadCsv();

            List<Data> datas = Datas.Distinct(new KenEqualityComparer()).ToList();
           

            listBox1.Items.Clear();
            foreach (Data data in datas)
                listBox1.Items.Add(data.Ken);
        }

        public class Data
        {
            public Data(string kenfurigana,string cityfurigana,string namefurigana,string ken, string city, string name)
            {
                KenFurigana = kenfurigana;
                CityFurigana= cityfurigana;
                NameFurigana = namefurigana;
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

        public class DataFurigana
        {
            public DataFurigana(string ken,string city,string cho)
            {
                Ken = ken;
                City = city;
                Cho = cho;
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

            public string Cho
            {
                get;
                private set;
            }
        }



        class KenEqualityComparer : IEqualityComparer<Data>
        {
            public bool Equals(Data x, Data y)
            {
                if (x.Ken == y.Ken)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(Data obj)
            {
                return 0;
            }
        }

        class CityEqualityComparer : IEqualityComparer<Data>
        {
            public bool Equals(Data x, Data y)
            {
                if (x.City == y.City)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(Data obj)
            {
                return 0;
            }
        }

        class NameEqualityComparer : IEqualityComparer<Data>
        {
            public bool Equals(Data x, Data y)
            {
                if (x.Name == y.Name)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(Data obj)
            {
                return 0;
            }
        }

        void ReadCsv()
        {
            StreamReader sr = new StreamReader(@"C:\Users\s-oha\source\repos\AdressHelper\AdressHelper\\KEN_ALL.CSV", Encoding.GetEncoding("Shift_JIS"));

            Datas.Clear();
            while (true)
            {
                string str = sr.ReadLine();
                if (str == null)
                    break;

                string[] strs = str.Split(new char[] { ',' });
                Datas.Add(new Data(strs[1], strs[2], strs[3], strs[4], strs[5], strs[6]));
            }
            sr.Close();
        }
        
        void ReadFurigana()
        {
            StreamReader sr = new StreamReader(@"C:\Users\s-oha\source\repos\AdressHelper\AdressHelper\\KEN_ALL.CSV", Encoding.GetEncoding("Shift_JIS"));

            DataFuriganas.Clear();
            while (true)
            {
                string str = sr.ReadLine();
                if (str == null)
                    break;

                string[] strs = str.Split(new char[] { ',' });
                DataFuriganas.Add(new DataFurigana(strs[2], strs[3], strs[4]));
            }
            sr.Close();
        }
        

        public bool Equals(Data x,Data y)
        {
            if(x.Ken == y.Ken) 
                return true;
            else
                return false;
        }

        public int GetHashCode(Data obj)
        {
            return 0;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCities();
            listBox2.SelectedIndex = -1;
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowNames();
            listBox3.SelectedIndex = -1;
            textBox1.Text = "";
        }

        void ShowCities()
        {
            int index = listBox1.SelectedIndex;
            if (index == -1)
                return;

            string ken = (string)listBox1.Items[index];
            List<Data> kenDatas = Datas.Where(x => x.Ken == ken).ToList();
            List<Data> cityDatas = kenDatas.Distinct(new CityEqualityComparer()).ToList();

            // 50音順に並べて区切りも入れる
            cityDatas.Add(new Data("", "ｱ", "", "", "　ア ========", ""));
            cityDatas.Add(new Data("", "ｶ", "", "", "　カ ========", ""));
            cityDatas.Add(new Data("", "ｻ", "", "", "　サ ========", ""));
            cityDatas.Add(new Data("", "ﾀ", "", "", "　タ ========", ""));
            cityDatas.Add(new Data("", "ﾅ", "", "", "　ナ ========", ""));
            cityDatas.Add(new Data("", "ﾊ", "", "", "　ハ ========", ""));
            cityDatas.Add(new Data("", "ﾏ", "", "", "　マ ========", ""));
            cityDatas.Add(new Data("", "ﾔ", "", "", "　ヤ ========", ""));
            cityDatas.Add(new Data("", "ﾗ", "", "", "　ラ ========", ""));
            cityDatas.Add(new Data("", "ﾜ", "", "", "　ワ ========", ""));

            cityDatas = cityDatas.OrderBy(x => x.CityFurigana).ToList();

            listBox2.Items.Clear();
            foreach (Data data in cityDatas)
                listBox2.Items.Add(data.City);
        }
    

    void ShowNames()
        {
            int index1 = listBox1.SelectedIndex;
            if (index1 == -1)
                return;

            int index2 = listBox2.SelectedIndex;
            if (index2 == -1)
                return;

            string ken = (string)listBox1.Items[index1];
            string city = (string)listBox2.Items[index2];

            List<Data> cityDatas = Datas.Where(x => x.Ken == ken && x.City == city).ToList();
            List<Data> nameDatas = cityDatas.Distinct(new NameEqualityComparer()).ToList();

            // 50音順に並べて区切りも入れる
            nameDatas.Add(new Data("", "", "ｱ", "", "", "　ア ========"));
            nameDatas.Add(new Data("", "", "ｶ", "", "", "　カ ========"));
            nameDatas.Add(new Data("", "", "ｻ", "", "", "　サ ========"));
            nameDatas.Add(new Data("", "", "ﾀ", "", "", "　タ ========"));
            nameDatas.Add(new Data("", "", "ﾅ", "", "", "　ナ ========"));
            nameDatas.Add(new Data("", "", "ﾊ", "", "", "　ハ ========"));
            nameDatas.Add(new Data("", "", "ﾏ", "", "", "　マ ========"));
            nameDatas.Add(new Data("", "", "ﾔ", "", "", "　ヤ ========"));
            nameDatas.Add(new Data("", "", "ﾗ", "", "", "　ラ ========"));
            nameDatas.Add(new Data("", "", "ﾜ", "", "", "　ワ ========"));

            nameDatas = nameDatas.OrderBy(x => x.NameFurigana).ToList();

            listBox3.Items.Clear();
            foreach (Data data in nameDatas)
                listBox3.Items.Add(data.Name);
        }

        void ShowFurigana()
        {

            int index1 = listBox1.SelectedIndex;
            if (index1 == -1)
                return;

            int index2 = listBox2.SelectedIndex;
            if (index2 == -1)
                return;

            int index3 = listBox3.SelectedIndex;
            if (index3 == -1) 
                return;

            string ken = "";
            string city = "";

            List<DataFurigana> cityDatas = DataFuriganas.Where(x => x.Ken == ken && x.City == city).ToList();
            
            cityDatas = cityDatas.OrderBy(x => x.Cho).ToList();

            textBox2.Text = "";
 
        }

        private void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetName();
            ShowFurigana();
        }

        void GetName()
        {
            int index1 = listBox1.SelectedIndex;
            int index2 = listBox2.SelectedIndex;
            int index3 = listBox3.SelectedIndex;

            if(index1 == -1 || index2 == -1 || index3 == -1) 
            {
                textBox1.Text = "";
                return;
            }

            string ken = (string)listBox1.Items[index1];
            string city = (string)listBox2.Items[index2];
            string cho = (string)listBox3.Items[index3];

            textBox1.Text = String.Format("{0}{1}{2}",ken, city, cho);
        }
    }
}
