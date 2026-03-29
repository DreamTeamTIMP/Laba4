using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace DispathcerConsole
{
    public partial class MainForm : Form
    {
        private Socket listener;

        private int dataCount = 0;
        private const int MaxPoints = 60;

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

            this.listener = listener;
            var client = await listener.AcceptAsync();

            try
            {
                while (true)
                {
                    var buffer = new byte[1_028];
                    var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                    if (received == 0) return;

                    var request = Encoding.UTF8.GetString(buffer, 0, received).Trim();

                    var parts = request.Split(',');

                    if (parts.Length == 2 &&
                            int.TryParse(parts[0], out int temp) &&
                            int.TryParse(parts[1], out int press))
                    {
                        Invoke(new Action(() => UpdateCharts(temp, press)));
                    }



                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Соединение потеряно: {ex.Message}");
            }
            finally
            {
                client?.Close();
                listener?.Close();
                client?.Dispose();
                listener?.Dispose();
                buttonStart.Invoke(() => buttonStart.Enabled = true);
            }

        }


        private void UpdateCharts(int temp, int press)
        {
            chartTemp.Series["Температура"].Points.AddXY(dataCount, temp);
            chartPress.Series["Давление"].Points.AddXY(dataCount, press);
            dataCount++;

            if (chartTemp.Series["Температура"].Points.Count > MaxPoints)
            {
                chartTemp.Series["Температура"].Points.RemoveAt(0);
                chartPress.Series["Давление"].Points.RemoveAt(0);
            }

            chartTemp.ChartAreas[0].AxisX.Minimum = dataCount > MaxPoints ? dataCount - MaxPoints : 0;
            chartTemp.ChartAreas[0].AxisX.Maximum = dataCount;
            chartPress.ChartAreas[0].AxisX.Minimum = chartTemp.ChartAreas[0].AxisX.Minimum;
            chartPress.ChartAreas[0].AxisX.Maximum = dataCount;

            chartTemp.Invalidate();
            chartPress.Invalidate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener?.Close();
            listener?.Dispose();
            Close();
        }
    }
}
