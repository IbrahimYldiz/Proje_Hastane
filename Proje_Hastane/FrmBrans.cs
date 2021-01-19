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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();

        private void FrmBrans_Load(object sender, EventArgs e)
        {
            getir();

        }

        void getir()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }

        void temizle()
        {
            txtAd.Clear();
            txtID.Clear();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
           DialogResult result= MessageBox.Show("Kaydetmek İstediğinizden Emin misiz?", "Kayıt Onay", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (result==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@b1)", bgl.baglanti());
                komut.Parameters.AddWithValue("@b1", txtAd.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Branş Kayıt Edildi", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getir();
                temizle();
            }
            else
            {
                MessageBox.Show("Kayıt İşlemi İptal Edildi", "İptal Bilgilendirme", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result1=MessageBox.Show(txtAd.Text + " Branşını Silmek İstediğinizden Emin misiniz?", "Silme Onayı",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result1==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from Tbl_Branslar where Bransid=@b1", bgl.baglanti());
                komut.Parameters.AddWithValue("@b1", txtID.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show(txtAd.Text + " Branşı Silindi", "Silinme Bilgilendirmesi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                getir();
            }
            else
            {
                MessageBox.Show("Silme İşlemi İptal Edildi", "İptal Bilgilendirmesi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            DialogResult result2 = MessageBox.Show("Güncellemek İstediğinizden Emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result2==DialogResult.Yes)
            {
                SqlCommand komut=new SqlCommand("Update Tbl_Branslar set BransAd=@b1 where Bransid=@b2", bgl.baglanti());
                komut.Parameters.AddWithValue("@b1", txtAd.Text);
                komut.Parameters.AddWithValue("@b2", txtID.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                getir();
                MessageBox.Show(txtAd.Text + " Branşı Güncellendi");
            }
            else
            {
                MessageBox.Show("Güncelleme İşlemi İptal Edildi");
            }
        }
    }
}
