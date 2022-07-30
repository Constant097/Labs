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

namespace Exercise1TestHarness
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

        const string labFolder = @"E:\Labfiles\Lab 10\Lab A\";
        MeasureMassDevice device;
        private void createInstance_Click(object sender, RoutedEventArgs e)
        {
            device = new MeasureMassDevice (Units.Metric, labFolder + "LogFile.txt");
            device.StartCollecting();

            // TODO: Add code to set the unitsBox to the current units.
            unitsBox.Text = device.UnitsToUse.ToString();
            System.Threading.Thread.Sleep(10000);

            mostRecentMeasureBox.Text = device.MostRecentMeasure.ToString();
            // TODO: Update to use the LoggingFileName property.
            loggingFileNameBox.Text = device.LoggingFileName.Replace(labFolder, "");
            metricValueBox.Text = device.MetricValue().ToString();
            imperialValueBox.Text = device.ImperialValue().ToString();
            rawDataValues.ItemsSource = null;
            // TODO: Update to use the DataCaptured property.
            rawDataValues.ItemsSource = device.DataCaptured;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                // TODO: Add code to update the log file name property of the device.
                device.LoggingFileName = labFolder + loggingFileNameBox.Text;
            }
            else
            {
                MessageBox.Show("You must start collecting first.");
            }
        }

        private void dispose_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                device.StopCollecting();
                device.Dispose();
                device = null;
            }
        }
    }
}
