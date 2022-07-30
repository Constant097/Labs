using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeviceControl;
using System.IO;

namespace MeasuringDevice
{
    // TODO: Implement the IMeasuringDeviceWithProperties interface.
    public abstract class MeasureDataDevice : ILoggingMeasuringDevice, IDisposable
    {
        /// <summary>
        /// Converts the raw data collected by the measuring device into a metric value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to metric units.</returns>
        public abstract decimal MetricValue();

        /// <summary>
        /// Converts the raw data collected by the measuring device into an imperial value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to imperial units.</returns>
        public abstract decimal ImperialValue();

        /// <summary>
        /// Starts the measuring device.
        /// </summary>
        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);

            // New code to check the logging file is not already open.
            // If it is already open then write a log message.
            // If not open the logging file.
            if (loggingFileWriter == null)
            {
                // Check if the logging file exists - if not create it.
                if (!File.Exists(loggingFileName))
                {
                    loggingFileWriter = File.CreateText(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Created");
                    loggingFileWriter.WriteLine("Collecting Started");
                }
                else
                {
                    loggingFileWriter = new StreamWriter(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Opened");
                    loggingFileWriter.WriteLine("Collecting Started");
                }
            }
            else
            {
                loggingFileWriter.WriteLine("Log file status checked - Already open");
                loggingFileWriter.WriteLine("Collecting Started");
            }
            GetMeasurements();
        }

        /// <summary>
        /// Stops the measuring device. 
        /// </summary>
        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }

            // New code to write to the log.
            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Collecting Stopped");
            }
        }

        /// <summary>
        /// Enables access to the raw data from the device in whatever units are native to the device.
        /// </summary>
        /// <returns>The raw data from the device in native format.</returns>
        public int[] GetRawData()
        {
            return dataCaptured;
        }

        private void GetMeasurements()
        {
            dataCaptured = new int[10];
            System.Threading.ThreadPool.QueueUserWorkItem((dummy) =>
            {
                int x = 0;
                Random timer = new Random();

                while (controller != null)
                {
                    System.Threading.Thread.Sleep(timer.Next(1000, 5000));
                    dataCaptured[x] = controller != null ? controller.TakeMeasurement() : dataCaptured[x];
                    mostRecentMeasure = dataCaptured[x];

                    if (loggingFileWriter != null)
                    {
                        loggingFileWriter.WriteLine("Measurement Taken: {0}", mostRecentMeasure.ToString());
                    }

                    x++;
                    if (x == 10)
                    {
                        x = 0;
                    }
                }
            });
        }

        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected DeviceType measurementType;

        protected string loggingFileName;
        private TextWriter loggingFileWriter;

        /// <summary>
        /// Returns the file name of the logging file for the device.
        /// </summary>
        /// <returns>The file name of the logging file.</returns>
        public string GetLoggingFile()
        {
            return loggingFileName;
        }

        /// <summary>
        /// Dispose method required for the IDispose interface.
        /// </summary>
        public void Dispose()
        {
            // Check that the log file is closed, if it is not closed log a message and close it.
            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Object Disposed");
                loggingFileWriter.Flush();
                loggingFileWriter.Close();
                loggingFileWriter = null;
            }
        }

        // TODO: Add properties specified by the IMeasuringDeviceWithProperties interface.
    }
}
