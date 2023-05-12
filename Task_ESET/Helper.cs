using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_ESET
{
    /// <summary>
    /// Represents a class with helper methods for the Task_ESET project.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Checks a given parameter for unusual characters. Returns true if any unusual character is found. Otherwise, returns false.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool FindUnusualChar(string line)
        {
            try
            {
                foreach (char ch in line)
                {
                    if (Char.IsControl(ch) || (Char.IsWhiteSpace(ch) && !Char.IsSeparator(ch)) || (ch > 127 && ch != '»'))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return false;
        }

        /// <summary>
        /// Calculates the time elapsed since a given parameter value and returns the result as a string.
        /// </summary>
        /// <param name="t"></param>
        public string TimeCatcher(DateTime t)
        {
            return "\nTime response: " + (DateTime.Now - t).ToString();
        }
    }
}
