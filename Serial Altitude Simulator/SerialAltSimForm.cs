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
            InitializeProtocol();

            scanForPorts();
        }

        private void InitializeProtocol()
        {
            this.currentAltitudeValue = 0;
            this.altitudeProtocols.Add(ProtocolType.TRIMBLE_GARMIN, new AltitudeProtocol {stopBits = StopBits.One, parity = Parity.None, baudRate = 9600, dataBits = 8, checksum = false });
            this.altitudeProtocols.Add(ProtocolType.UPS_GARMIN, new AltitudeProtocol { stopBits = StopBits.One, parity = Parity.None, baudRate = 1200, dataBits = 8, checksum = true });
            this.altitudeProtocols.Add(ProtocolType.NORTHSTAR, new AltitudeProtocol { stopBits = StopBits.One, parity = Parity.Odd, baudRate = 9600, dataBits = 8, checksum = false });
            this.altitudeProtocols.Add(ProtocolType.UPS_AT_618_LORAN, new AltitudeProtocol { stopBits = StopBits.One, parity = Parity.Odd, baudRate = 1200, dataBits = 7, checksum = true });
            this.altitudeProtocols.Add(ProtocolType.MAGELLAN, new AltitudeProtocol { stopBits = StopBits.One, parity = Parity.Even, baudRate = 1200, dataBits = 7, checksum = true });
            this.altitudeProtocols.Add(ProtocolType.SHADIN, new AltitudeProtocol { stopBits = StopBits.One, parity = Parity.None, baudRate = 9600, dataBits = 8, checksum = true });
            //this.altitudeProtocols.Add(ProtocolType.ARNAV, new AltitudeProtocol { stopBits = StopBits.One, parity = Parity.Odd, baudRate = 9600, dataBits = 8 });
        }

        private void scanForPorts()
        {
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
                case ProtocolType.TRIMBLE_GARMIN: // ICARUS_Trimble/Garmin
                    // Sample: "ALT 00800\r" 
                    return "ALT " + ((altitudeValue < 0) ? altitudeValue.ToString("0000") : altitudeValue.ToString("00000")) + "\r";

                case ProtocolType.UPS_GARMIN: // UPSAT/Garmin AT/IIMorrow/Dynon Encoder
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

                case ProtocolType.SHADIN: // Shadin RMS
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


        private struct AltitudeProtocol
        {
            public StopBits stopBits;
            public Parity parity;
            public int baudRate;
            public int dataBits;
            public bool checksum;
        }

        public enum ProtocolType
        {
            TRIMBLE_GARMIN,
            UPS_GARMIN,
            NORTHSTAR,
            UPS_AT_618_LORAN,
            MAGELLAN,
            SHADIN,
            ARNAV
        };

        private Dictionary<ProtocolType, AltitudeProtocol> altitudeProtocols = new Dictionary<ProtocolType, AltitudeProtocol>();
        private AltitudeProtocol selectedProtocol;
        private decimal currentAltitudeValue;

        private void altitudeSelector_ValueChanged(object sender, EventArgs e)
        {
            this.currentAltitudeValue = altitudeSelector.Value;
        }


    }
}
