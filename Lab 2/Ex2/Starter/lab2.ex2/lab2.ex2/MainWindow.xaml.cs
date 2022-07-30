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

namespace lab2.ex2
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

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the integer entered by the user
            int i;
            if (!int.TryParse(inputTextBox.Text, out i))
            {
                MessageBox.Show("TextBox does not contain an integer");
                return;
            }
            // Check that the user has not entered a negative number 
            if (i < 0)
            {
                MessageBox.Show("Please enter a positive number or zero");
                return;
            }
            // Remainder will hold the remainder after dividing i by 2
            // after each iteration of the algorithm
            int remainder = 0;
            // Binary will be used to construct the string of bits
            // that represent i as a binary value
            StringBuilder binary = new StringBuilder();
            // Generate the binary representation of i
            do
            {
                remainder = i % 2;
                i = i / 2;
                binary.Insert(0, remainder);
            }
            while (i > 0);
            // Display the result
            binaryLabel.Content = binary.ToString();
        } 

    }
}