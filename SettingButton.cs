using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Холст_для_QR
{
    public partial class SettingButton : Button
    {
        public int Order { get; set; }
        public bool Active { get; set; }
        public Panel Panel { get; set; }
        public SettingButton()
        {
            InitializeComponent();

        }
    }
}
