using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Task1
{
    public class Client
    {
        private readonly Socket client;
        private readonly IPEndPoint ipEndPoint;
        RichTextBox clientTextBox;

        public Client(IPEndPoint ipEndPoint)
        {
            this.ipEndPoint = ipEndPoint;
            client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task SendMessage(string? message)
        {
            var messageByte = Encoding.UTF8.GetBytes(message);
            _ = await client.SendAsync(messageByte, SocketFlags.None);
            await ReceiveMessage(clientTextBox);

        }

        internal void CloseConnect()
        {
            client?.Close();
            client?.Dispose();
        }

        internal async Task ConnectAsync(ComboBox comboBox, RichTextBox textBox)
        {
            clientTextBox = textBox;

            await client.ConnectAsync(ipEndPoint);

            string response = await ReceiveMessage(clientTextBox);

            DrawBoxes.FillComboBox(comboBox, response);

        }

        private async Task<string> ReceiveMessage(RichTextBox textBox)
        {
            var buffer = new byte[4_096];
            var received = await client.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            textBox.Text += $"\nКлиент получил {DateTime.Now} " + response;
            return response;
        }
    }
}
