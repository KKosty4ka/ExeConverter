using System;
using System.IO;
using System.Windows.Forms;

namespace ExeConverter
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm() );
        }
    }
}
