using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UtilForDataCheck
{
    public partial class TipForm : Form
    {
        private Control control = new Control();
        private string ShowText = string.Empty;
        private Form form = new Form();
        public TipForm(Control control_, string ShowText_)
        {
            InitializeComponent();
            control = control_;
            ShowText = ShowText_;
            this.label1.Text = ShowText;
            
        }

        public new void Show()
        {
            this.timerShow.Start();
            this.Size = new Size(ShowText.Length * 14 + 10, 21);
            Point pC = control.PointToScreen(new Point(0, 0));
            this.Location = new Point(pC.X + (control.Width / 3 * 2), pC.Y - 10);
            base.Show();
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
            {
                this.timerShow.Stop();
                this.Close();
            }
        }

    }
}
