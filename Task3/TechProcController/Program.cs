
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Unicode;

namespace TechProcController
{
    public class ProcessUnit
    {
        private static int nextId = 0;
        private int id;
        private UnitStatus status;

        public UnitStatus Status { get => status; set => status = value; }
        public int Id { get => id; private set => id = value; }

        public ProcessUnit()
        {
            Id = ++nextId;
            Status = UnitStatus.Working;
        }
        readonly Random rand = new();

        public void ChangeStatus()
        {
            if (Status == UnitStatus.Working)
            {
                if (rand.NextDouble() < 0.2)
                    Status = UnitStatus.Accident;
            }
            else if (Status == UnitStatus.Accident)
            {
                Status = UnitStatus.Repair;
            }
            else if (Status == UnitStatus.Repair)
            {
                if (rand.NextDouble() < 0.5)
                {
                    Status = UnitStatus.Working;
                }
            }
        }
        public bool IsWorking()
        {
            if (Status == UnitStatus.Working) return true;
            else return false;
        }
        public bool IsAccident()
        {
            if (Status == UnitStatus.Accident) return true;
            else return false;
        }
        public bool IsRepair()
        {
            if (Status == UnitStatus.Repair) return true;
            else return false;
        }
    }
    public enum UnitStatus
    {
        Working = 0,
        Accident = 1,
        Repair = 2
    }

    public class ProcessUnitController
    {
        private ProcessUnit[] processUnits;

        public ProcessUnitController(int unitsCount)
        {
            processUnits = new ProcessUnit[unitsCount];
            for (int i = 0; i < unitsCount; i++)
            {
                processUnits[i] = new ProcessUnit(); // создаём объект для каждого элемента
            }
        }

        public string Data()
        {
            // processUnits - это, например, List<ProcessUnit> или Dictionary<int, Status>
            // где каждый элемент имеет свойства Id и Status
            return string.Join(",", processUnits.Select(u => $"{u.Id}:{(int)u.Status}"));
        }

        public void ChangeUnitsStatus()
        {
            foreach (ProcessUnit unit in processUnits)
            {
                unit.ChangeStatus();
            }
        }
    }

    public static class Program
    {
        public async static Task Main()
        {
            int unitsCount = ConfigParser.Parse();

            Console.WriteLine("Контроллер запущен. Ожидание подключения диспетчера...");

            IPHostEntry localhost = await Dns.GetHostEntryAsync("127.0.0.1");
            IPAddress localIPAddress = localhost.AddressList[0];
            IPEndPoint ipEndPoint = new(localIPAddress, 6666);
            Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            while (true)
            {
                try
                {
                    await client.ConnectAsync(ipEndPoint);
                    var messageByte = Encoding.UTF8.GetBytes(unitsCount.ToString());
                    _ = await client.SendAsync(messageByte, SocketFlags.None);

                    break; // подключились — выходим из цикла
                }
                catch (SocketException)
                {
                    // Сервер ещё не запущен, ждём 5 секунд и пробуем снова
                    await Task.Delay(5000);
                }
            }
            Console.WriteLine("Диспетчер подключился.");
            var buffer = new byte[1_028];
            var received = await client.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            Console.WriteLine($"Контроллер получил ответ: {response}");

            ProcessUnitController procUnitController = new(unitsCount);
            try
            {
                while (true)
                {
                    procUnitController.ChangeUnitsStatus();
                    byte[] buff = Encoding.UTF8.GetBytes(procUnitController.Data());

                    await client.SendAsync(buff);

                    await Task.Delay(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public static class ConfigParser
    {
        private static readonly string cfgName = "cfg.txt";

        public static int Parse()
        {
            if (File.Exists(cfgName))
            {

                string content = File.ReadAllText(cfgName);

                if (int.TryParse(content, out int value))
                    return value;
                else throw new ArgumentException("Неверная конфигурация файла");
            }
            else
            {
                throw new ArgumentNullException("Файл конфигурации не найден.");
            }
        }

    }
}