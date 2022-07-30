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
using StressTest;

namespace Enumerations
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

        /// <summary>
        /// Display the enumeration values in ListBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Material[] materialsList = (Material[])System.Enum.GetValues(typeof(Material));
            for (int i = 0; i < materialsList.Length; i++)
            {
                materials.Items.Add(materialsList[i]);
            }

            CrossSection[] crossSectionList = (CrossSection[])System.Enum.GetValues(typeof(CrossSection));
            for (int i = 0; i < crossSectionList.Length; i++)
            {
                crosssections.Items.Add(crossSectionList[i]);
            }

            TestResult[] testResultsList = (TestResult[])System.Enum.GetValues(typeof(TestResult));
            for (int i = 0; i < testResultsList.Length; i++)
            {
                testresults.Items.Add(testResultsList[i]);
            }

            materials.SelectedIndex = 0;
            crosssections.SelectedIndex = 0;
            testresults.SelectedIndex = 0;
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (materials.SelectedIndex == -1 || crosssections.SelectedIndex == -1 || testresults.SelectedIndex == -1) return;

            Material selectedMaterial = (Material)materials.SelectedItem;
            CrossSection selectedCrossSection = (CrossSection)crosssections.SelectedItem;
            TestResult selectedTestResult = (TestResult)testresults.SelectedItem;
            StringBuilder selectionStringBuilder = new StringBuilder();

            switch (selectedMaterial)
            {
                case Material.StainlessSteel: selectionStringBuilder.Append("Material: Stainless Steel, ");
                    break;
                case Material.Aluminum: selectionStringBuilder.Append("Material: Aluminum, ");
                    break;
                case Material.ReinforcedConcrete: selectionStringBuilder.Append("Material: Reinforced Concrete, ");
                    break;
                case Material.Composite: selectionStringBuilder.Append("Material: Composite, ");
                    break;
                case Material.Titanium: selectionStringBuilder.Append("Material: Titanium, ");
                    break;
            }
            switch (selectedCrossSection)
            {
                case CrossSection.IBeam: selectionStringBuilder.Append("Cross-section: I-Beam, ");
                    break;
                case CrossSection.Box: selectionStringBuilder.Append("Cross-section: Box, ");
                    break;
                case CrossSection.ZShaped: selectionStringBuilder.Append("Cross-section: Z-Shaped, ");
                    break;
                case CrossSection.CShaped: selectionStringBuilder.Append("Cross-section: C-Shaped, ");
                    break;
            }
            switch (selectedTestResult)
            {
                case TestResult.Pass: selectionStringBuilder.Append("Result: Pass.");
                    break;
                case TestResult.Fail: selectionStringBuilder.Append("Result: Fail.");
                    break;
            }
            testDetails.Content = selectionStringBuilder.ToString();
        }
    }
}
