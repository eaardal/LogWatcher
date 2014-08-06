using System;

namespace LogWatcher.Domain.Messages.ErrorMessages
{
    public class GenericExceptionMessage
    {
        public GenericExceptionMessage()
        {
            
        }

        public GenericExceptionMessage(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; set; } 
    }
}