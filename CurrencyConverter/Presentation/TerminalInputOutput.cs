using System;

namespace CurrencyConverter.Presentation
{
    public interface IInputOutput
    {
        void   WriteInfo(string  message);
        void   WriteEvent(string message);
        void   WriteError(string message);
        
        string Read();
    }

    public class TerminalInputOutput : IInputOutput
    {
        private void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteInfo(string  message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Write(message);
        }

        public void WriteEvent(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Write(message);
        }

        public void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Write(message);
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
