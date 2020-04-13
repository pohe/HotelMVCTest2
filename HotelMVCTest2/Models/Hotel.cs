using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMVCTest2.Models
{
    public class Hotel
    {
        private int _id;
        private string _name;
        private string _address;

        public Hotel()
        {
        }
        public Hotel(int id, string name, string address)
        {
            _id = id;
            _name = name;
            _address = address;
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;

            }
        }

        [Display(Name="Hotel Name")]
        [Required]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }
        [Required]
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }

    }
}
