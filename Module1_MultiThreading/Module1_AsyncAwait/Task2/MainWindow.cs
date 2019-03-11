using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using(var dialog = new frmAddUrl()) {
                if (dialog.ShowDialog() == DialogResult.OK) {
                    frmDownload frmDownload = new frmDownload(this);
                    frmDownload.Url = dialog.Url;
                    frmDownload.Show();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure want to delete this record?", "Message", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK) {
                for(int i = listView.SelectedItems.Count; i > 0; i--) {
                    ListViewItem item = listView.SelectedItems[i - 1];
                    App.DB.Sites.Rows[item.Index].Delete();
                    listView.Items[item.Index].Remove();
                }
                App.DB.AcceptChanges();
                App.DB.WriteXml($"{Application.StartupPath}/data.dat");
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            string filename = $"{Application.StartupPath}/data.dat";
            if (File.Exists(filename)){
                App.DB.ReadXml(filename);
                foreach(var row in App.DB.Sites) {
                    ListViewItem item = new ListViewItem(row.Id.ToString());
                    item.SubItems.Add(row.Url);
                    item.SubItems.Add(row.DocumentSize.ToString());
                    listView.Items.Add(item);
                }
            }
        }
    }
}
