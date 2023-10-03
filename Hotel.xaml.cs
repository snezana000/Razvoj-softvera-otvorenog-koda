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
    /// Interaction logic for Hotel.xaml
    /// </summary>
    public partial class Hotel : Window
    {
        public Hotel()
        {
            InitializeComponent();
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT * from Hotel ";
            komanda.Connection = konekcija;
            //Grid.ItemsSource = command.ExecuteReader();
            //Grid.Visibility = Visibility.Visible;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);
            DataTable dataTable = new DataTable("Turisticka_agencija");
            dataAdapter.Fill(dataTable);
            Grid.ItemsSource = dataTable.DefaultView;

            cbxBazen.Items.Add("Ne");
            cbxBazen.Items.Add("Da");
            cbxKlima.Items.Add("Ne");
            cbxKlima.Items.Add("Da");
            cbxParking.Items.Add("Ne");
            cbxParking.Items.Add("Da");
        }

        public bool isValid()
        {
            if (txtKodHotela.Text == string.Empty)
            {
                MessageBox.Show("Kod Hotela je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNaziv.Text == string.Empty)
            {
                MessageBox.Show("Naziv je potrebn", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtAdresa.Text == string.Empty)
            {
                MessageBox.Show("Adresa je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbxBazen.SelectedItem == null)
            {
                MessageBox.Show("Popunite Bazen", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbxKlima.SelectedItem == null)
            {
                MessageBox.Show("Popunite Klimu ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbxParking.SelectedItem == null)
            {
                MessageBox.Show("Popunite Parking", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbxDrzava.SelectedItem == null)
            {
                MessageBox.Show("Popunite Državu ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txtID.Text = row_selected["ID"].ToString();
                txtKodHotela.Text = row_selected["KodHotela"].ToString();
                txtNaziv.Text = row_selected["Naziv"].ToString();
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
                komanda.CommandText = "INSERT INTO Hotel (KodHotela,Naziv,Adresa,Bazen,Klima,Parking,ID_Destinacije) VALUES (@Kod, @Naziv,@Adresa,@Bazen,@Klima,@Parking,@IDDestinacije) ";
                komanda.Parameters.AddWithValue("@Kod", txtKodHotela.Text);
                komanda.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
                komanda.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
                komanda.Parameters.AddWithValue("@Bazen", cbxBazen.Text);
                komanda.Parameters.AddWithValue("@Klima", cbxKlima.Text);
                komanda.Parameters.AddWithValue("@Parking", cbxParking.Text);
                komanda.Parameters.AddWithValue("@IDDestinacije", cbxDrzava.SelectedValue);
                komanda.Connection = konekcija;
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Uspešno ste uneli");
                    komanda.CommandText = "SELECT * FROM Hotel ";
                    komanda.Connection = konekcija;
                    Grid.ItemsSource = komanda.ExecuteReader();
                    txtID.Text = "";
                    Hotel hotel = new Hotel();
                    hotel.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Greška");
                }
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "Delete from Hotel where ID= @ID ";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste obrisali");
                komanda.CommandText = "SELECT * FROM Hotel";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                Hotel hotel = new Hotel();
                hotel.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Greška");
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "update Hotel set KodHotela = @KodHotela,Naziv=@Naziv,Adresa = @Adresa,Bazen=@Bazen,Klima= @Klima ,Parking=@Parking,ID_destinacije=@IDDestinacije where ID = @ID";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Parameters.AddWithValue("@KodHotela", txtKodHotela.Text);
            komanda.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
            komanda.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
            komanda.Parameters.AddWithValue("@Bazen", cbxBazen.SelectedItem);
            komanda.Parameters.AddWithValue("@Klima", cbxKlima.SelectedItem);
            komanda.Parameters.AddWithValue("@Parking", cbxParking.SelectedItem);
            komanda.Parameters.AddWithValue("@IDDestinacije", cbxDrzava.SelectedValue);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste izmenili");
                komanda.CommandText = "SELECT * FROM Hotel ";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                Hotel hotel = new Hotel();
                hotel.Show();
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

        private void cbxDrzava_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,Drzava FROM Destinacija ", konekcija);
            DataSet ds = new DataSet();
            da.Fill(ds, "Destinacija");
            //Populate the combobox
            cbxDrzava.ItemsSource = ds.Tables[0].DefaultView;
            cbxDrzava.DisplayMemberPath = "Drzava";
            cbxDrzava.SelectedValuePath = "ID";
        }
    }
}
