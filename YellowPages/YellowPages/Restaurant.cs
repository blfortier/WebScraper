using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowPages
{
    public class Restaurant
    {
        private string _name;
        private string _address;
        private string _cityState;
        private string _phoneNumber;

        public string Name { get => _name; set => _name = value; }
        public string Address { get => _address; set => _address = value; }
        public string CityState { get => _cityState; set => _cityState = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }

        public Restaurant() { }

        public Restaurant(string name, string address, string cityState, string phoneNumber)
        {
            this.Name = name;
            this.Address = address;
            this.CityState = cityState;
            this.PhoneNumber = phoneNumber;
        }
    }
}
