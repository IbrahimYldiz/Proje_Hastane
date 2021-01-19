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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string TCnumara;
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            //kullanımı göster
            lblTC.Text=TCnumara;

            //Ad Soyad
            SqlCommand komut1=new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTC=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();
            
            //Branşları DataGride aktarma

            DataTable dt1=new DataTable();
            SqlDataAdapter da=new SqlDataAdapter("Select Bransid as 'Branş Kimliği', BransAd as 'Branş' from  Tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView2.DataSource = dt1;


            //doktor listesi datagrid aktarma
            // as 'görülmesini istediğin başlık' bu şekilde yazıldığında sql sorgusuna ek olarak başlıkları istediğin formatta ayarlayabili yorsun.

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2=new SqlDataAdapter("Select (DoktorAd+' '+DoktorSoyad) as 'Ad Soyad',DoktorBrans as 'Branş' From Tbl_Doktorlar",bgl.baglanti());
            da2.Fill(dt2);
            dataGridView3.DataSource = dt2;

            //branş comboboxa aktarma
            bransgetir();


        }

        void bransgetir()
        {
            SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);

            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
           doktorgetir();
        }

        void doktorgetir()
        {
            cmbDoktor.Text = "Lütfen Doktor Seçiniz.";
            cmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);

            }
            bgl.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut =new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", rchDuyuru.Text);
            komut.ExecuteReader();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPanel drp=new FrmDoktorPanel();
            drp.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frb=new FrmBrans();
            frb.Show();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl=new FrmRandevuListesi();
            frl.Show();
        }

        
        private void btngetir_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("Select RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,HastaTC from Tbl_Randevular where Randevuid=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                mskTarih.Text = dr[0].ToString();
                mskSaat.Text = dr[1].ToString();
                cmbBrans.Text=dr[2].ToString();
                cmbDoktor.Text=dr[3].ToString();
                mskTC.Text = dr[4].ToString();
            }

            txtID.ReadOnly = true;
            bgl.baglanti().Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1=new SqlCommand("update Tbl_Randevular set RandevuTarih=@p2,RandevuSaat=@p3,RandevuBrans=@p4,RandevuDoktor=@p5,HastaTC=@p6 where Randevuid=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", txtID.Text);
            komut1.Parameters.AddWithValue("@p2", mskTarih.Text);
            komut1.Parameters.AddWithValue("@p3", mskSaat.Text);
            komut1.Parameters.AddWithValue("@p4", cmbBrans.Text);
            komut1.Parameters.AddWithValue("@p5", cmbDoktor.Text);
            komut1.Parameters.AddWithValue("@p6", mskTC.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            txtID.ReadOnly = false;

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
           DialogResult result1= MessageBox.Show("Randevuyu Silmek İstediğinizden Emin misiniz?", "Randevu Silme", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result1 == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from Tbl_Randevular where Randevuid=@p1",bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtID.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Randevu Silindi");
            }
            else
            {
                MessageBox.Show("Randevu Silme İşlemi İptal Edildi.");
            }

            txtID.ReadOnly = false;
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtID.ReadOnly = false;
            txtID.Clear();
            mskTarih.Clear();
            mskSaat.Clear();
            cmbBrans.Items.Clear();
            cmbBrans.Text = " ";
            cmbDoktor.Text = " ";
            cmbDoktor.Items.Clear();
            mskTC.Clear();
            bransgetir();
            
        }

        private void btnduyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr=new FrmDuyurular();
            fr.Show();
        }
    }
}
