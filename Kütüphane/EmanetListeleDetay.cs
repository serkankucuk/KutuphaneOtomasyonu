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
    public partial class EmanetListeleDetay : Form
    {
        public EmanetListeleDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti =
                       new SqlConnection(@"Data Source=NB-SERKAN\SQLEXPRESS;Initial Catalog=Kutuphane;User ID=sa;Password=123456");



        private void EmanetListeleDetay_Load(object sender, EventArgs e)
        {
         
            baglanti.Open();
            SqlCommand EmanetListeleDetay = new SqlCommand("SELECT * FROM Emanetler INNER JOIN Uyeler ON Emanetler.UyeNo = Uyeler.UyeNo INNER JOIN Kitaplar ON Kitaplar.KitapNo=Emanetler.KitapNo WHERE Emanetler.EmanetNo = @secilenEmanetNo", baglanti);
            EmanetListeleDetay.Parameters.Add("@secilenEmanetNo", SqlDbType.Int).Value = Form1.EmanetListeleSecilenEmanetNo.ToString();
          
            SqlDataReader sonuclar = EmanetListeleDetay.ExecuteReader();
            while (sonuclar.Read())
            {
              labelEmanetNoVeri.Text = sonuclar["EmanetNo"].ToString();
              labelEmanetVermeTarihVeri.Text = sonuclar["EmanetVermeTarih"].ToString();
              labelEmanetAlmaVeri.Text = sonuclar["EmanetGeriAlmaTarih"].ToString();
              labelEmanetIslemTarihVeri.Text = sonuclar["EmanetIslemTarih"].ToString();
              labelEmanetNotVeri.Text = sonuclar["EmanetNot"].ToString();

              labelUyeNoVeri.Text = sonuclar["UyeNo"].ToString();
              labelUyeAdVeri.Text = sonuclar["UyeAd"].ToString();
              labelUyeSoyadVeri.Text = sonuclar["UyeSoyad"].ToString();
              labelUyeTelefonVeri.Text = sonuclar["UyeTelefon"].ToString(); 
              labelUyeEpostaVeri.Text = sonuclar["UyeEposta"].ToString();
              labelUyeAdresVeri.Text = sonuclar["UyeAdres"].ToString();

              labelKitapNoVeri.Text = sonuclar["KitapNo"].ToString();
              labelKitapAdVeri.Text = sonuclar["KitapAd"].ToString();
              labelKitapYazarVeri.Text = sonuclar["KitapYazari"].ToString();
              labelKitapBaskiYilVeri.Text = sonuclar["KitapBaskiYil"].ToString();
              labelKitapSayfaSayiVeri.Text = sonuclar["KitapSayfaSayi"].ToString();
              labelKitapDilVeri.Text = sonuclar["KitapDil"].ToString();
              labelKitapYayinEviVeri.Text = sonuclar["KitapYayinEvi"].ToString();
              labelKitapAciklamaVeri.Text = sonuclar["KitapAciklama"].ToString();
             

            }
         
            baglanti.Close();





        }

        private void buttonKitapTeslimAlindi_Click(object sender, EventArgs e)
        {
            DialogResult sonuc;
            sonuc = MessageBox.Show("Kitabın Alındığını teyit ediyormusunuz?", "Uyarı", MessageBoxButtons.OKCancel);
            if (sonuc == DialogResult.OK)
            {
                try
                {

                    
                    baglanti.Open();
                    SqlCommand EmanetTeslimEdildi = new SqlCommand("UPDATE Emanetler SET EmanetTeslimEdildi='Evet' WHERE EmanetNo = @secilen   ", baglanti);
                    EmanetTeslimEdildi.Parameters.Add("@secilen", SqlDbType.Int).Value = Form1.EmanetListeleSecilenEmanetNo.ToString();
                    EmanetTeslimEdildi.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kitap Teslim Alındı");
                    this.Hide();
                }
                catch (Exception)
                {

                    MessageBox.Show("Bir hata oluştu");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       

       
    }
}
