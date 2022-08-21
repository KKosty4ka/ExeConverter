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

namespace ExeConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            cmbFormats.SelectedIndex = 0;
            cmbFormats.SelectedItem = "exe -> bat";
        }


        private string OpenFile(string type)
        {
            using (OpenFileDialog d = new OpenFileDialog() )
            {
                d.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                d.Filter = $"{type}|{type}";
                d.DefaultExt = type;
                d.RestoreDirectory = true;

                if (d.ShowDialog() != DialogResult.OK) return null;

                return d.FileName;
            }
        }

        private string SaveFile(string type)
        {
            using (SaveFileDialog d = new SaveFileDialog() )
            {
                d.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                d.Filter = $"{type}|{type}";
                d.DefaultExt = type;
                d.RestoreDirectory = true;

                if (d.ShowDialog() != DialogResult.OK) return null;

                return d.FileName;
            }
        }


        private void btnConvert_Click(object sender, EventArgs e)
        {
            string infile = OpenFile("*.exe");
            if (infile is null) return;
            byte[] input = File.ReadAllBytes(infile);
            
            string output = "";
            string outtype = "";

            if (cmbFormats.SelectedIndex == 0)
            {
                output = ExeConverter.ExeToBat(input);
                outtype = "*.bat";
            }
            else if (cmbFormats.SelectedIndex == 1)
            {
                output = ExeConverter.ExeToVbs(input);
                outtype = "*.vbs";
            }
            else if (cmbFormats.SelectedIndex == 2)
            {
                output = ExeConverter.ExeToJs(input);
                outtype = "*.js";
            }
            else if (cmbFormats.SelectedIndex == 3)
            {
                output = ExeConverter.ExeToPs1(input);
                outtype = "*.ps1";
            }

            string outfile = SaveFile(outtype);
            if (outfile is null) return;
            File.WriteAllText(outfile, output);
        }
    }
}
