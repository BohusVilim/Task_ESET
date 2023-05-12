using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_ESET.Models;

namespace Task_ESET
{
    /// <summary>
    /// Represents the main method for printing and writing data.
    /// </summary>
    public class Printer
    {
        public Parser _parser;
        public Helper _helper;
        public Printer(Parser parser, Helper helper)
        {
            _parser = parser;
            _helper = helper;
        }

        /// <summary>
        /// Processes the parameter data from List<InfectedFiles> list and prints the relevant information to the console.
        /// Also writes these data to a text file.
        /// </summary>
        /// <param name="list"></param>
        public void Print(List<InfectedFile> list, DateTime t)
        {
            string filePath = "C:\\Users\\bohus\\OneDrive\\Počítač\\ESET\\ESET_Parser task OUTPUT.txt";

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    foreach (var item in list)
                    {
                        if (item.Threat != ", Threat: is OK")
                        {
                            if (item.IsArchive == true)
                            {
                                Console.Write("\n" + item.Name
                                    + ", Is Archive: yes"
                                    + ", Packers: ");

                                streamWriter.Write("\n" + item.Name
                                    + ", Is Archive: yes"
                                    + ", Packers: ");

                                var packers = item.Children.Select(a => a.Packer).Distinct();
                                foreach (var packer in packers)
                                {
                                    Console.Write(packer);
                                    streamWriter.Write(packer);
                                }

                                var threats = item.Children.Where(b => b.Threat != "is OK").Select(c => c.Threat).Distinct();
                                Console.Write(", Threats: ");
                                streamWriter.Write(", Threats: ");

                                foreach (var threat in threats)
                                {
                                    Console.Write(threat);
                                    streamWriter.Write(threat);
                                }

                                if (item.Action != ", Action: ")
                                {
                                    Console.Write(item.Action);
                                    streamWriter.Write(item.Action);
                                }

                                if (item.Info != ", Info: ")
                                {
                                    Console.Write(item.Info);
                                    streamWriter.Write(item.Info);
                                }
                            }
                            else
                            {
                                Console.Write("\n" + item.Name + item.Threat);
                                streamWriter.Write("\n" + item.Name + item.Threat);

                                if (item.Action != ", Action: ")
                                {
                                    Console.Write(item.Action);
                                    streamWriter.Write(item.Action);
                                }

                                if (item.Info != ", Info: ")
                                {
                                    Console.Write(item.Info);
                                    streamWriter.Write(item.Info);
                                }
                            }
                        }
                    }

                    Console.WriteLine(_helper.TimeCatcher(t));
                    streamWriter.WriteLine(_helper.TimeCatcher(t));
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
