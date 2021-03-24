using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace SemaforySterowanieTest
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SerialPort port;
        public MainPage()
        {
            this.InitializeComponent();
            port = new SerialPort();
            port.PortName = "COM5";
            port.BaudRate = 9600;
            port.DtrEnable = true;
            port.Handshake = Handshake.None;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.Two;
            port.WriteTimeout = 3000;
            port.Open();
            DevisibleAllLights();
            SetStartConfig();
            
        }

        private void sem1Switch_Toggled(object sender, RoutedEventArgs e)
        {
            if(sem1Switch.IsOn == true)
            {
                if(sem2Switch.IsOn == true)
                {
                    sem2Switch.IsOn = false;
                }
                sem1Red.Visibility = Visibility.Collapsed;
                sem1Green.Visibility = Visibility.Visible;
                //port.Write("01");
            }
            else
            {
                sem1Red.Visibility = Visibility.Visible;
                sem1Green.Visibility = Visibility.Collapsed;
                if (sem2Switch.IsOn == true)
                {
                    //port.Write("10");
                }
                else
                {
                    //port.Write("00");
                }
            }
        }

        private void sem2Switch_Toggled(object sender, RoutedEventArgs e)
        {
            if (sem2Switch.IsOn == true)
            {
                if (sem1Switch.IsOn == true)
                {
                    sem1Switch.IsOn = false;
                }
                sem2Red.Visibility = Visibility.Collapsed;
                sem2Green.Visibility = Visibility.Visible;
                //port.Write("10");
            }
            else
            {
                sem2Red.Visibility = Visibility.Visible;
                sem2Green.Visibility = Visibility.Collapsed;
                if (sem1Switch.IsOn == true)
                {
                    //port.Write("01");
                }
                else
                {
                    //port.Write("00");
                }
            }
        }
        private void DevisibleAllLights()
        {
            sem1Red.Visibility = Visibility.Collapsed;
            sem1Green.Visibility = Visibility.Collapsed;
            sem2Red.Visibility = Visibility.Collapsed;
            sem2Green.Visibility = Visibility.Collapsed;
        }
        private void SetStartConfig()
        {
            sem1Switch.IsOn = false;
            sem2Switch.IsOn = false;
            sem1Red.Visibility = Visibility.Visible;
            sem2Red.Visibility = Visibility.Visible;
            //port.Write("00");
        }
    }
}
