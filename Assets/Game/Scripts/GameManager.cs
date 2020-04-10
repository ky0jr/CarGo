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

		private Queue<ICommand> QueueCommand;

		private Transform _transform;

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
			
			procedure.AddCommand(procedure);
			
			Commands = new List<ICommand>
			{
				/*new Move(),
				new Rotate(),
				new Move(),
				new Attack(),*/
				procedure
			};
			QueueCommand = new Queue<ICommand>(Commands);
			
			Debug.Log(Commands.Count);

			Transform temp = _transform is null ? transform : _transform;
		}

		private void Update()
		{
			if (currentCommand is null || currentCommand.IsDone)
			{
				if (QueueCommand.Count == 0)
				{
					return;
				}
				currentCommand = QueueCommand.Dequeue();
				Debug.Log(currentCommand.ToString());
			}
			
			if (!currentCommand.IsDone)
			{
				currentCommand.Execute();
			}
		}
	}
}