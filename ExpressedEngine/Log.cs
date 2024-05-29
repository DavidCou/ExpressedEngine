using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    /// <summary>
    /// Used to send different types of messages to the console
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Writes a regular message to the console
        /// </summary>
        /// <param name="msg"> The desired message </param>
        public static void Normal(string msg)
        {
            // Sets the font color and writes the message
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[MESSAGE] - {msg}");
            // Note: It is best to set the text color bac to the defualt color after
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Writes an information message to the console
        /// </summary>
        /// <param name="msg"> The desired message </param>
        public static void Info(string msg)
        {
            // Sets the font color and writes the message
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INFO] - {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Writes an warning message to the console
        /// </summary>
        /// <param name="msg"> The desired message </param>
        public static void Warning(string msg)
        {
            // Sets the font color and writes the message
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING] - {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Writes an error message to the console
        /// </summary>
        /// <param name="msg"> The desired message </param>
        public static void Error(string msg)
        {
            // Sets the font color and writes the message
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error] - {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
