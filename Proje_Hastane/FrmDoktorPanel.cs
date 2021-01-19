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
    public partial class FrmDoktorPanel : Form
    {
        public FrmDoktorPanel()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmDoktorPanel_Load(object sender, EventArgs e)
        {
            getir();
            cmbBrans.Text = "Branş Seçiniz.";
            bransgetir();


        }

        void bransgetir()
        {
            SqlCommand komutCommand=new SqlCommand("Select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr = komutCommand.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void getir()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        void temizle()
        {
            txtAd.Clear();
            txtSifre.Clear();
            txtSoyad.Clear();
            cmbBrans.Text = " ";
            mskTC.Clear();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ekleme İşlemini Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", txtAd.Text);
                komut.Parameters.AddWithValue("@d2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@d3", cmbBrans.Text);
                komut.Parameters.AddWithValue("@d4", mskTC.Text);
                komut.Parameters.AddWithValue("@d5", txtSifre.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Doktor Eklendi");
                temizle();
                getir();
            }
            else
            {
                MessageBox.Show("Doktor Eklenmedi");
            }

            bgl.baglanti().Close();
            
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            lblid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            
            DialogResult result1=MessageBox.Show("Doktoru Silinsin mi?!", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result1==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", mskTC.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Doktor Silinmedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            getir();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Güncellemek İstediğinize Emin misiniz?","Güncelleme İşlemi",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (result1==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d5,DoktorTC=@d4 where Doktorid=@d6", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", txtAd.Text);
                komut.Parameters.AddWithValue("@d2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@d3", cmbBrans.Text);
                komut.Parameters.AddWithValue("@d4", mskTC.Text);
                komut.Parameters.AddWithValue("@d5", txtSifre.Text);
                komut.Parameters.AddWithValue("@d6", lblid.Text);
                MessageBox.Show("Güncelleme İşlemi Yapıldı", "Güncelleme Yapıldı Bilgilendirmesi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                getir();

            }
            else
            {
                MessageBox.Show("Güncelleme İptal Edildi.", "Güncelleme İptal Bilgilendirmesi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            



        }
    }
}
