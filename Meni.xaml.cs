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
using System.Windows.Shapes;

namespace Turisticka_agencija
{
    /// <summary>
    /// Interaction logic for Meni.xaml
    /// </summary>
    public partial class Meni : Window
    {
        public Meni()
        {
            InitializeComponent();
        }

        private void btnKlijent_Click(object sender, RoutedEventArgs e)
        {
            Klijent Klijent = new Klijent();
            Visibility = Visibility.Hidden;
            Klijent.Show();
        }

        private void btnUgovor_Click(object sender, RoutedEventArgs e)
        {
            Ugovor objUgovor = new Ugovor();
            Visibility = Visibility.Hidden;
            objUgovor.Show();
        }

        private void btnPaket_Click(object sender, RoutedEventArgs e)
        {
            Paket objPaket = new Paket();
            Visibility = Visibility.Hidden;
            objPaket.Show();
        }

        private void btnHotel_Click(object sender, RoutedEventArgs e)
        {
            Hotel objHotel = new Hotel();
            Visibility = Visibility.Hidden;
            objHotel.Show();
        }

        private void btnDestinacija_Click(object sender, RoutedEventArgs e)
        {
            Destinacija objDestinacija = new Destinacija();
            Visibility = Visibility.Hidden;
            objDestinacija.Show();
        }

        private void btnOdjaviSe_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pocetna = new MainWindow();
            pocetna.Show();
            this.Close();
        }
    }
}
