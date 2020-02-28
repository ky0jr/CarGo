using System.Collections.Generic;
using Title.Game.Manager;
using UnityEngine;

namespace Title.Game.Command
{
	public class Procedure: IProcedure
	{
		public int CommandId { get; }

		public CommandType CommandType => CommandType.Procedure;
		public List<ICommand> Commands { get; }

		private event System.Action ResetEvent;
		
		public bool IsDone { get; private set; }

		private ICommand currentCommand;

		private bool isReset = false;
		
		public Procedure(List<ICommand> commands)
		{
			Commands = commands;
			foreach (var command in Commands)
			{
				if (command is IReset reset)
				{
					ResetEvent += () => reset.Reset();
				}
			}
			
			CommandId = GameManager.Instance.GetCommandId();
		}

		public void Execute()
		{
			if(IsDone)
				return;
			
			foreach (var command in Commands)
			{
				currentCommand = command;
				if (currentCommand.CommandId == this.CommandId)
				{
					isReset = true;
					break;
				}
				while (currentCommand.IsDone == false)
				{
					Debug.Log(currentCommand.ToString());
					currentCommand.Execute();
				}
			}

			if (!isReset)
			{
				IsDone = true;
			}
			else
			{
				ResetEvent?.Invoke();
				isReset = false;
			}
		}

		public override string ToString()
		{
			return $"Procedure {CommandId}";
		}
	}
}