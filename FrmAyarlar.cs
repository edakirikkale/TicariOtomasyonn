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

namespace Ticari_Otomasyon
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
      

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            TxtKullanıcıad.Text = "";
            TxtSıfre.Text = "";
        }

        private void BtnIslem_Click(object sender, EventArgs e)
        {
            if(BtnIslem.Text=="KAYDET")
            {
                ep.Clear();
                if (TxtKullanıcıad.Text.Trim().Length == 0)
                {
                    ep.SetError(TxtKullanıcıad, "Kullanıcı Adı Alanı Boş Geçilemez !!");
                    TxtKullanıcıad.Focus();
                    return;
                }
                ep.Clear();
                if (TxtSıfre.Text.Trim().Length == 0)
                {
                    ep.SetError(TxtSıfre, "Şifre Alanı Boş Geçilemez !!");
                    TxtSıfre.Focus();
                    return;
                }
                SqlCommand komut = new SqlCommand("insert into TBL_ADMIN values(@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtKullanıcıad.Text);
                komut.Parameters.AddWithValue("@p2", TxtSıfre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                listele();
            }
            if(BtnIslem.Text=="GÜNCELLE")
            {
                ep.Clear();
                if (TxtSıfre.Text.Trim().Length == 0)
                {
                    ep.SetError(TxtSıfre, "Şifre Alanı Boş Geçilemez !!");
                    TxtSıfre.Focus();
                    return;
                }
                SqlCommand komut1 = new SqlCommand("update TBL_ADMIN set SIFRE=@P2 WHERE KULLANICIAD=@P1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", TxtKullanıcıad.Text);
                komut1.Parameters.AddWithValue("@p2", TxtSıfre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Temizle();
                listele();
            }
        }
        private void Temizle()
        {
           
            TxtKullanıcıad.Text = "";
            TxtSıfre.Text = "";
            BtnIslem.Text = "KAYDET";
            TxtKullanıcıad.Enabled = true;
            TxtKullanıcıad.Focus();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            //if(dr !=null)
            //{
            //    TxtKullanıcıad.Text = dr["KULLANICIAD"].ToString();
            //    TxtSıfre.Text= dr["SIFRE"].ToString();
            //} 
        }

        private void TxtKullanıcıad_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                TxtKullanıcıad.Enabled = false;
                TxtKullanıcıad.Text = dr["KULLANICIAD"].ToString();
                TxtSıfre.Text = dr["SIFRE"].ToString();
            }

            if (TxtKullanıcıad.Text != "")
            {
                BtnIslem.Text = "GÜNCELLE";
                BtnIslem.BackColor = Color.Gold;
                
            }

            else
            {
                BtnIslem.Text = "KAYDET"; ;
                BtnIslem.BackColor = Color.LightCyan;
                TxtKullanıcıad.Enabled = false;
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
