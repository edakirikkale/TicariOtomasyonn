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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void BtnGiris_MouseHover(object sender, EventArgs e)
        {
            BtnGiris.BackColor = Color.Green;
        }

        private void BtnGiris_MouseLeave(object sender, EventArgs e)
        {
            BtnGiris.BackColor = Color.Red;
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From TBL_ADMIN where KULLANICIAD=@P1 and SIFRE=@P2", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxKullanıcıAdı.Text);
            komut.Parameters.AddWithValue("@P2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
           if(dr.Read())
            {
                FrmAnaModul fr = new FrmAnaModul();
                fr.kullanici = TxKullanıcıAdı.Text;
                fr.Show();
                this.Hide();

            }
           else
            {

                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            bgl.baglanti().Close();
        }
    }
}
