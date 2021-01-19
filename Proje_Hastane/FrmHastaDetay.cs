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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        void randevugecmisi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTC=" + lblTC.Text, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public string tc;
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            //ad soyad çekme
            lblTC.Text = tc;
            SqlCommand komut=new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0]+" " + dr[1];
            }
            bgl.baglanti().Close();

            //randevu geçmişini çekme
            randevugecmisi();

            //Branşları Çekme
            SqlCommand komut2=new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Text = "Lütfen Doktor Seçiniz.";
            cmbDoktor.Items.Clear();
            SqlCommand komut3=new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);

            }
            bgl.baglanti().Close();
            
        }

        void aktifrandevular()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuBrans='" + cmbBrans.Text + "'" + "and RandevuDoktor='" + cmbDoktor.Text + "' and RandevuDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            aktifrandevular();
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr=new FrmBilgiDuzenle();
            fr.TCno = lblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Randevu Alınıyor Onaylıyor musunuz?","Onay",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (result==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("update Tbl_Randevular set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where Randevuid=@p3", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", lblTC.Text);
                komut.Parameters.AddWithValue("@p2", rchSikayet.Text);
                komut.Parameters.AddWithValue("@p3", txtid.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Randevu Oluşturuldu");
            }
            else
            {
                MessageBox.Show("Randevu Oluşturulmadı");
            }
            bgl.baglanti().Close();
            
             randevugecmisi();
             aktifrandevular();
        }
    }
}
