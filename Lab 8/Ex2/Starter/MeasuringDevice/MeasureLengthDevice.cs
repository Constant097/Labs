using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeviceControl;

namespace MeasuringDevice 
{ 
 public class MeasureLengthDevice : IMeasuringDevice
 { 
 public MeasureLengthDevice(Units deviceUnits)
 { 
    unitsToUse = deviceUnits;
} 
 public decimal MetricValue()
 { 
    decimal metricMostRecentMeasure;
    if (unitsToUse == Units.Metric)
    { 
        metricMostRecentMeasure = Convert.ToDecimal(mostRecentMeasure);
    } 
    else
    { 
       
        decimal decimalImperialValue = Convert.ToDecimal(mostRecentMeasure);
        decimal conversionFactor = 25.4M;
        metricMostRecentMeasure = decimalImperialValue * conversionFactor;
    } 
         return metricMostRecentMeasure;
    } 
         public decimal ImperialValue()
    { 
         decimal imperialMostRecentMeasure;
         if (unitsToUse == Units.Imperial)
         { 
               imperialMostRecentMeasure = Convert.ToDecimal(mostRecentMeasure);
         } 
    else
    { 
         decimal decimalMetricValue = 
         Convert.ToDecimal(mostRecentMeasure);
         decimal conversionFactor = 0.03937M;
         imperialMostRecentMeasure = decimalMetricValue * conversionFactor;
    } 
    return imperialMostRecentMeasure;
    } 
        public void StartCollecting()
    { 
     controller = DeviceController.StartDevice(measurementType);
     GetMeasurements();
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
         System.Threading.Thread.Sleep(
         timer.Next(1000, 5000));
         dataCaptured[x] = controller != null ? 
         controller.TakeMeasurement() 
        : dataCaptured[x];
         mostRecentMeasure = dataCaptured[x];
         x++;
        if (x == 10)
        { 
         x = 0;
         } 
         } 
         });
         }
    public void StopCollecting()
 { 
 if (controller != null)
 { 
 controller.StopDevice();
 controller = null;
 } 
 } 
 
 public int[] GetRawData()
 { 
 return dataCaptured;
 } 
 private Units unitsToUse;
 private int[] dataCaptured;
 private int mostRecentMeasure;
 private DeviceController controller;
 private const DeviceType measurementType = DeviceType.LENGTH;
 } 
} 