namespace Distributor.Messaging
{
    public static class Exchange
    {
        public static string GetName<TMessage>() where TMessage : class, IMessage
        {
            return typeof(TMessage).FullName;
        }
    }
}