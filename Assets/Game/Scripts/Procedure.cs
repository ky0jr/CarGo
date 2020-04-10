using System.Collections.Generic;
using System.Diagnostics;
using Title.Game.Manager;

namespace Title.Game.Command
{
	public class Procedure: ICommand
	{
		public int CommandId { get; }

		public CommandType CommandType => CommandType.Procedure;
		private List<ICommand> Commands { get; }

		public bool IsDone { get; private set; }

		private ICommand currentCommand;

		private Queue<ICommand> QueueCommand;
		
		public Procedure(List<ICommand> commands)
		{
			Commands = new List<ICommand>();
			AddCommand(commands);
			CommandId = GameManager.Instance.GetCommandId();
		}

		public void Execute()
		{
			if (QueueCommand.Count == 0)
				IsDone = true;
			
			if(IsDone)
				return;

			if (currentCommand is null || currentCommand.IsDone)
			{
				currentCommand = QueueCommand.Dequeue();
				UnityEngine.Debug.Log(currentCommand.ToString());
			}

			if (!currentCommand.IsDone)
			{
				if (currentCommand is Procedure procedure)
				{
					procedure.Reset();
					procedure.Execute();
				}
				else
				{
					currentCommand.Execute();
				}
			}
		}
		
		public void Reset()
		{
			QueueCommand = new Queue<ICommand>(Commands);
			
			
		}
		
		public void AddCommand(IEnumerable<ICommand> commands)
		{
			Commands.AddRange(commands);
			QueueCommand = new Queue<ICommand>(Commands);
		}

		public void AddCommand(ICommand command)
		{
			Commands.Add(command);
			QueueCommand = new Queue<ICommand>(Commands);
		}
		
		public override string ToString()
		{
			return $"Procedure {CommandId}";
		}
	}
}