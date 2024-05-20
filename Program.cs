using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarRental_desktop
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Contains("--stat"))
            {
                try
                {
                    var test = Statisztika.LoadCars();
                }
                catch (Exception errormessage)
                {
                    Console.WriteLine(errormessage);
                    Environment.Exit(1);
                }
                Statisztika stat = new Statisztika();

                stat.NumberOfCarsCheaperThan20k();
                stat.IsThereACarPricierThan26k();
                stat.PriciestCarDetails();
                stat.NumberOfCarsPerBrand();
                stat.IsAGivenCarDailyCostBiggerThan25k();

                Console.ReadKey();
            }
            else
            {
                Application app = new Application();
                app.Run(new MainWindow());
            }
        }
    }
}
