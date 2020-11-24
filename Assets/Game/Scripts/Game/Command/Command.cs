namespace Title.Game.Command
{
    public class Command
    {
        public readonly CommandType CommandType;

        public Command(CommandType commandType)
        {
            CommandType = commandType;
        }
    }

}