using Title.Game.Manager;

namespace Title.Game.Command
{
	public abstract class Command : ICommand, IReset
	{
		public int CommandId { get; }
		public abstract CommandType CommandType { get; }
		
		public bool IsDone { get; protected set; }

		public Command()
		{
			CommandId = GameManager.Instance.GetCommandId();
		}
		
		public abstract void Execute();

		void IReset.Reset()
		{
			IsDone = false;
		}
		
		public override string ToString()
		{
			return $"{CommandType.ToString()} {CommandId} Command";
		}
	}
	
	public class Move : Command
	{
		public override CommandType CommandType => CommandType.Move;

		public Move(): base()
		{
			
		}
		
		public override void Execute()
		{
			IsDone = true;
		}
	}
	
	public class Attack : Command
	{
		public override CommandType CommandType => CommandType.Attack;
		
		public Attack(): base()
		{
			
		}
		
		public override void Execute()
		{
			IsDone = true;
		}
	}
	
	public class Rotate : Command
	{
		public override CommandType CommandType => CommandType.Rotate;
		
		public Rotate(): base()
		{
			
		}
		
		public override void Execute()
		{
			IsDone = true;
		}
	}
}