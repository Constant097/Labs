using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MeasuringDevice;

namespace Exercise2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void createInstance_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Modify this method.
            using (MeasureMassDevice device = new MeasureMassDevice(Units.Metric, @"E:\Labfiles\Lab 9\LogFile.txt"))
            {
                device.StartCollecting();
                System.Threading.Thread.Sleep(20000);
                loggingFileNameBox.Text = device.GetLoggingFile();
                metricValueBox.Text = device.MetricValue().ToString();
                imperialValueBox.Text = device.ImperialValue().ToString();
                rawDataValues.ItemsSource = device.GetRawData();
                device.StopCollecting();
            }
        }
    }
}
