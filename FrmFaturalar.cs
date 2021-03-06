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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }



        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            TxtAlıcı.Text = "";
            TxtId.Text = "";
            TxtSeri.Text = "";
            TxtSıraNo.Text = "";
            TxtTeslimAlan.Text = "";
            TxtTeslimEden.Text = "";
            TxtVergiDaire.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();

            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
          

            if (TxtFaturaId.Text=="")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI(SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@P2", TxtSıraNo.Text);
                komut.Parameters.AddWithValue("@P3", MskTarih.Text);
                komut.Parameters.AddWithValue("@P4", MskSaat.Text);
                komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
                komut.Parameters.AddWithValue("@P6", TxtAlıcı.Text);
                komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();


            }
            //firmacarisi
            if (TxtFaturaId.Text !="" && comboBox1.Text == "Firma")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();


                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY(URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3",decimal.Parse( TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaId.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();



                //Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_FIRMAHAREKETLER (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) VALUES (@H1,@H2,@H3,@H4,@H5,@H6,@H7,@H8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@H1", TxtUrunId.Text);
                komut3.Parameters.AddWithValue("@H2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@H3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@H4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@H5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@H6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@H7", TxtFaturaId.Text);
                komut3.Parameters.AddWithValue("@H8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya Ait Ürün Bilgisi  Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

                // stok sayısını azaltma
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set ADET=ADET-@S1 where ID=@S2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@S1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@S2", TxtUrunId.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

            }

           
            //if (TxtFaturaId.Text == ""  )
            //{
            //    SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI(SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
            //    komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            //    komut.Parameters.AddWithValue("@P2", TxtSıraNo.Text);
            //    komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            //    komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            //    komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
            //    komut.Parameters.AddWithValue("@P6", TxtAlıcı.Text);
            //    komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            //    komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            //    komut.ExecuteNonQuery();
            //    bgl.baglanti().Close();
            //    MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    listele();


            //}
            // müşteri carisi
            if (TxtFaturaId.Text != "" &&comboBox1.Text == "Müşteri")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();


                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY(URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaId.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();



                //Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_MUSTERİHAREKETLER (URUNID,ADET,PERSONEL,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) VALUES (@H1,@H2,@H3,@H4,@H5,@H6,@H7,@H8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@H1", TxtUrunId.Text);
                komut3.Parameters.AddWithValue("@H2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@H3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@H4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@H5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@H6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@H7", TxtFaturaId.Text);
                komut3.Parameters.AddWithValue("@H8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya Ait Ürün Bilgisi  Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

                // stok sayısını azaltma
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set ADET=ADET-@S1 where ID=@S2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@S1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@S2", TxtUrunId.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

            }


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["FATURABILGIID"].ToString();
                TxtSıraNo.Text = dr["SIRANO"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
                TxtAlıcı.Text = dr["ALICI"].ToString();
                TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
                TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
            }    }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FATURABILGI where FATURABILGIID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
            listele();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FATURABILGI set SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 WHERE FATURABILGIID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@P2", TxtSıraNo.Text);
            komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlıcı.Text);
            komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@P9", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr!=null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
                fr.Show();
            }
        }

        private void TxtSıraNo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select URUNAD,SATISFIYAT From TBL_URUNLER  where ID=@P1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunId.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtUrunAd.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();

            }
            bgl.baglanti().Close();
        }
    }
}
