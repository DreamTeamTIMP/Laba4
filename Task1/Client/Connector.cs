using System.Net;

namespace Task1.Connect
{
    public class Connector
    {
        private IPEndPoint ipEndPoint;
        
        public IPEndPoint IpEndPoint { get => ipEndPoint; set => ipEndPoint = value; }

        public async Task CreateIpEndPoint(string ipAdress)
        {
            IPHostEntry localhost = await Dns.GetHostEntryAsync(ipAdress);
            IPAddress localIPAddress = localhost.AddressList[0];
            IpEndPoint = new(localIPAddress, 6666);
            
        }
    }
}
