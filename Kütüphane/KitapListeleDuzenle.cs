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
    public partial class KitapListeleDuzenle : Form
    {
        public KitapListeleDuzenle()
        {
            InitializeComponent();
        }
 SqlConnection baglanti =
         new SqlConnection(@"Data Source=NB-SERKAN\SQLEXPRESS;Initial Catalog=Kutuphane;User ID=sa;Password=123456");

        private void KitapListeleDuzenle_Load(object sender, EventArgs e)
        {
         List<string> KitapYayinEvleri = new List<string>();
         List<string> KitapDil = new List<string>();

        

            #region KitapYayinEviListeyeAktar
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT KitapYayinEvi  FROM kitaplar", baglanti);

            SqlDataReader sonuclar2 = komut2.ExecuteReader();
            while (sonuclar2.Read())
            {
                string gelendeger = sonuclar2["KitapYayinEvi"].ToString();
                if (KitapYayinEvleri.Contains(gelendeger))
                {                }
                else
                {KitapYayinEvleri.Add(sonuclar2["KitapYayinEvi"].ToString()); }
            }   baglanti.Close();
 #endregion


            #region YayinEvleriComboboxaEkle
            for (int i = 0; i < KitapYayinEvleri.Count; i++)
                {
                comboBoxKitapDuzenleKitapYayinEvi.Items.Add(KitapYayinEvleri[i].ToString());
                }
            #endregion

            #region SecilenKitabiListele
            string secilen = Form1.KitapListeleSecilen.ToString();
        baglanti.Open();
              SqlCommand komut = new SqlCommand("SELECT *  FROM kitaplar WHERE KitapNo = @secilen  ", baglanti);
              komut.Parameters.Add("@secilen", SqlDbType.Int).Value = Form1.KitapListeleSecilen.ToString();
              SqlDataReader sonuclar = komut.ExecuteReader();
              while (sonuclar.Read())
              {
                  labelKitapDuzenleKitapNoSecilen.Text = sonuclar["KitapNo"].ToString();
                  textBoxKitapDuzenleKitapAd.Text = sonuclar["KitapAd"].ToString();
                 numericUpDownKitapDuzenleKitapBaskiYil.Value = Convert.ToInt32(sonuclar["KitapBaskiYil"].ToString()); 
                  numericUpDownKitapDuzenleKitapSayfaSayi.Value = Convert.ToInt32(sonuclar["KitapSayfaSayi"].ToString());
                  comboBoxKitapDuzenleKitapDil.Text = sonuclar["KitapDil"].ToString();
                  comboBoxKitapDuzenleKitapYayinEvi.Text = sonuclar["KitapYayinEvi"].ToString();
                  textBoxKitapDuzenleKitapAciklama.Text = sonuclar["KitapAciklama"].ToString();


              }
              baglanti.Close();


            #endregion



            

              #region KitapDilListeyeAktar
              baglanti.Open();
              SqlCommand komutKitapDil = new SqlCommand("SELECT KitapDil  FROM kitaplar", baglanti);

              SqlDataReader sonuclarKitapDil = komutKitapDil.ExecuteReader();
              while (sonuclarKitapDil.Read())
              {
                  string gelendeger = sonuclarKitapDil["KitapDil"].ToString();
                  if (KitapDil.Contains(gelendeger))
                  { }
                  else
                  { KitapDil.Add(sonuclarKitapDil["KitapDil"].ToString()); }
              } baglanti.Close();
              #endregion


              #region KitapDilleriComboboxaEkle
              for (int i = 0; i < KitapDil.Count; i++)
              {
                  comboBoxKitapDuzenleKitapDil.Items.Add(KitapDil[i].ToString());
              }

              #endregion



        }

        
        private void buttonKitapDuzenleSil_Click(object sender, EventArgs e)
        {
            DialogResult sonuc;
sonuc = MessageBox.Show("Kitabı silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.OKCancel);
if (sonuc == DialogResult.OK)
{
    try
    {
string secilen = Form1.KitapListeleSecilen.ToString();
     
              baglanti.Open();
              SqlCommand KayitSil = new SqlCommand("DELETE FROM kitaplar WHERE KitapNo=@secilen  ", baglanti);
              KayitSil.Parameters.Add("@secilen", SqlDbType.Int).Value = Form1.KitapListeleSecilen.ToString();
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

        private void buttonKitapDuzenleIptal_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonKitapDuzenleKaydet_Click(object sender, EventArgs e)
        {
           
            SqlCommand komut = new SqlCommand("UPDATE Kitaplar  SET KitapAd=@KitapAd,KitapBaskiYil=@KitapBaskiYil,KitapSayfaSayi=@KitapSayfaSayi,KitapDil=@KitapDil,KitapYayinEvi=@KitapYayinEvi,KitapAciklama=@KitapAciklama WHERE KitapNo = @secilen  ", baglanti);
            
              komut.Parameters.Add("@secilen", SqlDbType.Int).Value = Form1.KitapListeleSecilen.ToString();
            komut.Parameters.Add("@KitapAd", SqlDbType.NVarChar).Value = textBoxKitapDuzenleKitapAd.Text;
            komut.Parameters.Add("@KitapBaskiYil", SqlDbType.NChar).Value = numericUpDownKitapDuzenleKitapBaskiYil.Value;
            komut.Parameters.Add("@KitapSayfaSayi", SqlDbType.NChar).Value = numericUpDownKitapDuzenleKitapSayfaSayi.Value;
            komut.Parameters.Add("@KitapDil", SqlDbType.NVarChar).Value = comboBoxKitapDuzenleKitapDil.Text;
            komut.Parameters.Add("@KitapYayinEvi", SqlDbType.NVarChar).Value = comboBoxKitapDuzenleKitapYayinEvi.Text;
            komut.Parameters.Add("@KitapAciklama", SqlDbType.Text).Value = textBoxKitapDuzenleKitapAciklama.Text;



            baglanti.Open();
            komut.ExecuteNonQuery();
            MessageBox.Show("Kitap Düzenlendi");
            baglanti.Close();
            this.Hide();
            
        }

        
    }
}
