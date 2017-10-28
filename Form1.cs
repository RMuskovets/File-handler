using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Обробник_файлів
{
    public partial class Form1 : Form
    {
        private string dir = "";
        private Dictionary<string, string> dict;
        public Form1()
        {
            dir = OpenFile();
            InitializeComponent();
            dict = new Dictionary<string, string>();
            table.FullRowSelect = true;
            dict.Add(".doc", "C:\\Users\\Roman\\Documents");
            dict.Add(".txt", "C:\\Users\\Roman\\Documents");
            dict.Add(".docx", "C:\\Users\\Roman\\Documents");
            dict.Add(".xls", "C:\\Users\\Roman\\Documents\\Tables");
            dict.Add(".xlsx", "C:\\Users\\Roman\\Documents\\Tables");
            dict.Add(".png", "C:\\Users\\Roman\\Pictures");
            dict.Add(".jpg", "C:\\Users\\Roman\\Pictures");
            foreach (KeyValuePair<string, string> x in dict)
            {
                string a = x.Key, b = x.Value;
                table.Items.Add(new ListViewItem(new string[] { a, b }));
            }
        }

        private string OpenFile()
        {
            string s = "";
            var ofd = new FolderBrowserDialog();
            DialogResult x = ofd.ShowDialog();
            if (x == DialogResult.OK) s = ofd.SelectedPath;
           
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string x in Directory.GetFiles(dir))
            {
                string[] spl = x.Split(separator: (".".ToCharArray()));
                string extension = spl[spl.Length - 1];
                if (dict.TryGetValue("." + extension, out string Dir))
                {
                    bool a = x.Contains(dir + "\\");
                    var s = Dir + x.Replace(dir, "");
                    File.Replace(x, s, x + ".bkp");
                    File.Delete(x + ".bkp");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListViewItem x = table.SelectedItems[0];
            x.SubItems[1].Text = OpenFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Обробник файлів";
        }
    }
}
