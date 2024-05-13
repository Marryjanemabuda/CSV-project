using System;
using System.Collections.Generic;
using System.IO;

namespace CSV_Assesment
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the test data file
            string filePath = "C:\\Users\\Marryjanem\\Desktop\\Personal\\CSV Assesment\\CSV Assesment\\test data\\TestData.csv";

            // Read the contents of the file
            string csvData = ReadInputFile(filePath);

            // Parse the CSV data
            List<string[]> data = ParseCSV(csvData);

            if (data.Count == 0)
            {
                Console.WriteLine("No data found in the test data file.");
                return;
            }

            // Convert data to dictionary with names as keys and scores as values
            Dictionary<string, int> scores = new Dictionary<string, int>();
            foreach (var row in data)
            {
                if (row.Length >= 2)
                {
                    string name = row[0];
                    int score;
                    if (int.TryParse(row[1], out score))
                    {
                        if (!scores.ContainsKey(name))
                        {
                            scores.Add(name, score);
                        }
                        else
                        {
                            // Update the score if a higher score is found
                            if (score > scores[name])
                            {
                                scores[name] = score;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid score for {name}: {row[1]}");
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid data: {string.Join(",", row)}");
                }
            }

            if (scores.Count == 0)
            {
                Console.WriteLine("No valid scores found in the input file.");
                return;
            }

            // Find the maximum score
            int maxScore = int.MinValue;
            string topScorer = "";
            foreach (var kvp in scores)
            {
                if (kvp.Value > maxScore)
                {
                    maxScore = kvp.Value;
                    topScorer = kvp.Key;
                }
            }

            // Output the top scorer and their score
            Console.WriteLine($"Top Scorer: {topScorer}");
            Console.WriteLine($"Score: {maxScore}");
        }

        // Function to read input data from a file
        static string ReadInputFile(string filePath)
        {
            string data = "";

            try
            {
                // Read all text from the file
                data = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading input file: {ex.Message}");
            }

            return data;
        }

        // Function to parse CSV data from string
        static List<string[]> ParseCSV(string csvData)
        {
            List<string[]> data = new List<string[]>();

            string[] rows = csvData.Trim().Split('\n');
            foreach (string row in rows)
            {
                string[] fields = row.Trim().Split(',');
                data.Add(fields);
            }

            return data;
        }
    }
}
