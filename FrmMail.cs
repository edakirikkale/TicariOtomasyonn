using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            TxtMailAdres.Text = mail;
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void BtnGönder_Click(object sender, EventArgs e)
        {
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("edakirikkale250@gmail.com", "Eda.2019");
            istemci.Port = 587;
            istemci.Host = "smtp.gmail.com";
            istemci.EnableSsl = true;
            mesajim.To.Add(TxtMailAdres.Text);
            mesajim.From = new MailAddress("edakirikkale250@gmail.com");
            
            mesajim.Subject = TxtKonu.Text;
            mesajim.Body = TxtMesaj.Text;
            istemci.Send(mesajim);
            MessageBox.Show("Mail Gönderildi.","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
           

        }
    }
}
