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
namespace Server
{
    public partial class MainForm : Form
    {
        private Server listener;
        private Connector connector;

        public MainForm()
        {
            InitializeComponent();
        }
        private void ShowError(string exception) => MessageBox.Show(exception, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);


        public void ChangeServerText(string text)
        {
            richTextBoxServer.Text += "\n" + text;
        }

        private async void buttonOffnOnServer_Click(object sender, EventArgs e)
        {
            if (buttonOffnOnServer.Text == "Включить сервер")
            {
                try
                {
                    connector = new Connector();
                    await connector.CreateIpEndPoint("127.0.0.1");
                    listener = new(connector.IpEndPoint,richTextBoxServer);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                    return;
                }
                ChangeServerText($"Сервер включен {DateTime.Now}");
                buttonOffnOnServer.Text = "Отключить сервер";

                await listener.StartAsync();
                return;
            }
            else
            {
                try
                {
                    listener.CloseServer();
                    buttonOffnOnServer.Text = "Включить сервер";
                    ChangeServerText($"Сервер выключен {DateTime.Now}");

                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        
    }
}
