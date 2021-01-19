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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("Select * From Tbl_Sekreter where SekreterTC=@p1 and SekreterSifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmSekreterDetay frs=new FrmSekreterDetay();
                frs.TCnumara = mskTC.Text;
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("T.C. Kimlik Numarası ya da Şifre Hatalı","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                mskTC.Focus();
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
