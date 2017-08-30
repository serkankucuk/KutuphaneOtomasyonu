using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kütüphane
{
    public partial class UyeListeleDuzenle : Form
    {
        public UyeListeleDuzenle()
        {
            InitializeComponent();
        }
 SqlConnection baglanti =
         new SqlConnection(@"Data Source=NB-SERKAN\SQLEXPRESS;Initial Catalog=Kutuphane;User ID=sa;Password=123456");
           
        private void UyeListeleDuzenle_Load(object sender, EventArgs e)
        {
            string secilen = Form1.UyeListeleSecilen.ToString();
            baglanti.Open();
            SqlCommand UyeListeleDuzenle = new SqlCommand("SELECT *  FROM Uyeler WHERE UyeNo = @secilen  ", baglanti);
            UyeListeleDuzenle.Parameters.Add("@secilen", SqlDbType.Int).Value = Form1.UyeListeleSecilen.ToString();
            SqlDataReader sonuclar = UyeListeleDuzenle.ExecuteReader();
            while (sonuclar.Read())
            {
                labelUyeListeleDuzenleSecilen.Text = sonuclar["UyeNo"].ToString();
                textBoxUyeListeleDuzenleAd.Text = sonuclar["UyeAd"].ToString();
                textBoxUyeListeleDuzenleSoyad.Text = sonuclar["UyeSoyad"].ToString();
                maskedTextBoxUyeListeleDuzenleTelefon.Text = sonuclar["UyeTelefon"].ToString();
                textBoxUyeListeleDuzenleEposta.Text = sonuclar["UyeEposta"].ToString();
                textBoxUyeListeleDuzenleAdres.Text = sonuclar["UyeAdres"].ToString();
              

            }
            baglanti.Close();

        }

        private void buttonUyeListeleDuzenleKaydet_Click(object sender, EventArgs e)
        {
         
            try
            {
             SqlCommand komutUyeListeleDuzenle = new SqlCommand("UPDATE Uyeler SET UyeAd=@UyeAd,UyeSoyad=@UyeSoyad,UyeTelefon=@UyeTelefon,UyeEposta=@UyeEposta,UyeAdres=@UyeAdres WHERE UyeNo = @secilen  ", baglanti); 
                komutUyeListeleDuzenle.Parameters.Add("@secilen", SqlDbType.Int).Value = Form1.UyeListeleSecilen.ToString();
			  komutUyeListeleDuzenle.Parameters.Add("@UyeAd", SqlDbType.NVarChar).Value = textBoxUyeListeleDuzenleAd.Text;
               komutUyeListeleDuzenle.Parameters.Add("@UyeSoyad", SqlDbType.NVarChar).Value = textBoxUyeListeleDuzenleSoyad.Text;
               komutUyeListeleDuzenle.Parameters.Add("@UyeTelefon", SqlDbType.NVarChar).Value = maskedTextBoxUyeListeleDuzenleTelefon.Text.ToString();
                komutUyeListeleDuzenle.Parameters.Add("@UyeEposta", SqlDbType.NVarChar).Value = textBoxUyeListeleDuzenleEposta.Text;
                komutUyeListeleDuzenle.Parameters.Add("@UyeAdres", SqlDbType.NVarChar).Value = textBoxUyeListeleDuzenleAdres.Text;
			
			

            baglanti.Open();
            komutUyeListeleDuzenle.ExecuteNonQuery();
            MessageBox.Show("Üye Düzenlendi");
            baglanti.Close();
            this.Hide();
            }
            catch (Exception)
            {

                MessageBox.Show("hata");
            }
            
        }

        private void buttonUyeListeleDuzenleIptal_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonUyeListeleDuzenleSil_Click(object sender, EventArgs e)
        {
            DialogResult sonuc;
sonuc = MessageBox.Show("Üyeyi silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.OKCancel);
if (sonuc == DialogResult.OK)
{
    try
    {
        string secilen = Form1.UyeListeleSecilen.ToString();
       
        baglanti.Open();
        SqlCommand KayitSil = new SqlCommand("DELETE FROM Uyeler WHERE UyeNo=@secilen  ", baglanti);
        KayitSil.Parameters.Add("@secilen", SqlDbType.Int).Value = Form1.UyeListeleSecilen.ToString();
        KayitSil.ExecuteNonQuery();
        baglanti.Close();
        MessageBox.Show("Kayıt başarıyla silindi");
        this.Hide();
    }
    catch (Exception)
    {

        MessageBox.Show("Bir hata oluştu");
    }
}
        }
    }
}
