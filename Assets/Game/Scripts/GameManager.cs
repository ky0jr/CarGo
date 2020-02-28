using System.Collections.Generic;
using Title.Game.Command;
using UnityEngine;

namespace Title.Game.Manager
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance => instance;

		private static GameManager instance;
		
		private List<ICommand> Commands;

		private ICommand currentCommand;

		private int commandCounter = 0;

		public int GetCommandId() => ++commandCounter;
		
		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(this);
			} else {
				instance = this;
			}
		}

		private void Start()
		{
			Procedure procedure = new Procedure(new List<ICommand>{new Attack(), new Attack()});
			
			procedure.Commands.Add(procedure);
			
			Commands = new List<ICommand>
			{
				/*new Move(),
				new Rotate(),
				new Move(),
				new Attack(),*/
				procedure
			};
			
			Debug.Log(Commands.Count);
		}

		private void Update()
		{
			foreach (var command in Commands)
			{
				currentCommand = command;
				while (currentCommand.IsDone == false)
				{
					Debug.Log(currentCommand.ToString());
					currentCommand.Execute();
				}
			}
		}
	}
}