using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1.Connect;

namespace Task1
{
    public partial class MainForm : Form
    {
        private Client client;
        private Connector connector;

        public MainForm()
        {
            InitializeComponent();
            EditEnabledButtons(false);
        }

        private void EditEnabledButtons(bool enable)
        {
            buttonDisconnect.Enabled = enable;
            buttonConnect.Enabled = !enable;
            buttonSendToServer.Enabled = enable;
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBoxIPAdress.Text))
            {
                ShowError("Ничего не введено в строку IP-адрес.");
                return;
            }
            try
            {
                connector = new();
                await connector.CreateIpEndPoint(richTextBoxIPAdress.Text);
                client = new(connector.IpEndPoint);
                await client.ConnectAsync(comboBoxDirectory, richTextBoxClient);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return;
            }
            EditEnabledButtons(true);
        }

        private void ShowError(string exception) => MessageBox.Show(exception, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);


        private async void buttonSendToServer_Click(object sender, EventArgs e)
        {
            string message;
            if (listBoxFolders.SelectedItem is null)
            {
                if (comboBoxDirectory.SelectedItem is not null)
                {
                    message = comboBoxDirectory.SelectedItem.ToString();
                }
                else
                {
                    return;
                }
            }
            else
            {
                message = Path.Combine(comboBoxDirectory.SelectedItem.ToString(), listBoxFolders.SelectedItem.ToString());
            }
            try
            {
                await client.SendMessage(message);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return;
            }
        }

        private void comboBoxDirectory_SelectedIndexChanged(object sender, EventArgs e) => DrawBoxes.FillListBox(listBoxFolders, comboBoxDirectory);


        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            client.CloseConnect();
            EditEnabledButtons(false);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client?.CloseConnect();
            this.Close();
        }
    }
}
