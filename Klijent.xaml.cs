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
using System.Windows.Shapes;

namespace Turisticka_agencija
{
    /// <summary>
    /// Interaction logic for Klijent.xaml
    /// </summary>
    public partial class Klijent : Window
    {
        public Klijent()
        {
            InitializeComponent();
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT * from Klijent ";
            komanda.Connection = konekcija;
            //Grid.ItemsSource = komanda.ExecuteReader();
            //Grid.Visibility = Visibility.Visible;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);
            DataTable dataTable = new DataTable("Turisticka_agencija");
            dataAdapter.Fill(dataTable);

            Grid.ItemsSource = dataTable.DefaultView;
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txtID.Text = row_selected["ID"].ToString();
                txtKodKlienta.Text = row_selected["KodKlijenta"].ToString();
                txtIme.Text = row_selected["Ime"].ToString();
                txtPrezime.Text = row_selected["Prezime"].ToString();
                txtTelefon.Text = row_selected["Telefon"].ToString();
                txtAdresa.Text = row_selected["Adresa"].ToString();
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                SqlConnection konekcija = new SqlConnection();
                konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
                konekcija.Open();
                SqlCommand komanda = new SqlCommand();
                komanda.CommandText = "INSERT INTO Klijent (KodKlijenta,Ime,Prezime,Telefon,Adresa) VALUES (@Kod, @Ime,@Prezime,@Telefon,@Adresa) ";
                komanda.Parameters.AddWithValue("@Kod", txtKodKlienta.Text);
                komanda.Parameters.AddWithValue("@Ime", txtIme.Text);
                komanda.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
                komanda.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
                komanda.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
                komanda.Connection = konekcija;
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Uspešno ste uneli");
                    komanda.CommandText = "SELECT * FROM Klijent ";
                    komanda.Connection = konekcija;
                    Grid.ItemsSource = komanda.ExecuteReader();
                    txtID.Text = "";
                    txtIme.Text = "";
                    txtKodKlienta.Text = "";
                    txtAdresa.Text = "";
                    txtPrezime.Text = "";
                    txtTelefon.Text = "";
                    Klijent klijent = new Klijent();
                    klijent.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Greška");
                }
            }
        }

        public bool isValid()
        {
            if (txtKodKlienta.Text == string.Empty)
            {
                MessageBox.Show("Kod klijenta je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtIme.Text == string.Empty)
            {
                MessageBox.Show("Ime je potrebno", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPrezime.Text == string.Empty)
            {
                MessageBox.Show("Prezime je potrebno", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTelefon.Text == string.Empty)
            {
                MessageBox.Show("Telefon je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtAdresa.Text == string.Empty)
            {
                MessageBox.Show("Adresa je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "update Klijent set KodKlijenta = @KodKlienta,Ime=@Ime,Prezime = @Prezime,Telefon=@Telefon,Adresa = @Adresa where ID = @ID";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Parameters.AddWithValue("@KodKlienta", txtKodKlienta.Text);
            komanda.Parameters.AddWithValue("@Ime", txtIme.Text);
            komanda.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
            komanda.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komanda.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste izmenili");
                komanda.CommandText = "SELECT * FROM Klijent ";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                Klijent klijent = new Klijent();
                klijent.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Greška");
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "Delete from Klijent where ID= @ID ";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste obrisali");
                komanda.CommandText = "SELECT * FROM Klijent";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                txtIme.Text = "";
                txtKodKlienta.Text = "";
                txtAdresa.Text = "";
                txtPrezime.Text = "";
                txtTelefon.Text = "";
                Klijent klijent = new Klijent();
                klijent.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Greška");
            }
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            Meni meni = new Meni();
            meni.Show();
            this.Close();
        }
    }
}
