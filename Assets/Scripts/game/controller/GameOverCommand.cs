
using System;
using UnityEngine;
using tikal.main;
using strange.extensions.command.impl;

namespace tikal.game
{
	public class GameOverCommand : EventCommand
	{
		[Inject]
		public IScore scoreKeeper{get;set;}
		
		[Inject]
		public IGameTimer gameTimer{get;set;}
		
		public override void Execute()
		{
			gameTimer.Stop();
			
			//dispatch between contexts
			Debug.Log("GAME OVER...dispatch across contexts");
			
			dispatcher.Dispatch(MainEvent.GAME_COMPLETE, scoreKeeper.score);
		}
	}
}

