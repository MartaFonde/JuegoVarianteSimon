using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Componente_FormaColor;

namespace JuegoVarianteSimon
{
    public partial class Form1 : Form
    {
        
        List<int> indices = new List<int>();

        public Form1()
        {
            InitializeComponent();
            creaFormaColor();
            numIndices();
            this.panel1.Enabled = true;
        }

        public void creaFormaColor()
        {
            int filas = 5;
            int col = 8;
            int distancia = 10;

            FormaColor fc;
            int x = 10;
            int y = 10;
            int ind = 0;

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    fc = new FormaColor();
                    fc.Location = new Point (x, y);
                    fc.Forma = eForma.Elipse;
                    fc.ForeColor = Color.CadetBlue;
                    fc.Size = new Size(40, 30);
                    fc.Texto = "Acierto";
                    fc.Tag = ind;
                    //fc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.click);
                    //fc.Click += new System.EventHandler(this.click);
                    this.panel1.Controls.Add(fc);
                    x += 40 + distancia;
                    ind++;
                }
                y += 30 + distancia;
                x = 10;
            }
        }

        private void click(object sender, EventArgs e)
        {
            
            FormaColor fc = (FormaColor)sender;
            
            //Rectangle rect = new Rectangle(fc.Location.X, fc.Location.Y, fc.Location.X + fc.Width, fc.Location.Y + fc.Height);
            //if (rect.Contains(e.))
            {
                if (indices.Contains(Convert.ToInt32(fc.Tag)))
                {
                    //MessageBox.Show(fc.Tag.ToString());
                    indices.Remove(Convert.ToInt32(fc.Tag));
                    fc.Forma = eForma.Texto;
                    
                    if (indices.Count == 0)
                    {
                        timer1.Enabled = false;
                        btnInicio.Enabled = true;
                        Form2 f2 = new Form2();

                        if (f2.ShowDialog() == DialogResult.OK)
                        {
                            string nombre = f2.txtNombre.Text;
                            string edad = f2.cbEdad.SelectedItem.ToString();
                            toolTip1.SetToolTip(btnInicio, nombre + " " + edad);
                        }
                    }
                }
            }
        }


        private void numIndices()
        {
            this.Text = "";
            Random r = new Random();
            int n;
            bool correcto;

            for (int i = 0; i < 4; i++)
            {
                correcto = false;
                while (!correcto)
                {
                    n = r.Next(0, 40);
                    if (!indices.Contains(n))
                    {
                        indices.Add(n);
                        correcto = true;
                       // this.Text += n.ToString()+ " - ";
                    }
                }                
            }
            timer1.Enabled = true;
            btnInicio.Enabled = false;
            
        }

        int decimas = 0;
        int cont = 0;
        bool inicio = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            FormaColor co;
            
            if(inicio)
            {
                inicio = false;

                for (int i = 0; i < this.panel1.Controls.Count; i++)
                {
                    if (indices.Contains(i))
                    {
                        co = (FormaColor)this.panel1.Controls[i];
                        co.doClick(sender, e);
                        //co.ForeColor = Color.Black;
                        co.Click += new System.EventHandler(this.click);
                    }
                }
            }            
            
            timer1.Interval = 100;

            cont++;
            if(cont % 10 == 0)
            {
                decimas++;
                lblTiempo.Text = decimas.ToString();
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.panel1.Controls)
            {
                FormaColor co = (FormaColor)c;
                if(co.Forma == eForma.Texto)
                {
                    co.Size = new Size(40, 30);
                    co.Forma = eForma.Elipse;
                    //co.ForeColor = Color.CadetBlue;
                    co.Click -= new System.EventHandler(this.click);
                }
            }
            this.panel1.Enabled = true;
            numIndices();
            decimas = 0;
            cont = 0;
            inicio = true;
        }
    }
}
