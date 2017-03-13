using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Serial_Altitude_Simulator
{
    public partial class FormattedNumericUpDown : NumericUpDown
    {
        private string formatString;
        private int floor = 1;

        public FormattedNumericUpDown()
        {
            InitializeComponent();
        }

        protected override void UpdateEditText()
        {
            base.Value = System.Math.Floor(base.Value / floor) * floor;
            Text = System.Convert.ToInt64(base.Value).ToString(FormatString);
        }

        public void SelectAll()
        {
            base.Select(0, base.Text.Length);
        }


        [System.ComponentModel.DefaultValue("")]
        [Description("Format string used to display numeric value"), Category("Appearance")] 
        public string FormatString {
            get {
                return formatString;
            }
            set { 
                formatString = value;
                UpdateEditText();
            }
        }

        [System.ComponentModel.DefaultValue(1)]
        [Description("Round value down to nearest"), Category("Data")]
        public int Floor
        {
            get
            {
                return floor;
            }
            set
            {
                if (value > 0) floor = value;
                UpdateEditText();
            }
        }
    }
}
