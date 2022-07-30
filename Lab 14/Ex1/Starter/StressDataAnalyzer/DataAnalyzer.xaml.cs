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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

// Namespaces containing the Tree and TestResult types
using BinaryTree;
using StressTestResult;

// Namespace containing counters and other performance diagnostics
using System.Diagnostics;

namespace StressDataAnalyzer
{
    /// <summary>
    /// WPF application to enable a user to query stress data and display the results.
    /// <para>
    /// The data is held in a file and read into a binary tree structure holding TestResult structs.
    /// </para>
    /// <para>
    /// The user enters query criteria onto the form and then clicks Display.
    /// </para>
    /// <para>
    /// The application displays the results in a TextBox control on the form.
    /// </para>
    /// </summary>
    public partial class DataAnalyzer : Window
    {
        private const string stressDataFilename = @"E:\Labfiles\Lab 14\StressData.dat";
        // Declare a string variable to hold the name of the file 
        // that contains the stress test data.

        // Declare a Tree variable to hold the loaded data.
        private Tree<TestResult> stressData = null;
        public DataAnalyzer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event-handling method for the Loaded event of the WPF window.
        /// <para>
        /// This method calls readTestData to read in the test data and populate the binary tree with the results.
        /// </para>
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Read the test data and populate the binary tree
            // Use a BackgroundWorker object to avoid tying up the UI
            BackgroundWorker workerThread = new BackgroundWorker();
            workerThread.WorkerReportsProgress = false;
            workerThread.WorkerSupportsCancellation = false;
            workerThread.DoWork += (o, args) =>
            {
                this.ReadTestData();
            };
            workerThread.RunWorkerCompleted += (o, args) =>
            {
                this.displayResults.IsEnabled = true;
                this.statusMessage.Content = "Ready";
            };
                workerThread.RunWorkerAsync();
            this.statusMessage.Content = "Reading test data ...";
        }

