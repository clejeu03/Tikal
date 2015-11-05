using System;
using UnityEngine;
using strange.extensions.command.impl;

namespace tikal.game
{
	public class ReplayGameCommand : EventCommand
	{
		[Inject]
		public IScore scoreKeeper{get;set;}
		
		[Inject]
		public IGameTimer gameTimer{get;set;}
		
		public override void Execute()
		{
			scoreKeeper.Reset();
			dispatcher.Dispatch(GameEvent.RESTART_GAME);
			gameTimer.Start();
		}
	}
}

