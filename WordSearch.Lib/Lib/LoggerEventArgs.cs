using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch.Models.Lib
{
    public class LoggerEventArgs
    {
        public string Message { get; }
        public LoggerEventArgs(string message)
        {
            Message = message;
        }
    }
}