        // Add a method that reads the test data from the file specified by the stressDataFileName string
        // and creates the stressData binary tree using this data.
        private void ReadTestData()
        {
            try
            {
                using (FileStream readStream = File.Open(stressDataFilename, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    TestResult initialNode = (TestResult)formatter.Deserialize(readStream);
                    stressData = new Tree<TestResult>(initialNode);
                    while (readStream.Position < readStream.Length)
                    {
                        TestResult data = (TestResult)formatter.Deserialize(readStream);
                        stressData.Insert(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Event-handling method for the Click event of the displayResults button.
        /// <para>
        /// This method retrieves the query criteria entered by the user on the form and then calls
        /// the CreateQuery method to generate an enumerable result set.
        /// </para>
        /// The results are formatted by using the FormatResults method, 
        /// and are then displayed in the results TextBox control on the form.
        /// </summary>
        private void DisplayResults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Capture the criteria for the start and end dates. Use DateTime.MinValue and DateTime.MaxValue as default values
                DateTime dateStart = String.IsNullOrWhiteSpace(this.startDate.Text) ? DateTime.MinValue : DateTime.Parse(this.startDate.Text);
                DateTime dateEnd = String.IsNullOrWhiteSpace(this.endDate.Text) ? DateTime.MaxValue : DateTime.Parse(this.endDate.Text); ;

                // The date and times in the test data include a time of day, 
                // whereas the dates selected by the user will have the time of day set to midnight.
                // Consequently, you need to add 1 day to the end date specified by the user
                // to avoid losing test results generated on that date.
                if (dateEnd < DateTime.MaxValue)
                {
                    dateEnd = dateEnd.AddDays(1);
                }

                // Capture the temperature range criteria
                short temperatureStart = (short)this.fromTemperature.Value;
                short temperatureEnd = (short)this.toTemperature.Value;

                // Capture the applied stress criteria
                short appliedStressStart = (short)this.fromAppliedStress.Value;
                short appliedStressEnd = (short)this.toAppliedStress.Value;

                // Capture the deflection criteria
                short deflectionStart = (short)this.fromDeflection.Value;
                short deflectionEnd = (short)this.toDeflection.Value;

                // Generate an enumerable result set using these criteria
                IEnumerable<TestResult> query = CreateQuery(dateStart, dateEnd, temperatureStart, temperatureEnd, appliedStressStart, appliedStressEnd, deflectionStart, deflectionEnd);

                // Determine how long the quety actually takes to run -
                // Calling the Count() method retrieves all rows
                Stopwatch timer = Stopwatch.StartNew();
                int rowCount = query.Count();
                long timeTaken = timer.ElapsedMilliseconds;
                queryTime.Content = String.Format("Time (ms): {0}", timeTaken);

                // Format the results into a string
                // This might take some time, so use a BackgroundWorker to avoid tying up the user interface
                BackgroundWorker workerThread = new BackgroundWorker();
                workerThread = new BackgroundWorker();
                workerThread.WorkerReportsProgress = false;
                workerThread.WorkerSupportsCancellation = false;

                // Return the formatted string as the result of the background 
                // operation.
                workerThread.DoWork += (o, args) =>
                {
                    args.Result = FormatResults(query);
                };

                // When the BackgroundWorker object has completed reading 
                // the test data, display the results, set the status bar 
                // to "Ready", and enable the displayResults button.
                workerThread.RunWorkerCompleted += (o, args) =>
                {
                    this.results.Text = args.Result as string;
                    this.displayResults.IsEnabled = true;
                    this.statusMessage.Content = "Ready";
                };
                // Start the BackgroundWorker, disable the Display button,
                // and set the status bar to "Fetching results ..."
                workerThread.RunWorkerAsync();
                this.displayResults.IsEnabled = false;
                this.statusMessage.Content = "Fetching results ...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Method that generates an enumerable collection of TestResult items from the stressData
        /// binary tree, based on the criteria specified by the user. All data fetched will fall within
        /// the range specified by these criteria.
        /// </summary>
        /// <param name="dateStart">
        /// The start date criterion.
        /// </param>
        /// <param name="dateEnd">
        /// The end date criterion.
        /// </param>
        /// <param name="temperatureStart">
        /// The lower temperature criterion,
        /// </param>
        /// <param name="temperatureEnd">
        /// The upper temperature criterion.
        /// </param>
        /// <param name="appliedStressStart">
        /// The lower applied stress criterion.
        /// </param>
        /// <param name="appliedStressEnd">
        /// The upper applied stress criterion.
        /// </param>
        /// <param name="deflectionStart">
        /// The lower deflection criterion.
        /// </param>
        /// <param name="deflectionEnd">
        /// The upper deflection criterion.
        /// </param>
        /// <returns>
        /// An IEnumerable&lt;TestResult&gt; object that can be used to iterate through the results.
        /// </returns>
        private IEnumerable<TestResult> CreateQuery(DateTime dateStart, DateTime dateEnd, short temperatureStart, short temperatureEnd, short appliedStressStart, short appliedStressEnd, short deflectionStart, short deflectionEnd)
        {
            IEnumerable<TestResult> query =
                   from result in stressData
                   where result.TestDate >= dateStart && result.TestDate <= dateEnd &&
                         result.Temperature >= temperatureStart && result.Temperature <= temperatureEnd &&
                         result.AppliedStress >= appliedStressStart && result.AppliedStress <= appliedStressEnd &&
                         result.Deflection >= deflectionStart && result.Deflection <= deflectionEnd
                   orderby result.TestDate
                   select result;
            return query;
        }

        /// <summary>
        /// Fetch the data defined by the LINQ query specified as the parameter 
        /// and format the results as a string.
        /// </summary>
        /// <param name="query">
        /// The IEnumerable&lt;TestResult&gt;
        /// </param>
        /// <returns>
        /// A formatted string that contains the data fetched by the query.
        /// </returns>
        private string FormatResults(IEnumerable<TestResult> query)
        {
            // Use a StringBuilder object to construct the formatted string.
            StringBuilder builder = new StringBuilder();

            // Add a heading and indicate the number of matching results retrieved.
            builder.Append(String.Format("Stress Test Results. Number of matching items: {0}\n\n", query.Count()));

            // Add column headings.
            builder.Append("Test Date\t\tTemperature\tApplied Stress\tDeflection\n");

            // Iterate through the results and format each item found.
            foreach (var item in query)
            {
                builder.Append(String.Format("{0:d}\t\t{1}\t\t{2}\t\t{3}\n",
                    item.TestDate,
                    item.Temperature,
                    item.AppliedStress,
                    item.Deflection));
            }

            // Return the string that is constructed by using the 
            // StringBuilder object
            return builder.ToString();
        }
    }
}
