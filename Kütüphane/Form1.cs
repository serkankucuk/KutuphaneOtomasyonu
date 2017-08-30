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


    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
           }
        
        SqlConnection baglanti =
   new SqlConnection(@"Data Source=NB-SERKAN\SQLEXPRESS;Initial Catalog=Kutuphane;User ID=sa;Password=123456");


        private void buttonKitapEkle_Click(object sender, EventArgs e)
        {
            
            try 
	{
         SqlCommand komut = new SqlCommand("INSERT INTO Kitaplar (KitapAd,KitapYazari,KitapBaskiYil,KitapSayfaSayi,KitapDil,KitapYayinEvi,KitapAciklama) VALUES (@KitapAd,@KitapYazari,@KitapBaskiYil,@KitapSayfaSayi,@KitapDil,@KitapYayinEvi,@KitapAciklama)", baglanti);
                  komut.Parameters.Add("@KitapAd", SqlDbType.NVarChar).Value = textBoxKitapEkleAd.Text;
                  komut.Parameters.Add("@KitapYazari", SqlDbType.NVarChar).Value = comboBoxKitapEkleYazar.Text;
                  komut.Parameters.Add("@KitapBaskiYil", SqlDbType.Int).Value = numericUpDownKitapEkleBaskiYil.Value;
                  komut.Parameters.Add("@KitapSayfaSayi", SqlDbType.Int).Value = numericUpDownKitapEkleSayfaSayi.Value;
                  komut.Parameters.Add("@KitapDil", SqlDbType.NVarChar).Value = comboBoxKitapEkleDil.Text;
                  komut.Parameters.Add("@KitapYayinEvi", SqlDbType.NVarChar).Value = comboBoxKitapEkleYayinEvi.Text;
                  komut.Parameters.Add("@KitapAciklama", SqlDbType.Text).Value = textBoxKitapEkleAciklama.Text;
       
               
       
      baglanti.Open();
      komut.ExecuteNonQuery();
      MessageBox.Show("Kitap Başarıyla Eklendi");
      textBoxKitapEkleAd.Text = null;
      numericUpDownKitapEkleBaskiYil.Value = 2015;
      numericUpDownKitapEkleSayfaSayi.Value = 300;
      comboBoxKitapEkleDil.Text = "Türkçe";
      comboBoxKitapEkleYayinEvi.Text = "Bilinmiyor";
      textBoxKitapEkleAciklama.Text = null;


      
       baglanti.Close();
	}
	catch (Exception)
	{

        MessageBox.Show("Bir Hata Oluştu");
	}

                
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            groupBoxKitapEkle.Visible = false;
            groupBoxKitapListele.Visible = false;
            groupBoxUyeEkle.Visible = false;
            groupBoxUyeListele.Visible = false;
            groupBoxEmanetVer.Visible = false;
            groupBoxEmanettekiKitaplarListele.Visible = false;
            
        }

        private void kitapEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           groupBoxKitapEkle.Visible = true;
           groupBoxKitapListele.Visible = false;
           groupBoxUyeListele.Visible = false;
           groupBoxUyeEkle.Visible = false;
           groupBoxEmanetVer.Visible = false;
           groupBoxEmanettekiKitaplarListele.Visible = false;
           
            
           buttonGirisKitapEkle.Visible = false;
           buttonGirisKitapListele.Visible = false;
           buttonGirisEmanetEkle.Visible = false;
           buttonGirisEmanetListele.Visible = false;
           buttonGirisUyeEkle.Visible = false;
           buttonGirisUyeListele.Visible = false;
                

           #region ComboboxDoldur 

           comboBoxKitapEkleDil.Items.Clear();
           comboBoxKitapEkleYayinEvi.Items.Clear();
           comboBoxKitapEkleYazar.Items.Clear();
           List<string> KitapYayinEvleri = new List<string>();
           List<string> KitapDil = new List<string>();
           List<string> KitapYazar = new List<string>();
          
          
         
           baglanti.Open();
           SqlCommand komut2 = new SqlCommand("SELECT KitapYayinEvi  FROM kitaplar", baglanti);

           SqlDataReader sonuclar2 = komut2.ExecuteReader();
           while (sonuclar2.Read())
           {
               string gelendeger = sonuclar2["KitapYayinEvi"].ToString();
               if (KitapYayinEvleri.Contains(gelendeger))
               { }
               else
               { KitapYayinEvleri.Add(sonuclar2["KitapYayinEvi"].ToString()); }
           } baglanti.Close();


           for (int i = 0; i < KitapYayinEvleri.Count; i++)
           {
               comboBoxKitapEkleYayinEvi.Items.Add(KitapYayinEvleri[i].ToString());
           }


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

           for (int i = 0; i < KitapDil.Count; i++)
           {
               comboBoxKitapEkleDil.Items.Add(KitapDil[i].ToString());
           }



           baglanti.Open();
           SqlCommand komutKitapYazar = new SqlCommand("SELECT KitapYazari  FROM kitaplar", baglanti);

           SqlDataReader sonuclarKitapYazar = komutKitapYazar.ExecuteReader();
           while (sonuclarKitapYazar.Read())
           {
               string gelendeger = sonuclarKitapYazar["KitapYazari"].ToString();
               if (KitapDil.Contains(gelendeger))
               { }
               else
               { KitapYazar.Add(sonuclarKitapYazar["KitapYazari"].ToString()); }
           } baglanti.Close();

           for (int i = 0; i < KitapYazar.Count; i++)
           {
               comboBoxKitapEkleYazar.Items.Add(KitapYazar[i].ToString());
           }




           #endregion


        }

        private void kitapListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxKitapListele.Visible = true;
            groupBoxKitapEkle.Visible = false;
            groupBoxUyeEkle.Visible = false;
            groupBoxUyeListele.Visible = false;
            groupBoxEmanetVer.Visible = false;
            groupBoxEmanettekiKitaplarListele.Visible = false;


            buttonGirisKitapEkle.Visible = false;
            buttonGirisKitapListele.Visible = false;
            buttonGirisEmanetEkle.Visible = false;
            buttonGirisEmanetListele.Visible = false;
            buttonGirisUyeEkle.Visible = false;
            buttonGirisUyeListele.Visible = false;

         baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT *  FROM kitaplar", baglanti);
         
            
            SqlDataAdapter veri = new SqlDataAdapter(komut);
           
            DataTable dt = new DataTable();
            veri.Fill(dt);
           
            dataGridViewKitapListele.DataSource = dt;
            
            baglanti.Close();

          


            

        }
        public static string KitapListeleSecilen;
        private void dataGridViewKitapListele_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            KitapListeleSecilen = dataGridViewKitapListele.Rows[dataGridViewKitapListele.CurrentRow.Index].Cells["KitapNo"].Value.ToString();
            KitapListeleDuzenle FormKitapListeleDuzenle = new KitapListeleDuzenle();
            FormKitapListeleDuzenle.Show();
        }

     

        private void buttonUyeEkle_Click(object sender, EventArgs e)
        {

            try
            {
                SqlCommand komutUyeEkle = new SqlCommand("INSERT INTO Uyeler (UyeAd,UyeSoyad,UyeTelefon,UyeEposta,UyeAdres) VALUES (@UyeAd,@UyeSoyad,@UyeTelefon,@UyeEposta,@UyeAdres)", baglanti);
                komutUyeEkle.Parameters.Add("@UyeAd", SqlDbType.NVarChar).Value = textBoxUyeEkleAd.Text;
                komutUyeEkle.Parameters.Add("@UyeSoyad", SqlDbType.NVarChar).Value = textBoxUyeEkleSoyad.Text;
                komutUyeEkle.Parameters.Add("@UyeTelefon", SqlDbType.NVarChar).Value = maskedTextBoxUyeEkleTelefon.Text.ToString();
                komutUyeEkle.Parameters.Add("@UyeEposta", SqlDbType.NVarChar).Value = textBoxUyeEkleEposta.Text;
                komutUyeEkle.Parameters.Add("@UyeAdres", SqlDbType.NVarChar).Value = textBoxUyeEkleAdres.Text;

                baglanti.Open();
                komutUyeEkle.ExecuteNonQuery();
                MessageBox.Show("Üye Başarıyla Eklendi");
                baglanti.Close();

                textBoxUyeEkleAd.Text="";
                textBoxUyeEkleSoyad.Text="";
                maskedTextBoxUyeEkleTelefon.Text = "";
                textBoxUyeEkleEposta.Text = "";
                textBoxUyeEkleAdres.Text = "";



            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu");
                
            }

        }


       

        private void UyeListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxEmanetVer.Visible = false;
            groupBoxUyeListele.Visible = true;
            groupBoxKitapListele.Visible = false;
            groupBoxKitapEkle.Visible = false;
            groupBoxUyeEkle.Visible = false;
            groupBoxEmanettekiKitaplarListele.Visible = false;


            buttonGirisKitapEkle.Visible = false;
            buttonGirisKitapListele.Visible = false;
            buttonGirisEmanetEkle.Visible = false;
            buttonGirisEmanetListele.Visible = false;
            buttonGirisUyeEkle.Visible = false;
            buttonGirisUyeListele.Visible = false;

           baglanti.Open();
            SqlCommand UyeListele = new SqlCommand("SELECT *  FROM Uyeler", baglanti);
            SqlDataAdapter veri = new SqlDataAdapter(UyeListele);
            DataTable dt = new DataTable();
            veri.Fill(dt);
            dataGridViewUyeListele.DataSource = dt;
            baglanti.Close();
        }

        private void UyeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxEmanetVer.Visible = false;
            groupBoxKitapEkle.Visible = false;
            groupBoxKitapListele.Visible = false;
            groupBoxUyeListele.Visible = false;
            groupBoxUyeEkle.Visible = true;
            groupBoxEmanettekiKitaplarListele.Visible = false;


            buttonGirisKitapEkle.Visible = false;
            buttonGirisKitapListele.Visible = false;
            buttonGirisEmanetEkle.Visible = false;
            buttonGirisEmanetListele.Visible = false;
            buttonGirisUyeEkle.Visible = false;
            buttonGirisUyeListele.Visible = false;
        }


        public static string UyeListeleSecilen;
        private void dataGridViewUyeListele_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
             
         UyeListeleSecilen = dataGridViewUyeListele.Rows[dataGridViewUyeListele.CurrentRow.Index].Cells["UyeNo"].Value.ToString();

            UyeListeleDuzenle FormUyeListeleDuzenle = new UyeListeleDuzenle();
            FormUyeListeleDuzenle.Show();
        }

        private void emanetVerToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            groupBoxKitapEkle.Visible = false;
            groupBoxKitapListele.Visible = false;
            groupBoxUyeListele.Visible = false;
            groupBoxUyeEkle.Visible = false;
            groupBoxEmanetVer.Visible = true;
            groupBoxEmanettekiKitaplarListele.Visible = false;

            buttonGirisKitapEkle.Visible = false;
            buttonGirisKitapListele.Visible = false;
            buttonGirisEmanetEkle.Visible = false;
            buttonGirisEmanetListele.Visible = false;
            buttonGirisUyeEkle.Visible = false;
            buttonGirisUyeListele.Visible = false;

            dateTimePickerEmanetVerEmanetVermeTarih.Value = DateTime.Today;
            dateTimePickerEmanetVerEmanetAlmaTarih.Value = DateTime.Today.AddDays(1);

           
            
            #region ComboboxUyeListele //Kullanılmıyor
            /*
            SqlCommand komutEmanetVerUyeListele = new SqlCommand("SELECT UyeNo,UyeAd,UyeSoyad,UyeTelefon FROM Uyeler", baglanti);
           baglanti.Open();
            SqlDataReader sonuclarEmanetVerUyeListele = komutEmanetVerUyeListele.ExecuteReader();
            
            while (sonuclarEmanetVerUyeListele.Read() )
            {
                //MessageBox.Show(sonuclarEmanetVerUyeListele["UyeAd"].ToString());
                string UyeBilgi =sonuclarEmanetVerUyeListele["UyeNo"].ToString() + " - " + sonuclarEmanetVerUyeListele["UyeAd"].ToString() + " " + sonuclarEmanetVerUyeListele["UyeSoyad"].ToString() +" TEL: " + sonuclarEmanetVerUyeListele["UyeTelefon"].ToString();
                comboBoxEmanetVerUyeAd.Items.Add(UyeBilgi);

            }
          
            baglanti.Close();*/
            #endregion

            #region ComboboxKitapListele //Kullanılmıyor
              /*
            baglanti.Open();
            SqlCommand komutEmanetVerKitapListele = new SqlCommand("SELECT KitapNo,KitapAd,KitapYazari FROM Kitaplar", baglanti);
            SqlDataReader sonuclarEmanetVerKitapListele = komutEmanetVerKitapListele.ExecuteReader();

            while (sonuclarEmanetVerKitapListele.Read())
            {
               string KitapBilgi = sonuclarEmanetVerKitapListele["KitapNo"].ToString() + " - " + sonuclarEmanetVerKitapListele["KitapAd"].ToString() + " - " + sonuclarEmanetVerKitapListele["KitapYazari"].ToString();
               comboBoxEmanetVerKitapAd.Items.Add(KitapBilgi); 
            }
            comboBoxEmanetVerUyeAd.SelectedIndex = 0;
            comboBoxEmanetVerKitapAd.SelectedIndex = 0;
            */
            #endregion  
             
            #region ComboboxUyeListeleDTile
            SqlDataAdapter adp = new SqlDataAdapter("SELECT UyeNo,(UyeAd + ' ' + UyeSoyad + ' TEL: ' + UyeTelefon ) AS UyeBilgi FROM Uyeler ORDER BY UyeAd ASC", baglanti);
            DataTable ComboboxUyeAd = new DataTable();
            adp.Fill(ComboboxUyeAd);
           
            comboBoxEmanetVerUyeAd.DataSource = ComboboxUyeAd;
           
            comboBoxEmanetVerUyeAd.DisplayMember = "UyeBilgi";// Combobox ta görünecek olan hücre
            comboBoxEmanetVerUyeAd.ValueMember = "UyeNo"; // Arka Planda tutulacak olan hücre
            #endregion
            #region ComboboxKitapListeleDTile
            SqlDataAdapter EmanetKitapListele = new SqlDataAdapter("SELECT KitapNo,(KitapAd + ' - ' + KitapYazari  + ' - ' + KitapYayinEvi ) AS KitapBilgi FROM Kitaplar ORDER BY KitapAd ASC", baglanti);
            DataTable ComboboxKitapAd = new DataTable();
            EmanetKitapListele.Fill(ComboboxKitapAd);

            comboBoxEmanetVerKitapAd.DataSource = ComboboxKitapAd;

            comboBoxEmanetVerKitapAd.DisplayMember = "KitapBilgi";// Combobox ta görünecek olan hücre
            comboBoxEmanetVerKitapAd.ValueMember = "KitapNo"; // Arka Planda tutulacak olan hücre
            #endregion

            

        }

        private void buttonEmanetVer_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show(comboBoxEmanetVerUyeAd.SelectedValue.ToString());
          //  MessageBox.Show(comboBoxEmanetVerKitapAd.SelectedValue.ToString());
            
            try 
	{
        DateTime dtime1 =dateTimePickerEmanetVerEmanetVermeTarih.Value;
        DateTime dtime2 = dateTimePickerEmanetVerEmanetAlmaTarih.Value;
        DateTime simdikizaman = DateTime.Now;
        int sonuc = DateTime.Compare(dtime1, dtime2);
      
        if (sonuc ==1)

        {
            MessageBox.Show("Emanet Geri Alma Tarihi Emanet Vermeden Önce Olamaz");
        }

        else {

  
        SqlCommand komut = new SqlCommand("INSERT INTO Emanetler (UyeNo,KitapNo,EmanetVermeTarih,EmanetGeriAlmaTarih,EmanetIslemTarih,EmanetNot) VALUES (@UyeNo,@KitapNo,@EmanetVermeTarih,@EmanetGeriAlmaTarih,@EmanetIslemTarih,@EmanetNot)", baglanti);
                   komut.Parameters.Add("@UyeNo", SqlDbType.Int).Value = comboBoxEmanetVerUyeAd.SelectedValue.ToString();
                    komut.Parameters.Add("@KitapNo", SqlDbType.Int).Value = comboBoxEmanetVerKitapAd.SelectedValue.ToString();
                    komut.Parameters.Add("@EmanetVermeTarih", SqlDbType.DateTime).Value = dateTimePickerEmanetVerEmanetVermeTarih.Value;
                    komut.Parameters.Add("@EmanetGeriAlmaTarih", SqlDbType.DateTime).Value = dateTimePickerEmanetVerEmanetAlmaTarih.Value;
                    komut.Parameters.Add("@EmanetIslemTarih", SqlDbType.DateTime).Value = simdikizaman;
                komut.Parameters.Add("@EmanetNot", SqlDbType.NVarChar).Value = textBoxEmanetVerNot.Text;
                  
      baglanti.Open();
      komut.ExecuteNonQuery();
      MessageBox.Show("Emanet Başarıyla Eklendi");
     
       baglanti.Close();

        }
      
	   
	   }
	catch (Exception)
	{

        MessageBox.Show("Bir Hata Oluştu");
	}
        }

        private void emanettekiKitaplarıListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxKitapEkle.Visible = false;
            groupBoxKitapListele.Visible = false;
            groupBoxUyeListele.Visible = false;
            groupBoxUyeEkle.Visible = false;
            groupBoxEmanetVer.Visible = false;
            groupBoxEmanettekiKitaplarListele.Visible = true;


            buttonGirisKitapEkle.Visible = false;
            buttonGirisKitapListele.Visible = false;
            buttonGirisEmanetEkle.Visible = false;
            buttonGirisEmanetListele.Visible = false;
            buttonGirisUyeEkle.Visible = false;
            buttonGirisUyeListele.Visible = false;


            
       baglanti.Open();
            SqlCommand komutEmanetleriListele = new SqlCommand("SELECT EmanetNo, UyeAd,UyeSoyad,UyeTelefon,KitapAd,EmanetVermeTarih,EmanetGeriAlmaTarih,EmanetNot FROM Emanetler INNER JOIN Uyeler ON Emanetler.UyeNo = Uyeler.UyeNo INNER JOIN Kitaplar ON Kitaplar.KitapNo=Emanetler.KitapNo WHERE EmanetTeslimEdildi='Hayır'", baglanti);
         
            
            SqlDataAdapter veri = new SqlDataAdapter(komutEmanetleriListele);
           
            DataTable dt = new DataTable();
            veri.Fill(dt);
            dataGridViewEmanetleriListele.DataSource = dt;
            baglanti.Close();
           


            for (int i = 0; i < dataGridViewEmanetleriListele.Rows.Count; i++)
                {
            if (dt.Rows[i]["EmanetGeriAlmaTarih"].ToString() ==DateTime.Today.ToString()) 
            {//Eğer Geri alma tarihi bugin ise
                dataGridViewEmanetleriListele.Rows[i].DefaultCellStyle.BackColor = Color.Red;
               
            }

                }
        }

        public static string EmanetListeleSecilenEmanetNo;
       
        private void dataGridViewEmanetleriListele_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


         EmanetListeleSecilenEmanetNo = dataGridViewEmanetleriListele.Rows[dataGridViewEmanetleriListele.CurrentRow.Index].Cells["EmanetNo"].Value.ToString();
        
         EmanetListeleDetay FormEmanetListeleDetay = new EmanetListeleDetay();
         FormEmanetListeleDetay.Show();

        }

      

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Serkan Küçük - 140662095 \n Bilgisayar Programcılığı UE");
        }

       

      

      

      
       

       

       
   

     

 
    }
}
