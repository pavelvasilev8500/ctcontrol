using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControlLibrary.Classes
{
    public class Server
    {

        DataPC dataPC = new DataPC();

        public void Post(string json)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create("http://192.168.0.107:4242/api/pcdata/");
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                using (var requestStream = httpRequest.GetRequestStream())
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(json);
                }
                using (var httpResponse = httpRequest.GetResponse())
                using (var responseStream = httpResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    string response = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
            }

        }

        public void Put(string json)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create("http://192.168.0.107:4242/api/pcdata/update/1");
                httpRequest.Method = "PUT";
                httpRequest.ContentType = "application/json";
                using (var requestStream = httpRequest.GetRequestStream())
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(json);
                }
                using (var httpResponse = httpRequest.GetResponse())
                using (var responseStream = httpResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    string response = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
            }

        }
    }
}
