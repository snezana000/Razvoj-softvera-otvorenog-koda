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
    /// Interaction logic for Ugovor.xaml
    /// </summary>
    public partial class Ugovor : Window
    {
        public Ugovor()
        {
            InitializeComponent();
            SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
            if (konekcija.State == ConnectionState.Closed)
                konekcija.Open();
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = "SELECT * from Ugovor ";
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
            if (txtKodUgovora.Text == string.Empty)
            {
                MessageBox.Show("Kod Ugovora je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCena.Text == string.Empty)
            {
                MessageBox.Show("Cena je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Datum je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNocenje.Text == string.Empty)
            {
                MessageBox.Show("Noćenje je potrebno", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtBrojLjudi.Text == string.Empty)
            {
                MessageBox.Show("Broj ljudi je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbxKlijent.SelectedItem == null)
            {
                MessageBox.Show("Klijent je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbxPaket.SelectedItem == null)
            {
                MessageBox.Show("Paket je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbxPocetna.SelectedItem == null)
            {
                MessageBox.Show("Početna destinacija je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbxKrajnja.SelectedItem == null)
            {
                MessageBox.Show("Krajnja destinacija je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
                if (konekcija.State == ConnectionState.Closed)
                    konekcija.Open();
                SqlCommand komanda = new SqlCommand();
                komanda.CommandText = "INSERT INTO Ugovor (KodUgovora,Cena,Datum,Nocenja,BrojPutnika,ID_klijent,ID_paketa,ID_PocetnaDestinacija,ID_KrajnaDestinacija) VALUES (@Kod, @Cena,@Datum,@Nocenje,@BrojPutnika,@IDKlient,@IDPaket,@IDPocetna,@IDKrajnja) ";
                komanda.Parameters.AddWithValue("@Kod", txtKodUgovora.Text);
                komanda.Parameters.AddWithValue("@Cena", txtCena.Text);
                komanda.Parameters.AddWithValue("@Datum", DatePicker.SelectedDate);
                komanda.Parameters.AddWithValue("@Nocenje", txtNocenje.Text);
                komanda.Parameters.AddWithValue("@BrojPutnika", txtBrojLjudi.Text);
                komanda.Parameters.AddWithValue("@IDKlient", cbxKlijent.SelectedValue);
                komanda.Parameters.AddWithValue("@IDPaket", cbxPaket.SelectedValue);
                komanda.Parameters.AddWithValue("@IDPocetna", cbxPocetna.SelectedValue);
                komanda.Parameters.AddWithValue("@IDKrajnja", cbxKrajnja.SelectedValue);
                komanda.Connection = konekcija;
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Uspešno ste uneli");
                    komanda.CommandText = "SELECT * FROM Ugovor ";
                    komanda.Connection = konekcija;
                    Grid.ItemsSource = komanda.ExecuteReader();
                    txtID.Text = "";
                    Ugovor ugovor = new Ugovor();
                    ugovor.Show();
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
            if (isValid())
            {
                SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
                if (konekcija.State == ConnectionState.Closed)
                    konekcija.Open();
                SqlCommand komanda = new SqlCommand();
                komanda.CommandText = "UPDATE Ugovor set KodUgovora=@Kod,Cena=@Cena,Datum=@Datum,Nocenja=@Nocenje,BrojPutnika=@BrojPutnika,ID_klijent=@IDKlient,ID_paketa=@IDPaket,ID_PocetnaDestinacija=@IDPocetna,ID_KrajnaDestinacija=@IDKrajnja Where ID = @ID";
                komanda.Parameters.AddWithValue("@ID", txtID.Text);
                komanda.Parameters.AddWithValue("@Kod", txtKodUgovora.Text);
                komanda.Parameters.AddWithValue("@Cena", txtCena.Text);
                komanda.Parameters.AddWithValue("@Datum", DatePicker.SelectedDate);
                komanda.Parameters.AddWithValue("@Nocenje", txtNocenje.Text);
                komanda.Parameters.AddWithValue("@BrojPutnika", txtBrojLjudi.Text);
                komanda.Parameters.AddWithValue("@IDKlient", cbxKlijent.SelectedValue);
                komanda.Parameters.AddWithValue("@IDPaket", cbxPaket.SelectedValue);
                komanda.Parameters.AddWithValue("@IDPocetna", cbxPocetna.SelectedValue);
                komanda.Parameters.AddWithValue("@IDKrajnja", cbxKrajnja.SelectedValue);
                komanda.Connection = konekcija;
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Uspešno ste Izmenili");
                    komanda.CommandText = "SELECT * FROM Ugovor ";
                    komanda.Connection = konekcija;
                    Grid.ItemsSource = komanda.ExecuteReader();
                    txtID.Text = "";
                    Ugovor ugovor = new Ugovor();
                    ugovor.Show();
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
            komanda.CommandText = "Delete from Ugovor where ID= @ID ";
            komanda.Parameters.AddWithValue("@ID", txtID.Text);
            komanda.Connection = konekcija;
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Uspešno ste obrisali");
                komanda.CommandText = "SELECT * FROM Ugovor ";
                komanda.Connection = konekcija;
                Grid.ItemsSource = komanda.ExecuteReader();
                txtID.Text = "";
                Ugovor ugovor = new Ugovor();
                ugovor.Show();
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
                txtKodUgovora.Text = row_selected["KodUgovora"].ToString();
                txtCena.Text = row_selected["Cena"].ToString();
                DatePicker.SelectedDate = Convert.ToDateTime(row_selected["Datum"].ToString());
                txtNocenje.Text = row_selected["Nocenja"].ToString();
                txtBrojLjudi.Text = row_selected["BrojPutnika"].ToString();
            }
        }

        private void cbxKlijent_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
            if (konekcija.State == ConnectionState.Closed)
                konekcija.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,Ime+Prezime as NazivK FROM Klijent ", konekcija);
            DataSet ds = new DataSet();
            da.Fill(ds, "Klijent");

            //Populate the combobox
            cbxKlijent.ItemsSource = ds.Tables[0].DefaultView;
            cbxKlijent.DisplayMemberPath = "NazivK";

            cbxKlijent.SelectedValuePath = "ID";
        }

        private void cbxPaket_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
            if (konekcija.State == ConnectionState.Closed)
                konekcija.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,Naziv  FROM Paket ", konekcija);
            DataSet ds = new DataSet();
            da.Fill(ds, "Paket");

            //Populate the combobox
            cbxPaket.ItemsSource = ds.Tables[0].DefaultView;
            cbxPaket.DisplayMemberPath = "Naziv";

            cbxPaket.SelectedValuePath = "ID";
        }

        private void cbxPocetna_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
            if (konekcija.State == ConnectionState.Closed)
                konekcija.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,Drzava FROM Destinacija ", konekcija);
            DataSet ds = new DataSet();
            da.Fill(ds, "Destinacija");

            //Populate the combobox
            cbxPocetna.ItemsSource = ds.Tables[0].DefaultView;
            cbxPocetna.DisplayMemberPath = "Drzava";

            cbxPocetna.SelectedValuePath = "ID";
        }

        private void cbxKrajnja_Loaded(object sender, RoutedEventArgs e)
        {

            SqlConnection konekcija = new SqlConnection(@"Data Source=DESKTOP-T4T8KF4\SQLEXPRESS;Initial Catalog=Turisticka agencija;Integrated Security=True");
            if (konekcija.State == ConnectionState.Closed)
                konekcija.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,Drzava FROM Destinacija ", konekcija);
            DataSet ds = new DataSet();
            da.Fill(ds, "Destinacija");

            //Populate the combobox
            cbxKrajnja.ItemsSource = ds.Tables[0].DefaultView;
            cbxKrajnja.DisplayMemberPath = "Drzava";

            cbxKrajnja.SelectedValuePath = "ID";
        }
    }
}
