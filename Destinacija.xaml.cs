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
    /// Interaction logic for Destinacija.xaml
    /// </summary>
    public partial class Destinacija : Window
    {
        public Destinacija()
        {
            InitializeComponent();
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT * from Destinacija ";
            komanda.Connection = konekcija;
            //Grid.ItemsSource = command.ExecuteReader();
            //Grid.Visibility = Visibility.Visible;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);
            DataTable dataTable = new DataTable("Turisticka_agencija");
            dataAdapter.Fill(dataTable);
            Grid.ItemsSource = dataTable.DefaultView;
        }

        public bool isValid()
        {
            if (txtKodDrzave.Text == string.Empty)
            {
                MessageBox.Show("Kod Drzave je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtMesto.Text == string.Empty)
            {
                MessageBox.Show("Mesto je potrebano", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTipOdmora.Text == string.Empty)
            {
                MessageBox.Show("Tip Odmora je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtKontinent.Text == string.Empty)
            {
                MessageBox.Show("Kontinent je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                SqlConnection konekcija = new SqlConnection();
                konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
                konekcija.Open();
                SqlCommand komanda = new SqlCommand();
                komanda.CommandText = "INSERT INTO Destinacija (KodDrzave,Drzava,Mesto,Tip_Odmora,Kontinent) VALUES (@Kod, @Drzave,@Mesto,@TipOdmora,@Kontinent) ";
                komanda.Parameters.AddWithValue("@Kod", txtKodDrzave.Text);
                komanda.Parameters.AddWithValue("@Drzave", txtDrzava.Text);
                komanda.Parameters.AddWithValue("@Mesto", txtMesto.Text);
                komanda.Parameters.AddWithValue("@TipOdmora", txtTipOdmora.Text);
                komanda.Parameters.AddWithValue("@Kontinent", txtKontinent.Text);
                komanda.Connection = konekcija;
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Uspešno ste uneli");
                    komanda.CommandText = "SELECT * FROM Destinacija ";
                    komanda.Connection = konekcija;
                    Grid.ItemsSource = komanda.ExecuteReader();
                    txtID.Text = "";
                    Destinacija destinacija = new Destinacija();
                    destinacija.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Greška");
                }
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["Turisticka_agencija"].ConnectionString;
            konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "update Destinacija set KodDrzave = @KodDrzave,Drzava=@Drzava,Mesto = @Mesto,Tip_Odmora=@TipOdmora,Kontinent= @Kontinent where ID = @ID";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Parameters.AddWithValue("@KodDrzave", txtKodDrzave.Text);
            komanda.Parameters.AddWithValue("@Drzava", txtDrzava.Text);
            komanda.Parameters.AddWithValue("@Mesto", txtMesto.Text);
            komanda.Parameters.AddWithValue("@TipOdmora", txtTipOdmora.Text);
            komanda.Parameters.AddWithValue("@Kontinent", txtKontinent.Text);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste izmenili");
                komanda.CommandText = "SELECT * FROM Destinacija ";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                Destinacija destinacija = new Destinacija();
                destinacija.Show();
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
            komanda.CommandText = "Delete from Destinacija where ID= @ID ";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste obrisali");
                komanda.CommandText = "SELECT * FROM Destinacija";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                Destinacija destinacija = new Destinacija();
                destinacija.Show();
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

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txtID.Text = row_selected["ID"].ToString();
                txtKodDrzave.Text = row_selected["KodDrzave"].ToString();
                txtDrzava.Text = row_selected["Drzava"].ToString();
                txtMesto.Text = row_selected["Mesto"].ToString();
                txtTipOdmora.Text = row_selected["Tip_Odmora"].ToString();
                txtKontinent.Text = row_selected["Kontinent"].ToString();

            }
        }
    }
}
