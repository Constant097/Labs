using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringDevice
{
    interface IMeasuringDeviceWithProperties : ILoggingMeasuringDevice
    {
        /// <summary>
        /// Gets the Units used natively by the device.
        /// </summary>
        Units UnitsToUse { get; }

        /// <summary>
        /// Gets an array of the measurements taken by the device.
        /// </summary>
        int[] DataCaptured { get; }

        /// <summary>
        /// Gets the most recent measurement taken by the device.
        /// </summary>
        int MostRecentMeasure { get; }

        /// <summary>
        /// Gets or sets the name of the logging file used. 
        /// If the logging file changes this closes the current file and creates the new file.
        /// </summary>
        string LoggingFileName { get; set; }
    }
}
