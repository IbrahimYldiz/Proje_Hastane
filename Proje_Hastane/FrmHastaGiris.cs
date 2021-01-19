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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr=new FrmHastaKayit();
            fr.Show();
        }
        
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("Select * From Tbl_Hastalar where HastaTC=@p1 and HastaSifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmHastaDetay fr=new FrmHastaDetay();
                fr.tc = mskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC ya da Sifre Girişi!!!");

            }
            bgl.baglanti().Close();

        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Girişler Sayfasına Dönülenecek Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                FrmGirisler fr = new FrmGirisler();
                fr.Show();
                this.Hide();
            }
        }
    }
}
