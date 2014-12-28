using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zxl.ConsolePro.Pro
{
    public class ProLinq
    {
        #region fields

        private Dictionary<int, string> countryDict = new Dictionary<int, string>();
        private Dictionary<int, string> stateDict = new Dictionary<int, string>();
        private Dictionary<int, string> cityDict = new Dictionary<int, string>();
        // key=stateId,value=cityId，为省份添加的自定义城市ID
        private Dictionary<int, int> cityDefDict = new Dictionary<int, int>();
        // key=stateId,value=cityName，为省份添加的自定义城市名称
        private Dictionary<int, string> cityDefNameDict = new Dictionary<int, string>();

        #endregion

        List<Country> countryList = new List<Country>();

        public ProLinq()
        {
            InitGeoMetaDataByTxt();

            foreach (var item0 in countryDict)
            {
                Country country = new Country();
                country.Id = item0.Key;
                country.Name = item0.Value;
                country.StateList = new List<State>();

                foreach (var item1 in stateDict)
                {
                    State state = new State();
                    state.Id = item1.Key;
                    state.Name = item1.Value;
                    state.CityList = new List<City>();

                    foreach (var item2 in cityDict)
                    {
                        City city = new City();
                        city.Id = item2.Key;
                        city.Name = item2.Value;

                        state.CityList.Add(city);
                    }

                    country.StateList.Add(state);
                }

                countryList.Add(country);
            }
        }

        public void Query()
        {
            var china = from o in countryList
                        where o.Id == 189
                        select o;
            var r = from country in countryList
                    from state in country.StateList
                    from city in state.CityList
                    where country.Id == 189 && state.Id == 505 && city.Id == 1504
                    select new
                    {
                        CountryName = country.Name,
                        StateName = state.Name,
                        CityName = city.Name
                    };

            foreach (var geo in r)
            {
                Console.WriteLine("国家：{0}，省份：{1}，城市：{2}", geo.CountryName, geo.StateName, geo.CityName);
            }
        }

        // 加载国家、省份、城市信息via txt
        private void InitGeoMetaDataByTxt()
        {
            string countryDataPath = @"E:\Tech\IPDB\geo\country.txt";
            string stateDataPath = @"E:\Tech\IPDB\geo\state.txt";
            string cityDataPath = @"E:\Tech\IPDB\geo\city.txt";
            // country data
            using (StreamReader sr = new StreamReader(countryDataPath))
            {
                string[] col;
                string line = "";
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line == null) break;

                    col = line.Split(','); // 共2列
                    int id = Convert.ToInt32(col[0]);
                    string name = col[1];
                    countryDict[id] = name;
                }
            }

            // state data
            using (StreamReader sr = new StreamReader(stateDataPath))
            {
                string[] col;
                string line = "";
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line == null) break;

                    col = line.Split(','); // 共3列：{ id，name，国家id }
                    int id = Convert.ToInt32(col[0]);
                    string name = col[1];
                    // col[3]:countryId
                    stateDict[id] = name;
                }
            }

            // city data
            using (StreamReader sr = new StreamReader(cityDataPath))
            {
                string[] col;
                string line = "";
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line == null) break;

                    col = line.Split(','); // 共4列：{ id，省份id，name，自定义flag }
                    int cityId = Convert.ToInt32(col[0]);
                    string cityName = col[2];
                    cityDict[cityId] = cityName;

                    int flag = Convert.ToInt32(col[3]);
                    if (flag == 1)
                    {
                        int stateId = Convert.ToInt32(col[1]);
                        cityDefDict[stateId] = cityId;
                        cityDefNameDict[stateId] = cityName;
                    }
                }
            }
        }
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<State> StateList { get; set; }
    }
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<City> CityList { get; set; }
    }
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<District> DistrictList { get; set; }
    }
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
