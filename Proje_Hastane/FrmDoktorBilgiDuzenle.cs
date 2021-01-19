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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        public string TCNO;

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            getir();
        }

        void getir()
        {
            mskTC.Text = TCNO;
            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[5].ToString();
                lblid.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

        }
        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {

            DialogResult result1 = MessageBox.Show("Değiştirilen Bilgiler Doğru mu?", "Uyarı", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result1 == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorTC=@p4,DoktorSifre=@p5 where Doktorid=@p6", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
                komut.Parameters.AddWithValue("@p4", mskTC.Text);
                komut.Parameters.AddWithValue("@p5", txtSifre.Text);
                komut.Parameters.AddWithValue("@p6", lblid.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi Yapıldı.");
                getir();
            }

            else
            {
                MessageBox.Show("Güncelleme İşlemi Yapılmadı");
            }
            bgl.baglanti().Close();
            
           
        
    }
}}
