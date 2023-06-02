using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Task_ESET.Models;

namespace Task_ESET
{
    /// <summary>
    /// Represents the main method for parsing input data.
    /// </summary>
    public class Parser
    {
        public Helper _helper;
        public Parser(Helper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// parses and processes text files, creates objects based on the InfectedFile and Child classes, 
        /// and returns a Tuple consisting of List<InfectedFile> and DateTime t - representing the start of the parsing.
        /// </summary>
        /// <returns></returns>
        public Tuple<List<InfectedFile>, DateTime> Parse()
        {
            bool cnt = false;

            List<InfectedFile> list = new List<InfectedFile>();

            DateTime t;

            do
            {
                Console.Write("Enter the file address: ");
                string filePath = Console.ReadLine();

                t = DateTime.Now;

                if (filePath != null && filePath.StartsWith("\""))
                {
                    filePath = filePath.Replace("\"", "");
                }

                string startOfTheLine = "name";

                try
                {
                    if (filePath == null || filePath == "" || filePath == string.Empty || !File.Exists(filePath))
                    {
                        Console.WriteLine("Please enter the correct file address");
                        continue;
                    }

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            var lastRecord = list.LastOrDefault();

                            if (line.StartsWith(startOfTheLine))
                            {
                                string pattern = @"""([^""]*)""";
                                Regex regex = new Regex(pattern);
                                Match match = regex.Match(line);
                                Group a = match.Groups[1];

                                match = match.NextMatch();
                                Group b = match.Groups[1];

                                match = match.NextMatch();
                                Group c = match.Groups[1];

                                match = match.NextMatch();
                                Group d = match.Groups[1];

                                string s = a.ToString();
                                
                                if (lastRecord != null && a.ToString().Contains("»"))
                                {
                                    string packerPattern = @"»\s*(.*?)\s*»";
                                    Match packerMatch = Regex.Match(a.ToString(), packerPattern);

                                    Child child = new Child()
                                    {
                                        Name = "Name: " + a.ToString(),
                                        Threat = b.ToString(),
                                        Action = ", Action: " + c.ToString(),
                                        Info = ", Info: " + d.ToString(),
                                        Packer = packerMatch.Groups[1].Value
                                    };

                                    lastRecord.IsArchive = true;
                                    lastRecord.Children.Add(child);

                                    if (_helper.FindUnusualChar(child.Name) == true)
                                    {
                                        lastRecord.Info = ", Info: May contain incomplete or damaged data";
                                    }
                                }
                                else
                                {
                                    InfectedFile infectedFile = new InfectedFile()
                                    {
                                        Name = "Name: " + a.ToString(),
                                        Threat = ", Threat: " + b.ToString(),
                                        Action = ", Action: " + c.ToString(),
                                        Info = ", Info: " + d.ToString()
                                    };

                                    if (_helper.FindUnusualChar(infectedFile.Name) == true)
                                    {
                                        infectedFile.Info = ", Info: May contain incomplete or damaged data";
                                    }

                                    list.Add(infectedFile);

                                }
                            }
                        }
                    }

                    cnt = true;
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (cnt != true);

            return Tuple.Create(list, t);
        }
    }
}