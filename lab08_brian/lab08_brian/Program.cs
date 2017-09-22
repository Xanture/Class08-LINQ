using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace lab08_brian
{
    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string address { get; set; }
        public string borough { get; set; }
        public string neighborhood { get; set; }
        public string county { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class RootObject
    {
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"data.json";
            string json = "";
            using (StreamReader r = new StreamReader(filePath))
            {
                json = r.ReadToEnd();
            }
            RootObject outPutJson = JsonConvert.DeserializeObject<RootObject>(json);

            var neightborhoods = from n in outPutJson.features
                                 where n.properties.neighborhood != null
                                 select n;
            foreach (Feature n in neightborhoods)
            {
                Console.WriteLine(n.properties.neighborhood);
            }
            Console.Read();
            Console.WriteLine("---------------------------------------");

            var notEmptyField = neightborhoods.Where(e => !string.IsNullOrEmpty(e.properties.neighborhood));

            foreach (Feature e in notEmptyField)
            {
                Console.WriteLine(e.properties.neighborhood);
            }
            Console.Read();
            Console.WriteLine("---------------------------------------");

            var noDuplicates = notEmptyField.GroupBy(p => p.properties.neighborhood).Select(d => d.First()).ToList();

            foreach (Feature d in noDuplicates)
            {
                Console.WriteLine(d.properties.neighborhood);
            }
            Console.Read();
            Console.WriteLine("----------------------------------------");

            var allTheThings = outPutJson.features.Where(n => !string.IsNullOrEmpty(n.properties.neighborhood))
                .GroupBy(p => p.properties.neighborhood).Select(d => d.First()).ToList();

            foreach (Feature a in allTheThings)
            {
                Console.WriteLine(a.properties.neighborhood);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Nailed it!");

            Console.Read();
        }
    }
}
