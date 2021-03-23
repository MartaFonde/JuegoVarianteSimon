using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Componente_FormaColor
{
    public enum eForma
    {
        Elipse,
        Rectangulo, 
        Texto
    }


    [
        DefaultEvent("CambiaForma")
    ]
    public partial class FormaColor : UserControl
    {
        [Category("Examen")]
        [Description("Texto")]
        public string Texto
        {
            set
            {
                label1.Text = value;
            }
            get
            {
                return label1.Text;
            }
        }

        private eForma forma;

        [Category("Examen")]
        [Description("Forma")]
        public eForma Forma
        {
            set
            {
                //if(Enum.IsDefined(typeof(eForma), value)) -- No es necesario comprobar +
                {
                    forma = value;                   

                    if (forma == eForma.Texto)
                    {
                        if(Texto == null || Texto == "")
                        {
                            throw new ArgumentException();
                        }
                        else
                        {
                            label1.Visible = true;
                            this.Size = new Size(label1.Width, label1.Height);
                            label1.Location = new Point(0, 0);
                        }                        
                    }
                    else if (forma == eForma.Elipse || forma == eForma.Rectangulo)
                    {
                        label1.Visible = false;                   
                    }
                    
                    CambiaForma?.Invoke(this, EventArgs.Empty);
                    Refresh();
                }
            }
            get
            {
                return forma;
            }
        }

        [Category("Examen")]
        [Description("Cambia forma")]
        public event System.EventHandler CambiaForma;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush b = new SolidBrush(this.ForeColor);

            switch (Forma)
            {
                case eForma.Elipse:
                    e.Graphics.FillEllipse(b, 0, 0, this.Width, this.Height);
                    break;
                case eForma.Rectangulo:
                    e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
                    break;
            }

            b.Dispose();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Forma == eForma.Elipse)
            {
                Forma = eForma.Rectangulo;
                Thread.Sleep(100);
                Forma = eForma.Elipse;
            }
        }

        public void doClick(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        public FormaColor()
        {
            InitializeComponent();
        }
    }
}
