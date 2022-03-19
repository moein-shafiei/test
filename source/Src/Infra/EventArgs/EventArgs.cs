namespace DotFramework.Infra
{
    public class EventArgs<TArgument> : System.EventArgs
    {
        public readonly TArgument Argument;

        public EventArgs() : 
            this(default(TArgument))
        {
        }

        public EventArgs(TArgument argument)
        {
            Argument = argument;
        }
    }
}
