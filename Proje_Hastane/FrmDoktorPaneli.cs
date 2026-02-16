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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGriddrp.DataSource = dt1;


            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", txtad.Text);
            komut.Parameters.AddWithValue("@d2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@d3", cmbbrans.Text);
            komut.Parameters.AddWithValue("@d4", msktc.Text);
            komut.Parameters.AddWithValue("@d5", txtsifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        

        private void dataGriddrp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGriddrp.SelectedCells[0].RowIndex;
            txtad.Text = dataGriddrp.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGriddrp.Rows[secilen].Cells[2].Value.ToString();
            cmbbrans.Text = dataGriddrp.Rows[secilen].Cells[3].Value.ToString();
            msktc.Text = dataGriddrp.Rows[secilen].Cells[4].Value.ToString();
            txtsifre.Text = dataGriddrp.Rows[secilen].Cells[5].Value.ToString();

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Delete From Tbl_Doktorlar Where DoktorTC=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", msktc.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi.");

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@d1, DoktorSoyad = @d2,DoktorBrans=@d3,DoktorSifre=@d5 where DoktorTC = @d4",bgl.baglanti());
            komut2.Parameters.AddWithValue("@d1", txtad.Text);
            komut2.Parameters.AddWithValue("@d2", txtsoyad.Text);
            komut2.Parameters.AddWithValue("@d3", cmbbrans.Text);
            komut2.Parameters.AddWithValue("@d4", msktc.Text);
            komut2.Parameters.AddWithValue("@d5", txtsifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi.");
        }
    }
}
