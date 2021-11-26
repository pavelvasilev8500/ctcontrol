using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControlLibrary.Classes
{
    public static class GetPCIP
    {
        public static string getIP()
        {
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    return "http://" + endPoint.Address.MapToIPv4().ToString() + ":8282";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
