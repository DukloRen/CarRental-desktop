using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRental_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        public void LoadDataGrid()
        {
            try
            {
                dataGrid.ItemsSource = Statisztika.LoadCars();
            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                MessageBox.Show(errormessage);
                Environment.Exit(1);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Statisztika stat = new Statisztika();
            var selectedCar = dataGrid.SelectedItem as Car;

            if (selectedCar == null)
            {
                MessageBox.Show("Törléshez előbb válasszon ki autót");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Biztos szeretné törölni a kiválasztott autót?", "Törlés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (stat.DeleteARowFromDatabase(selectedCar.id))
                    {
                        MessageBox.Show("Sikeres törlés!");
                    }
                    else
                    {
                        MessageBox.Show("Sikertelen törlés!");
                    }
                }
            }
            LoadDataGrid();
        }
    }
}
