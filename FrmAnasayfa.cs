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
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da =new SqlDataAdapter("Select URUNAD ,SUM(ADET) AS 'ADET' FROM TBL_URUNLER GROUP BY URUNAD having sum(adet) <= 20 order by sum(ADET)",bgl.baglanti());
            da.Fill(dt);
            gridControlstoklar.DataSource = dt;
        }

        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select  top 10 TARIH, SAAT, BASLIK From TBL_NOTLAR  order by ID desc", bgl.baglanti());
                da.Fill(dt);
                gridControlajanda.DataSource = dt;
        }
        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareket2", bgl.baglanti());
            da.Fill(dt);
            gridControlFirmahareket.DataSource = dt;
        }
        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select AD,TELEFON1 From TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            gridControlFihrist.DataSource = dt;
        }
        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if (xmloku.Name=="title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }
        private void FrmAnasayfa_Load(object sender, EventArgs e)
        {
            stoklar();

            ajanda();

            FirmaHareketleri();

            fihrist();

            haberler();
            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");
            webBrowser2.Navigate("https://www.google.com.tr/");
        }
    }
}
