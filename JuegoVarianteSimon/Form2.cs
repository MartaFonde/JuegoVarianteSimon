using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoVarianteSimon
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            
            for (int i = 18; i < 100; i++)
            {
                cbEdad.Items.Add(i);
            }
            cbEdad.SelectedIndex = 0;
        }
    }
}
