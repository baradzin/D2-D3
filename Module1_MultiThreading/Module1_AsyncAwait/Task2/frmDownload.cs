using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task2.Database;

namespace Task2
{
    public partial class frmDownload : Form
    {
        WebClient client;
        public string Url { get; set; }
        public string Document { get; set; }
        public double DocumentSize { get; set; }
        public double Percentage { get; set; }
        private MainWindow _frmMain;

        public frmDownload(MainWindow frm)
        {
            InitializeComponent();
            _frmMain = frm;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri(this.Url);
            client.DownloadDataCompleted += Client_DownloadDataCompleted;
            client.DownloadProgressChanged += Client_DownloadProgressChanged1;
            client.DownloadDataAsync(uri);
        }

        private void Client_DownloadProgressChanged1(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Minimum = 0;
            double receive = double.Parse(e.BytesReceived.ToString());
            DocumentSize = double.Parse(e.TotalBytesToReceive.ToString());
            Percentage = receive / DocumentSize * 100;
            labelStatus.Text = $"Downloaded {Percentage}%";
            progressBar.Value = int.Parse(Math.Truncate(Percentage).ToString());
            progressBar.Update();
        }

        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            byte[] raw = e.Result;
            this.Document = Encoding.Default.GetString(raw);
            DataBase.SitesRow row = App.DB.Sites.NewSitesRow();
            row.Url = Url;
            row.Document = Document;
            row.DocumentSize = DocumentSize;
            App.DB.Sites.AddSitesRow(row);
            App.DB.AcceptChanges();
            App.DB.WriteXml($"{Application.StartupPath}/data.dat");
            ListViewItem item = new ListViewItem(row.Id.ToString());
            item.SubItems.Add(row.Url);
            item.SubItems.Add(row.DocumentSize.ToString());
            _frmMain.listView.Items.Add(item);
            this.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            client.CancelAsync();
        }
        
        private void frmDownload_Load(object sender, EventArgs e)
        {
            client = new WebClient();
            textUrl.Text = Url;
        }
    }
}
