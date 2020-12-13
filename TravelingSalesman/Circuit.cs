using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TravelingSalesman
{
    public class Circuit
    {
        public List<City> Path { get; set; }
        public double Distance
        {
            get
            {
                double value = 0;
                for (int i = 0; i < Path.Count; i++)
                {
                    value += Math.Sqrt(Math.Pow(Path[(i + 1) % Path.Count].Latitude - Path[i].Latitude, 2) + Math.Pow(Path[(i + 1) % Path.Count].Longitude - Path[i].Longitude, 2));
                }
                return value;
            }
        }
        public Circuit()
        {
            Path = JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(@"C:\Users\Administrator\source\repos\TravelingSalesman\TravelingSalesman\Capitals.json")).Permute();
        }
        public void PrintPath()
        {
            for (int i = 0; i < Path.Count; i++)
            {
                Console.WriteLine(Path[i].State + "~" + Path[(i + 1) % Path.Count].State);
            }
        }
    }
    public struct City
    {
        public string State;
        public double Latitude;
        public double Longitude;
    }
}
