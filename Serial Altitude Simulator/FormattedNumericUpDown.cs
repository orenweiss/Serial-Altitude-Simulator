//-----------------------------------------------------------------------
// <copyright file="FormattedNumericUpDown.cs" company="Federal Aviation Administration">
//   Copyright (c) 2017, Federal Aviation Administration. All Rights Reserved.
//
//   Licensed under the Apache License, Version 2.0,
//   you may not use this file except in compliance with one of the Licenses.
// 
//   You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an 'AS IS' BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

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
