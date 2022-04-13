namespace Core.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException(string[] Message) : base(Message.First())
        {
        }
    }
}
