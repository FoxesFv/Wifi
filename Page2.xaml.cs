using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ping p = new Ping();
            PingReply r;
            string s;
            s = textBox.Text;
            r = p.Send(s);

            if (r.Status == IPStatus.Success)
            {
                textBlock.Text = "Ping к " + s.ToString() + " [" + r.Address.ToString() + "]" + " Успешен"
                   + " Задержка = " + r.RoundtripTime.ToString() + " ms" + "\n";
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) || textBox.Text == "")
            {
                MessageBox.Show("Please use valid IP or web address!!");
            }
        }


    }
}
