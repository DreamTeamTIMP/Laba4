using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Controller
{
    public class Controller
    {
        private int temperature;
        private int pressure;

        public string Data;

        private Random rand = new Random();

        public int Temperature { get => temperature; set => temperature = value; }
        public int Pressure { get => pressure; set => pressure = value; }

        public void Generate()
        {
            Temperature = rand.Next(Constants.MinTemperature, Constants.MaxTemperature + 1);
            Pressure = rand.Next(Constants.MinPressure, Constants.MaxPressure + 1);
            Data = $"{Temperature},{Pressure}";
        }

        static class Program
        {
            static async Task Main()
            {
                Console.WriteLine("Контроллер запущен. Ожидание подключения диспетчера...");

                IPHostEntry localhost = await Dns.GetHostEntryAsync("127.0.0.1");
                IPAddress localIPAddress = localhost.AddressList[0];
                IPEndPoint ipEndPoint = new(localIPAddress, 6666);
                Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


                while (true)
                {
                    // Подключение (с повторами)
                    while (true)
                    {
                        try
                        {
                            await client.ConnectAsync(ipEndPoint);
                            break;
                        }
                        catch (SocketException)
                        {
                            Console.WriteLine("Сервер недоступен. Повтор через 5 сек...");
                            await Task.Delay(5000);
                        }
                    }

                    Console.WriteLine("Диспетчер подключился.");
                    Controller controller = new Controller();

                    // Цикл отправки
                    while (true)
                    {
                        try
                        {
                            controller.Generate();
                            byte[] buffer = Encoding.UTF8.GetBytes(controller.Data);
                            await client.SendAsync(buffer);
                            Console.WriteLine($"Отправлено: T={controller.Temperature}, P={controller.Pressure}");
                            await Task.Delay(1000);
                        }
                        catch (SocketException)
                        {
                            Console.WriteLine("Соединение разорвано. Переподключение...");
                            client.Close();
                            client = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                            break; // выходим из внутреннего цикла и идём на переподключение
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            return;
                        }
                    }
                }
            }
        }
    }
    public static class Constants
    {
        public const int MaxTemperature = 100;
        public const int MinTemperature = 0;

        public const int MaxPressure = 6;
        public const int MinPressure = 0;
    }
}