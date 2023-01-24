using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCart.DAl.Entities.Order_Aggregate
{
    public class Address
    {
        public Address()
        {

        }
        public Address(string firstName, string secondName, string country, string city, string street)
        {
            FirstName=firstName;
            SecondName=secondName;
            Country=country;
            City=city;
            Street=street;
        }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

    }
}
