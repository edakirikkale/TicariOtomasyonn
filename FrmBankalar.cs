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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

         void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select  Sehir From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbİl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            TxtBankaAd.Text = "";
            TxtHesapNo.Text = "";
            TxtHessapTuru.Text = "";
            TxtIban.Text = "";
            TxtId.Text = "";
            TxtSube.Text = "";
            TxtYetkılı.Text = "";
            MskTarıh.Text = "";
            MskTelefon.Text = "";
            lookUpEdit1.EditValue = 0;
            BtnGüncelle.Enabled = false;
            BtnKaydet.Enabled = true;
            Cmbİl.Text = "";
            Cmbİlce.Text = "";
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();

            sehirlistesi();

            temizle();

            firmalistesi();
        }

        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID ,AD From TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
           // lookUpEdit1.Properties.NullText = "Lütfen bir Ad Seçiniz :";
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //if (lookUpEdit1.Text.Length == 0)
            //{
            //    ep.SetError(lookUpEdit1, "Lütfen Firma Seçimi Yapınız...");
            //    lookUpEdit1.Focus();
            //    return;
            //}
            //if (MskTarıh.Text.Length == 0)
            //{
            //    ep.SetError(MskTarıh, "Lütfen Tarih Alanını Boş Geçmeyiniz");
            //    MskTarıh.Focus();
            //    return;
            //}
            SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR(BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", Cmbİl.Text);
            komut.Parameters.AddWithValue("@p3", Cmbİlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkılı.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTarıh.Text);
            komut.Parameters.AddWithValue("@p10", TxtHessapTuru.Text);
           komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            listele();
            MessageBox.Show("Banka Bilgisi Sistemde Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Cmbİl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbİlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ilce from TBL_ILCELER where sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbİl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbİlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            

            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtBankaAd.Text = dr["BANKAADI"].ToString();
                Cmbİl.Text = dr["IL"].ToString();
                Cmbİlce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                TxtIban.Text = dr["IBAN"].ToString();
                TxtHesapNo.Text = dr["HESAPNO"].ToString();
                TxtYetkılı.Text = dr["YETKILI"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTarıh.Text = dr["TARIH"].ToString();
                TxtHessapTuru.Text = dr["HESAPTURU"].ToString();            
                lookUpEdit1.Text = dr["AD"].ToString();            
                BtnGüncelle.Enabled = true;
                BtnKaydet.Enabled = false;
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Banka Bilgisi Silinecektir. Onaylıyor musunuz?","Sil Onay",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from TBL_BANKALAR where ID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtId.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                temizle();
                MessageBox.Show("Banka Bilgisi Sistemden Silindi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listele();
            }          
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            if (lookUpEdit1.Text.Length==0)
            {
                ep.SetError(lookUpEdit1,"Lütfen Firma Seçimi Yapınız...");
                lookUpEdit1.Focus();
                return;
            }
            //if (MskTarıh.Text.Length == 0)
            //{
            //    ep.SetError(lookUpEdit1, "Lütfen Tarih Alanını Boş Geçmeyiniz");
            //    MskTarıh.Focus();
            //    return;
            //}
            SqlCommand komut = new SqlCommand("update TBL_BANKALAR set BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11  WHERE ID=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", Cmbİl.Text);
            komut.Parameters.AddWithValue("@p3", Cmbİlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkılı.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTarıh.Text);
            komut.Parameters.AddWithValue("@p10", TxtHessapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p12", TxtId.Text);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti();
            temizle();
            MessageBox.Show("Banka Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            

        }

        private void lookUpEdit1_KeyDown(object sender, KeyEventArgs e)
        {
          //  e.Handled = true;
        }
    }
}
