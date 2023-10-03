using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace Turisticka_agencija
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            try
            {
                if (konekcija.State == ConnectionState.Closed)
                    konekcija.Open();
                String query = "SELECT COUNT (1) FROM Korisnik WHERE ImeKorisnika=@ImeKorisnika and Lozinka=@Lozinka";
                SqlCommand komanda = new SqlCommand(query, konekcija);
                komanda.CommandType = CommandType.Text;
                komanda.Parameters.AddWithValue("@ImeKorisnika", txtImeKorisnika.Text);
                komanda.Parameters.AddWithValue("@Lozinka", txtLozinka.Password);
                int count = Convert.ToInt32(komanda.ExecuteScalar());
                if (count == 1)
                {
                    Meni meni = new Meni();
                    meni.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Korisničko ime ili lozinka nisu ispravni!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                konekcija.Close();
            }
        }

    }
}
