using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_desktop
{
    internal class Car
    {
        public Car(int id, string license_plate_number, string brand, string model, int daily_cost)
        {
            this.id = id;
            this.license_plate_number = license_plate_number;
            this.brand = brand;
            this.model = model;
            this.daily_cost = daily_cost;
        }

        public int id { get; set; }
        public string license_plate_number { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public int daily_cost { get; set; }
    }
}
