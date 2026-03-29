using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.AxHost;

namespace DispatcherConsoleTask3
{
    public partial class MainForm : Form
    {
        private Button[] unitButtons;
        private int numUnits;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            IPHostEntry localhost = await Dns.GetHostEntryAsync("127.0.0.1");
            IPAddress localIPAddress = localhost.AddressList[0];
            IPEndPoint ipEndPoint = new(localIPAddress, 6666);
            Socket listener = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ipEndPoint);
            listener.Listen(100);
            var client = await listener.AcceptAsync();

            await StartToWork(client);
            while (true)
            {
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                if (received == 0) return;

                var request = Encoding.UTF8.GetString(buffer, 0, received).Trim();

                var parts = ToDictionary(request);

                if (parts.Count == numUnits)
                {
                    foreach ( var part in parts)
                    {
                        var color = part.Value switch
                        {
                            0 => Color.Green,
                            1 => Color.Red,
                            2 => Color.Gray,
                            _ => Color.White,
                        };
                        unitButtons[part.Key - 1].BackColor = color;
                    }
                }
            }

        }

        private async Task StartToWork(Socket client)
        {
            var buffer = new byte[4_096];
            var received = await client.ReceiveAsync(buffer, SocketFlags.None);
            if (received == 0) return;

            var request = Encoding.UTF8.GetString(buffer, 0, received).Trim();
            
            if (!int.TryParse(request, out int value))
                throw new ArgumentNullException(nameof(request));

            numUnits = value;
            CreateButtons(value);

            byte[] ack = Encoding.UTF8.GetBytes(value.ToString());
            await client.SendAsync(ack, SocketFlags.None);

        }

        private Dictionary<int,int> ToDictionary(string request)
        {
            return request.Split(",")
                           .Select(x => x.Split(":"))
                           .ToDictionary(x => int.Parse(x[0]), x => int.Parse(x[1]));
        }

        private void CreateButtons(int request)
        {
            unitButtons = new Button[request];
            int x = 10, y = 10;
            for (int i = 0; i < request; i++)
            {
                var btn = new Button
                {
                    Text = $"Установка {i + 1}",
                    Location = new Point(x, y),
                    Size = new Size(120, 40),
                    BackColor = Color.Green,
                    Tag = i
                };
                this.Controls.Add(btn);
                unitButtons[i] = btn;
                x += 130;
                if (x + 130 > this.ClientSize.Width)
                {
                    x = 10;
                    y += 50;
                }
            }
        }
    }
}
