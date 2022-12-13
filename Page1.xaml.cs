using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.NetworkInformation;   
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Navigation_Drawer_App
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            string localIp = GetLocalIPAddress();
            LocalIpTextBlock.Content = "Локальный IP-адрес: " + localIp; //+ " - " + GetNameByIpAdress(localIp);

            AllLocalIpsTextblock.Text = "НАЙДЕННЫЕ СЕТЕВЫЕ ИНТЕРФЕЙСЫ :";

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                AllLocalIpsTextblock.Text += "\n" + "Имя интерфейса: " + netInterface.Name;
                AllLocalIpsTextblock.Text += "\n" + "Описание: " + netInterface.Description;
                AllLocalIpsTextblock.Text += "\n";


            }






          
            AllLocalIpsTextblock.Text += "\n" + "НАЙДЕННЫЕ СЕТЕВЫЕ АДРЕСА УСТРОЙСТВ :";
            foreach (string ip in GetAllLocalIPAddresses())
            {
                
                AllLocalIpsTextblock.Text += "\n" + ip + PingIp(ip); // + " - " + GetNameByIpAdress(ip); - не работает
            }



            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            var externalIp = IPAddress.Parse(externalIpString);


            AllLocalIpsTextblock.Text += "\n";
            AllLocalIpsTextblock.Text += "\n" + "Внешний Ip адрес сети :";
            AllLocalIpsTextblock.Text += "\n" + externalIp;

        }



        public static string GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("В системе не найдены адаптеры");

        }


        public static List<string> GetAllLocalIPAddresses()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            List<string> ipsList = new List<string>();
            
            foreach (IPAddress ip in host.AddressList)
            {

                ipsList.Add(ip.ToString());
                
                

            }
            return ipsList;
        }


        public static string GetNameByIpAdress(string ip) // выдает одинаковые имена
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(ip);

            string name = hostEntry.HostName;

            return name;
        }

        public static string PingIp(string ip)
        {
            Ping ping = new Ping();
            PingReply reply;
           
            
            reply = ping.Send(ip);

            if (reply.Status == IPStatus.Success)
            {
              return "( " + reply.RoundtripTime.ToString() + " ms" + " )";
            }
            throw new Exception("Адрес указан не верно или недоступен");
        }




    }
}
