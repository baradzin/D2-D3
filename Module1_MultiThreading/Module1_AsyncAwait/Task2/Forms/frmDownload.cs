using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task2.Database;

namespace Task2
{
    public partial class frmDownload : Form
    { 
        public string Url { get; set; }
        public string Path { get; set; }
        public double DocumentSize { get; set; }
        public double Percentage { get; set; }
        public static object lockObject = new object();
        private MainWindow _frmMain;
        private CancellationTokenSource _cancellationTokenSource;

        public frmDownload(MainWindow frm)
        {
            InitializeComponent();
            _frmMain = frm;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri(this.Url);
            _cancellationTokenSource = new CancellationTokenSource();
            Path = FileNameHelper.GetFileName(uri, Application.StartupPath);
            var client = new HttpClientDownloadWithProgress(App.Client, Url, Path, _cancellationTokenSource.Token);
            client.ProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;

            try {
                await client.StartDownload();
            } catch (TaskCanceledException) {
                if (File.Exists(Path)) {
                    File.Delete(Path);
                }
            }
        }

        private void Client_DownloadProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage)
        {
            Percentage = progressPercentage ?? 0;
            labelStatus.Text = $"Downloaded {Percentage}%";
            progressBar.Value = int.Parse(Math.Truncate(Percentage).ToString());
            progressBar.Update();
        }

        private void Client_DownloadFileCompleted(long? fileSize)
        {
            lock (lockObject) {
                DataBase.SitesRow row = App.DB.Sites.NewSitesRow();
                row.Url = Url;
                row.Path = Path;
                row.FileSize = fileSize ?? -1;
                App.DB.Sites.AddSitesRow(row);
                App.DB.AcceptChanges();
                App.DB.WriteXml($"{Application.StartupPath}/data.dat");
                ListViewItem item = new ListViewItem(row.Id.ToString());
                item.SubItems.Add(row.Url);
                item.SubItems.Add(row.Path);
                item.SubItems.Add(row.FileSize.ToString());
                _frmMain.listView.Items.Add(item);
            }
            
            this.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
            this.Close();
        }
        
        private void frmDownload_Load(object sender, EventArgs e)
        {
            textUrl.Text = Url;
        }
    }
}
