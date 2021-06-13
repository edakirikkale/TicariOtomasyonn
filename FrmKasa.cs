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
using DevExpress.Charts;

namespace Ticari_Otomasyon
{

    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();


        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;
        }
        void giderler()
        {
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Select * From TBL_GIDERLER", bgl.baglanti());
            da3.Fill(dt3);
            gridControl2.DataSource = dt3;


        }
        public string ad;

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktıfKullanıcı.Text = ad;

            giderler();

            musterihareket();

            firmahareket();
            //toplam tutarı hesaplama
            SqlCommand komut1 = new SqlCommand("select sum(TUTAR)FROM TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKasaToplam.Text = dr1[0].ToString()+ " TL " ;
                
            }
            bgl.baglanti().Close();


            //SON AYIN FATURALARI
            SqlCommand komut2 = new SqlCommand("select (ELEKTRIK +SU+DOGALGAZ+INTERNET+EKSTRA) From TBL_GIDERLER ORDER BY ID ASC", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
             while(dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString() + " TL ";
            }
            bgl.baglanti().Close();

            //son ayın personel maaşları
            SqlCommand komut3 = new SqlCommand("select  MAASLAR FROM TBL_GIDERLER ORDER BY ID ASC", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
               LblPersonelMaasları.Text = dr3[0].ToString() + " TL ";
            }
            bgl.baglanti().Close();

            //TOPLAM MÜŞTERİ SAYISI
            SqlCommand komut4 = new SqlCommand("select COUNT (*) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4= komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayısı.Text = dr4[0].ToString() ;
            }
            bgl.baglanti().Close();

            //toplam firma sayısı
            SqlCommand komut5= new SqlCommand("select COUNT (*) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
               LblFirmaSayısı.Text = dr5[0].ToString() ;
            }
            bgl.baglanti().Close();

            //toplam firma şehir sayısı
            SqlCommand komut6 = new SqlCommand("select COUNT (distinct(IL)) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblSehirSayısı.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam  müşteri şehir sayısı
            SqlCommand komut7 = new SqlCommand("select COUNT (distinct(IL)) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblSehirSayısı2.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();


            //toplam  personel sayısı
            SqlCommand komut8 = new SqlCommand("select COUNT (*) FROM TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
               LblPersonelSayısı.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam  ÜRÜN sayısı
            SqlCommand komut9 = new SqlCommand("select sum(Adet) From TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
               LblStokSayısı.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

            
        }
        int sayac = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if(sayac>0 && sayac<=5)
            {   
                //elektrik
                groupControl10.Text = "Elektrik";
                chartControl1.Series["Aylar"].Points.Clear();
                
                SqlCommand komut10 = new SqlCommand("select top 4 AY, ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));

                }
                bgl.baglanti().Close();
               



            }
            if(sayac>5 && sayac<=10)
            {
                //su
                groupControl10.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();
            
                SqlCommand komut11 = new SqlCommand("select top 4 AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }

            //doğalgaz
            if (sayac > 10 && sayac <= 15)
            {

                groupControl10.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();
                //CHART  CONTROLE SU FATURASI SON 4 AY LİSTELEME
                SqlCommand komut11 = new SqlCommand("select top 4 AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //internet
            if (sayac > 15 && sayac <= 20)
            {

                groupControl10.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();
                //CHART  CONTROLE SU FATURASI SON 4 AY LİSTELEME
                SqlCommand komut11 = new SqlCommand("select top 4 AY,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //EKSTRA
            if (sayac > 20 && sayac <= 25)
            {

                groupControl10.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();
                //CHART  CONTROLE SU FATURASI SON 4 AY LİSTELEME
                SqlCommand komut11 = new SqlCommand("select top 4 AY,Ekstra FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            if(sayac==26)
            {
                sayac = 0;
            }
        }
        int sayac2 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            if (sayac2 > 0 && sayac2 <= 5)
            {
                //elektrik
                groupControl10.Text = "Elektrik";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("select top 4 AY, ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));

                }
                bgl.baglanti().Close();




            }
            if (sayac2 > 5 && sayac2 <= 10)
            {
                //su
                groupControl11.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("select top 4 AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }

            //doğalgaz
            if (sayac2 > 10 && sayac2 <= 15)
            {

                groupControl11.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();
                //CHART  CONTROLE SU FATURASI SON 4 AY LİSTELEME
                SqlCommand komut11 = new SqlCommand("select top 4 AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //internet
            if (sayac2 > 15 && sayac2 <= 20)
            {

                groupControl11.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();
                //CHART  CONTROLE SU FATURASI SON 4 AY LİSTELEME
                SqlCommand komut11 = new SqlCommand("select top 4 AY,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //EKSTRA
            if (sayac2 > 20 && sayac2 <= 25)
            {

                groupControl11.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();
                //CHART  CONTROLE SU FATURASI SON 4 AY LİSTELEME
                SqlCommand komut11 = new SqlCommand("select top 4 AY,Ekstra FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
