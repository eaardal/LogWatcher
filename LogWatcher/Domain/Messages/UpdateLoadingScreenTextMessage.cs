namespace LogWatcher.Domain.Messages
{
    class UpdateLoadingScreenTextMessage
    {
        public string Msg { get; set; }
        public string Identifier { get; set; }

        public UpdateLoadingScreenTextMessage(string identifier, string msg)
        {
            Identifier = identifier;
            Msg = msg;
        }
    }
}
