using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace OthelloWindowsForm.OthelloControls
{
    public partial class IndicatorControl : PictureBox
    {
        public IndicatorControl()
        {
            InitializeComponent();
        }

        public IndicatorControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private bool _state;
        public bool state
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                BeginInvoke(new Action(() => this.Image = CommonObject.indicatorImage[value]));
            }
        }
    }
}
