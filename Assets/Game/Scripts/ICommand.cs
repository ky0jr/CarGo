using System.Collections.Generic;

namespace Title.Game.Command
{
	public enum CommandType
	{
		Move,
		Rotate,
		Attack,
		Procedure
	}
	
	public interface ICommand
	{
		int CommandId { get; }
		
		bool IsDone { get; }
		
		void Execute();

		string ToString();
	}

}
