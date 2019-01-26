using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Test_Score_List
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The ReadScores method reads the scores from the
        // TestScores.txt file into the scoresList parameter.
        private void ReadScores(List<int> scoresList)
        {
            try
            {
                // Open the TestScores.txt file.
                StreamReader inputFile = File.OpenText("TestScores.txt");

                // Read the scores into the list.
                while (!inputFile.EndOfStream)
                {
                    // Add scores from file as items to the list.
                    scoresList.Add(int.Parse(inputFile.ReadLine()));
                }

                // Close the file.
                inputFile.Close();
            }
            catch (Exception ex)
            {
                // Display an error message.
                MessageBox.Show(ex.Message);
            }
        }

        // The DisplayScores method displays the contents of the
        // scoresList parameter in the ListBox control.
        private void DisplayScores(List<int> scoresList)
        {
            // For each score in scores list
            //  add as item to the list box.
            foreach (int score in scoresList)
            {
                testScoresListBox.Items.Add(score);
            }
        }

        // The Average method returns the average of the values
        // in the scoresList parameter.
        private double Average(List<int> scoresList)
        {
            int total = 0;      // Accumulator
            double average;     // To hold the average

            // for each score in scoresList
            //  add the scores together to get the total.
            foreach (int score in scoresList)
            {
                total += score;
            }

            // Calculate the average of the scores.
            average = (double)total / scoresList.Count;

            // Return the average.
            return average;
        }

        // The AboveAverage method returns the number of
        // above average scores in scoresList.
        private int AboveAverage(List<int> scoresList)
        {
            int numAbove = 0;       // Accumulator

            // Get the average from the scoresList.
            double avrg = Average(scoresList);

            // for each score in scoresList
            //  if the score is greater than the average
            //      increment numAbove
            foreach (int score in scoresList)
            {
                if (score > avrg)
                {
                    numAbove++;
                }
            }
            // Return the number of above average scores.
            return numAbove;
        }

        // The BelowAverage method returns the number of
        // below average scores in scoresList.
        private int BelowAverage(List<int> scoresList)
        {
            int numBelow = 0;       // Accumulator

            // Get the average from the scoresList.
            double avrg = Average(scoresList);

            // for each score in scoresList
            //  if the score is less than the average
            //      increment numBelow
            foreach (int score in scoresList)
            {
                if (score < avrg)
                {
                    numBelow++;
                }
            }
            // Return the number of below average scores.
            return numBelow;
        }

        private void getScoresButton_Click(object sender, EventArgs e)
        {
            double averageScore;    // To hold the average score
            int numAboveAverage;    // Number of above average scores
            int numBelowAverage;    // Number of below average scores

            // Create scoresList of int values.
            List<int> scoresList = new List<int>();

            // Call ReadScores method to read scoresList.
            ReadScores(scoresList);

            // Call DisplayScores method to display scoresList.
            DisplayScores(scoresList);

            // Call Average method to get average score
            // display in averageLabel as a string.
            averageScore = Average(scoresList);
            averageLabel.Text = averageScore.ToString("n1");

            // Call AboveAverage method to get above average scores
            // display in aboveAverageLabel as a string.
            numAboveAverage = AboveAverage(scoresList);
            aboveAverageLabel.Text = numAboveAverage.ToString();

            // Call BelowAverage method to get below average scores
            // display in belowAverageLabel as a string.
            numBelowAverage = BelowAverage(scoresList);
            belowAverageLabel.Text = numBelowAverage.ToString();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }
}
