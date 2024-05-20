using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarRental_desktop
{
    internal class Statisztika
    {
        public static List<Car> LoadCars()
        {
            var cars = new List<Car>();

            using var connection = new MySqlConnection("server=localhost;uid=root;pwd=;database=cars_database");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT id, license_plate_number, brand, model, daily_cost FROM cars;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var car = new Car(
                    reader.GetInt32("id"),
                    reader.GetString("license_plate_number"),
                    reader.GetString("brand"),
                    reader.GetString("model"),
                    reader.GetInt32("daily_cost")
                    );
                cars.Add(car);
            }
            return cars;
        }

        public void NumberOfCarsCheaperThan20k()
        {
            int counter = 0;
            foreach (var item in LoadCars())
            {
                if (item.daily_cost<20000)
                {
                    counter++;
                }
            }
            Console.WriteLine($"20.000 Ft-nál olcsóbb napidíjú autók száma: {counter}");
        }

        public void IsThereACarPricierThan26k()
        {
            bool result = false;
            foreach (var item in LoadCars())
            {
                if (item.daily_cost > 26000)
                {
                    result=true;
                }
            }
            if (result==false)
            {
                Console.WriteLine("Nincs az adatok között 26.000 Ft-nál drágább napidíjú autó");
            }
            else
            {
                Console.WriteLine("Van az adatok között 26.000 Ft-nál drágább napidíjú autó");
            }
        }

        public void PriciestCarDetails()
        {
            int max = int.MinValue;
            int index = -1;

            foreach (var item in LoadCars())
            {
                if (max<item.daily_cost)
                {
                    max = item.daily_cost;
                    index = item.id;
                }
            }

            //Ez azért kell mert a listában a számozás 0-tól kezdődik, de az adatbázisban az id-k meg 1-től.
            index -= 1;

            Console.WriteLine($"Legdrágább napidíjú autó: {LoadCars()[index].license_plate_number} -{LoadCars()[index].brand} –{LoadCars()[index].model} " +
                $"–{LoadCars()[index].daily_cost} Ft");
        }

        public void NumberOfCarsPerBrand()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (var item in LoadCars())
            {
                if (dictionary.ContainsKey(item.brand))
                {
                    dictionary[item.brand]++;
                }
                else
                {
                    dictionary.Add(item.brand, 1);
                }
            }

            Console.WriteLine("Autók száma:");
            foreach (var item in dictionary)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        public void IsAGivenCarDailyCostBiggerThan25k()
        {
            Console.WriteLine("Adjon meg egy rendszámot: ");
            string license_plate = Console.ReadLine();

            int index = -1;
            foreach (var item in LoadCars())
            {
                if (item.license_plate_number==license_plate)
                {
                    index = item.id;
                }
            }

            //Ez azért kell mert a listában a számozás 0-tól kezdődik, de az adatbázisban az id-k meg 1-től.
            index -= 1;

            if (index == -2)
            {
                Console.WriteLine("Nincs ilyen autó");
            }
            else if (LoadCars()[index].daily_cost<=25000)
            {
                Console.WriteLine("A megadott autó napidíja nem nagyobb mint 25.000 Ft");
            }
            else
            {
                Console.WriteLine("A megadott autó napidíja nagyobb mint 25.000 Ft");
            }
        }

        public bool DeleteARowFromDatabase(int id)
        {
            using var connection = new MySqlConnection("server=localhost;uid=root;pwd=;database=cars_database");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM cars WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);

            int affectedRows = command.ExecuteNonQuery();
            connection.Close();

            return affectedRows == 1;
        }
    }
}
