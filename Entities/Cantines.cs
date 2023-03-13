using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMock.Entities
{
    public class Cantine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Price_Api { get; set; }
        public Details Details { get; set; }

        [JsonConstructor]
        public Cantine(int id, string name, decimal price, string priceApi, string details)
        {
            Id = id;
            Name = name;
            Price = price;
            Price_api = priceApi;
            if (!string.IsNullOrEmpty(details))
            {
                var detailsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(details);
                Details = new Details(detailsDictionary["description"], detailsDictionary["openTime"],
                                      detailsDictionary["closeTime"], detailsDictionary["menuDay1"],
                                      detailsDictionary["menuDay2"], detailsDictionary["menuDay3"],
                                      detailsDictionary["menuDay4"], detailsDictionary["menuDay5"],
                                      detailsDictionary["menuDay6"], detailsDictionary["menuDay7"]);
            }
        }
    }
}
