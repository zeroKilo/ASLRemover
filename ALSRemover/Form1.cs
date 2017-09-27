using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALSRemover
{
    public partial class Form1 : Form
    {
        byte[] mem;
        string name;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.exe;*.dll|*.exe;*.dll";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                button2.Enabled = false;
                mem = File.ReadAllBytes(d.FileName);
                int off = BitConverter.ToInt32(mem, 0x3C);
                off += 0x5E;
                int flags = BitConverter.ToInt32(mem, off);
                if ((flags & 0x40) != 0)
                {
                    mem[off] &= 0xBF;
                    button2.Enabled = true;
                    name = Path.GetFileName(d.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.exe;*.dll|*.exe;*.dll";
            d.FileName = name;
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllBytes(d.FileName, mem);
                MessageBox.Show("Done.");
            }
        }
    }
}
