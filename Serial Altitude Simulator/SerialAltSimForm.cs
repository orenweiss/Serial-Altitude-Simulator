//-----------------------------------------------------------------------
// <copyright file="SerialAltSimForm.cs" company="Federal Aviation Administration">
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
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Serial_Altitude_Simulator
{
    public partial class SerialAltSimForm : Form
    {
        public SerialAltSimForm()
        {
            InitializeComponent();
            initializeProtocol();

            scanForPorts();
        }

        private void initializeProtocol()
        {
            this.currentAltitudeValue = 0;

            this.altitudeProtocols.Add(ProtocolType.ICARUS_GARMIN_TRIMBLE, new AltitudeProtocol {
                name = "ICARUS Format: Trimble / Garmin",
                stopBits = StopBits.One,
                parity = Parity.None,
                baudRate = 9600,
                dataBits = 8,
                checksum = false });
            this.altitudeProtocols.Add(ProtocolType.UPSAT_GARMIN, new AltitudeProtocol {
                name = "UPSAT Apollo / Garmin AT / IIMorrow",
                stopBits = StopBits.One,
                parity = Parity.None,
                baudRate = 1200,
                dataBits = 8,
                checksum = true });
            this.altitudeProtocols.Add(ProtocolType.NORTHSTAR, new AltitudeProtocol {
                name = "Northstar / Garmin",
                stopBits = StopBits.One,
                parity = Parity.Odd,
                baudRate = 9600,
                dataBits = 8,
                checksum = false });
            this.altitudeProtocols.Add(ProtocolType.UPS_AT_618_LORAN, new AltitudeProtocol {
                name = "UPS AT 618 Loran Devices (IIMorrow)",
                stopBits = StopBits.One,
                parity = Parity.Odd,
                baudRate = 1200,
                dataBits = 7,
                checksum = true });
            this.altitudeProtocols.Add(ProtocolType.MAGELLAN, new AltitudeProtocol {
                name = "Magellan",
                stopBits = StopBits.One,
                parity = Parity.Even,
                baudRate = 1200,
                dataBits = 7,
                checksum = true });

            foreach(AltitudeProtocol a in altitudeProtocols.Values)
            {
                protocolComboBox.Items.Add(a.name);
            }
        }

        private void scanForPorts()
        {
            openButton.Enabled = false;
            comPortsComboBox.Items.Clear();
            comPortsComboBox.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());

            if (comPortsComboBox.Items.Count > 0) {
                comPortsComboBox.SelectedIndex = 0;
                openButton.Enabled = true;
            }
        }

        private void resetComPort()
        {
            closeButton.Enabled = false;
            openButton.Enabled = true;
            comPortsComboBox.Enabled = true;
            protocolComboBox.Enabled = true;
        }

        private string calcChecksum(string alt)
        {
            int b = 0;
            foreach (char c in alt)
            {
                b = (c + b) % 256;
            }
            return (b).ToString("X2");
        }

        private string formatAltitude(decimal altitudeValue, ProtocolType protocol)
        {
            string alt = "";

            switch (protocol)
            {
                case ProtocolType.ICARUS_GARMIN_TRIMBLE: // ICARUS_Trimble/Garmin
                    // Sample: "ALT 00800\r" 
                    return "ALT " + ((altitudeValue < 0) ? altitudeValue.ToString("0000") : altitudeValue.ToString("00000")) + "\r";

                case ProtocolType.UPSAT_GARMIN: // UPSAT/Garmin AT/IIMorrow/Dynon Encoder
                    // Sample: "#AL +05200T+25D7\r"
                    alt = "#AL " + ((altitudeValue >= 0) ? "+" : "") + altitudeValue.ToString("00000") + "T+25";
                    return alt + calcChecksum(alt) + "\r";

                case ProtocolType.NORTHSTAR: // Northstar
                    // Sample: "ALT 02500\r"
                    return "ALT " + ((altitudeValue < 0) ? altitudeValue.ToString("0000") : altitudeValue.ToString("00000")) + "\r";

                case ProtocolType.UPS_AT_618_LORAN: // UPS AT 618 Loran 
                    alt = "#AL " + ((altitudeValue >= 0) ? "+" : "") + altitudeValue.ToString("00000") + "T+25"; 
                    return alt + calcChecksum(alt) + "\r";

                case ProtocolType.MAGELLAN: // Magellan
                    // Sample: "$MGL+02500T+25D6\r"
                    alt = "$MGL" + ((altitudeValue >= 0) ? "+" : "") + altitudeValue.ToString("00000") + "T+25";
                    return alt + calcChecksum(alt) + "\r";

                case ProtocolType.SHADIN_RMS: // SHADIN_RMS RMS
                    // Sample: "RMS +02500T+251B\r"
                    alt = "RMS " + ((altitudeValue >= 0) ? "+" : "") + altitudeValue.ToString("00000") + "T+25";
                    return alt + calcChecksum(alt) + "\r";
            }
            return alt;
        }



        private void transmitTimer_Tick(object sender, EventArgs e)
        {
            string alt = formatAltitude(this.currentAltitudeValue, (ProtocolType)protocolComboBox.SelectedIndex);

            try
            {
                serialPort.Write(alt);
            }
            catch (System.IO.IOException err)
            {
                MessageBox.Show(err.Message.ToString());
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }

        private void SerialAltSimForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            transmitTimer.Stop();
            resetComPort();

            try
            {
                serialPort.Close();
            }
            catch(System.IO.IOException err)
            {
                MessageBox.Show(err.Message.ToString());
            }
            catch(System.Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                Application.Exit();
            }
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            resetComPort();
            transmitTimer.Stop();
            serialPort.Close();
        }


        private void openButton_Click(object sender, EventArgs e)
        {
            closeButton.Enabled = true;
            openButton.Enabled = false;
            comPortsComboBox.Enabled = false;
            protocolComboBox.Enabled = false;

            this.selectedProtocol = altitudeProtocols[(ProtocolType)protocolComboBox.SelectedIndex];

            serialPort.BaudRate = selectedProtocol.baudRate;
            serialPort.DataBits = selectedProtocol.dataBits;
            serialPort.StopBits = selectedProtocol.stopBits;
            serialPort.Parity = selectedProtocol.parity;
 
            try
            {
                serialPort.PortName = comPortsComboBox.SelectedItem.ToString();
                serialPort.Open();
                transmitTimer.Start();
            }
            catch (System.IO.IOException err)
            {
                MessageBox.Show(err.Message.ToString());

                resetComPort();
                transmitTimer.Stop();
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message.ToString());

                resetComPort();
                transmitTimer.Stop();
            }
        }

        private void altitudeSelector_MouseUp(object sender, MouseEventArgs e)
        {
            altitudeSelector.SelectAll();
        }


        private void comPortsComboBox_DropDown(object sender, EventArgs e)
        {
            openButton.Enabled = false;
            comPortsComboBox.Items.Clear();

            foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
            {
                comPortsComboBox.Items.Add(com);
            }
        }

        private void serialPort_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            resetComPort();
            transmitTimer.Stop();
        }

        private void comPortsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comPortsComboBox.SelectedItem != null)
                openButton.Enabled = true;
        }

        private void altitudeSelector_ValueChanged(object sender, EventArgs e)
        {
            this.currentAltitudeValue = altitudeSelector.Value;
        }



        private struct AltitudeProtocol
        {
            public string name;
            public StopBits stopBits;
            public Parity parity;
            public int baudRate;
            public int dataBits;
            public bool checksum;
        }
        
        private Dictionary<ProtocolType, AltitudeProtocol> altitudeProtocols = new Dictionary<ProtocolType, AltitudeProtocol>();
        private AltitudeProtocol selectedProtocol;
        private decimal currentAltitudeValue;

    }

    public enum ProtocolType
    {
        ICARUS_GARMIN_TRIMBLE,
        UPSAT_GARMIN,
        NORTHSTAR,
        UPS_AT_618_LORAN,
        MAGELLAN,
        SHADIN_RMS,
        ARNAV,
        Microair_UAV
    };
}
