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

namespace Proje_Hastane
{
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void btnKayitYap_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Kaydınızı Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (result==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Hastalar (HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values (@had,@hsoy,@hastc,@hastel,@hsifre,@p6)", bgl.baglanti());


                komut.Parameters.AddWithValue("@had", txtAd.Text);
                komut.Parameters.AddWithValue("@hsoy", txtSoyad.Text);
                komut.Parameters.AddWithValue("@hastc", mskTC.Text);
                komut.Parameters.AddWithValue("@hastel", mskTelefon.Text);
                komut.Parameters.AddWithValue("@hsifre", txtSifre.Text);
                komut.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                if (cmbCinsiyet.Text == "Erkek")
                {
                    MessageBox.Show("Kaydınız Gerçekleşmiştir. Sağlıklı Günler Dileriz." + txtAd.Text);
                }

                if (cmbCinsiyet.Text == "Kadın")
                {
                    MessageBox.Show("Kaydınız Gerçekleşmiştir. Sağlıklı Günler Dileriz." + txtAd.Text);
                }
            }
            else
            {
                MessageBox.Show("Kayıt Yapılmadı");
            }

        }

       
    }
}
