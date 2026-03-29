using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Server
    {
        private Socket listener;
        private readonly RichTextBox textBox;
        private CancellationTokenSource cTS;

        public Server(IPEndPoint ipEndPoint, RichTextBox textBox)
        {
            Listener = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Listener.Bind(ipEndPoint);
            Listener.Listen(100);
            this.textBox = textBox;
            cTS = new CancellationTokenSource();
        }

        public Socket Listener { get => listener; set => listener = value; }

        internal async Task StartAsync()
        {

            while (!cTS.IsCancellationRequested)
            {
                try
                {
                    var client = await Listener.AcceptAsync();
                    await Listen(client);
                }
                catch (OperationCanceledException e)
                {
                    break;
                }
                catch (ObjectDisposedException e)
                {
                    break;
                }
                catch (SocketException e)
                {
                    break;
                }
            }
        }

        private async Task Listen(Socket client)
        {
            try
            {
                textBox.Invoke(new Action(() =>
                {
                    textBox.Text += "\n" + $"Клиент соединился {DateTime.Now} с адреса {client.RemoteEndPoint}";
                }));
                string devices = "\n";
                foreach (var drive in DriveInfo.GetDrives())
                {
                    devices += drive.Name + "\n";
                }
                byte[] devicesBytes = Encoding.UTF8.GetBytes(devices);
                await client.SendAsync(devicesBytes, 0);

                while (true)
                {
                    var buffer = new byte[4_096];
                    var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                    if (received == 0) return;

                    var request = Encoding.UTF8.GetString(buffer, 0, received);

                    textBox.Invoke(new Action(() =>
                    {
                        textBox.Text += $"\nСервер получил: {request}";
                    }));
                    string response = Request(request);
                    byte[] responceBytes = Encoding.UTF8.GetBytes(response);
                    await client.SendAsync(responceBytes, 0);
                }

            }
            finally
            {
                client?.Close();
                client?.Dispose();
            }
        }

        private static string Request(string request)
        {
            if (Directory.Exists(request))
            {
                try 
                {
                    // Получаем полные пути
                    var entries = Directory.GetFileSystemEntries(request);
                    // Преобразуем в имена (требует using System.Linq;)
                    var names = entries.Select(Path.GetFileName);
                    return string.Join(Environment.NewLine, names);
                }
                catch (Exception ex)
                {
                    return $"Ошибка при чтении каталога: {ex.Message}";
                }
            }
            else if (File.Exists(request) && Path.GetExtension(request).ToLower() == ".txt")
            {
                try
                {
                    string content = File.ReadAllText(request);
                    return content;
                }
                catch (Exception ex)
                {
                    return $"Ошибка при чтении файла: {ex.Message}";
                }
            }
            else
            {
                return "Ошибка: указанный путь не является каталогом или текстовым файлом.";
            }
        }

        internal void CloseServer()
        {
            listener.Shutdown(SocketShutdown.Both);
            cTS?.Cancel();
            listener?.Close();
            listener?.Dispose();
        }
    }
}
