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
    /// Interaction logic for Paket.xaml
    /// </summary>
    public partial class Paket : Window
    {
        public Paket()
        {
            InitializeComponent();
            SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
            if (konekcija.State == ConnectionState.Closed)
                konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT * from Paket ";
            komanda.Connection = konekcija;
            //Grid.ItemsSource = command.ExecuteReader();
            //Grid.Visibility = Visibility.Visible;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);
            DataTable dataTable = new DataTable("Turisticka agencija");
            dataAdapter.Fill(dataTable);
            Grid.ItemsSource = dataTable.DefaultView;
        }

        public bool isValid()
        {
            if (txtKodPaketa.Text == string.Empty)
            {
                MessageBox.Show("Kod Paketa je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNaziv.Text == string.Empty)
            {
                MessageBox.Show("Naziv je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTipPaketa.Text == string.Empty)
            {
                MessageBox.Show("Tip Paketa je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtOpis.Text == string.Empty)
            {
                MessageBox.Show("Opis je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPopust.Text == string.Empty)
            {
                MessageBox.Show("Popust je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (DatePicker1.SelectedDate == null)
            {
                MessageBox.Show("Datum OD je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (DatePicker2.SelectedDate == null)
            {
                MessageBox.Show("Datum DO je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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
                txtKodPaketa.Text = row_selected["KodPaketa"].ToString();
                txtNaziv.Text = row_selected["Naziv"].ToString();
                txtTipPaketa.Text = row_selected["Tip_paketa"].ToString();
                txtOpis.Text = row_selected["Opis"].ToString();
                txtPopust.Text = row_selected["Popust"].ToString();
                DatePicker1.SelectedDate = Convert.ToDateTime(row_selected["TrajanjeOD"].ToString());
                DatePicker2.SelectedDate = Convert.ToDateTime(row_selected["TrajanjeDO"].ToString());
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
                if (konekcija.State == ConnectionState.Closed)
                    konekcija.Open();
                SqlCommand komanda = new SqlCommand();
                komanda.CommandText = "INSERT INTO Paket (KodPaketa,Naziv,Tip_Paketa,Opis,Popust,TrajanjeOD,TrajanjeDO) VALUES (@Kod, @Naziv,@TipPaketa,@Opis,@Popust,@OD,@DO) ";
                komanda.Parameters.AddWithValue("@Kod", txtKodPaketa.Text);
                komanda.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
                komanda.Parameters.AddWithValue("@TipPaketa", txtTipPaketa.Text);
                komanda.Parameters.AddWithValue("@Opis", txtOpis.Text);
                komanda.Parameters.AddWithValue("@Popust", txtPopust.Text);
                komanda.Parameters.AddWithValue("@OD", DatePicker1.SelectedDate);
                komanda.Parameters.AddWithValue("@DO", DatePicker2.SelectedDate);
                komanda.Connection = konekcija;
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Uspešno ste uneli");
                    komanda.CommandText = "SELECT * FROM Paket ";
                    komanda.Connection = konekcija;
                    Grid.ItemsSource = komanda.ExecuteReader();
                    txtID.Text = "";
                    Paket paket = new Paket();
                    paket.Show();
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
            SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
            if (konekcija.State == ConnectionState.Closed)
                konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "Delete from Paket where ID= @ID ";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste obrisali");
                komanda.CommandText = "SELECT * FROM Paket";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                Paket paket = new Paket();
                paket.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Greška");
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
            if (konekcija.State == ConnectionState.Closed)
                konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "update Paket set KodPaketa = @KodPaketa,Naziv=@Naziv,Tip_Paketa = @TipPaketa,Opis=@Opis,Popust= @Popust ,TrajanjeOD=@OD,TrajanjeDO=@DO where ID = @ID";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Parameters.AddWithValue("@KodPaketa", txtKodPaketa.Text);
            komanda.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
            komanda.Parameters.AddWithValue("@TipPaketa", txtTipPaketa.Text);
            komanda.Parameters.AddWithValue("@Opis", txtOpis.Text);
            komanda.Parameters.AddWithValue("@Popust", txtPopust.Text);
            komanda.Parameters.AddWithValue("@OD", DatePicker1.SelectedDate);
            komanda.Parameters.AddWithValue("@DO", DatePicker2.SelectedDate);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste izmenili");
                komanda.CommandText = "SELECT * FROM Paket ";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                Paket paket = new Paket();
                paket.Show();
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
