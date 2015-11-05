using System;
using UnityEngine;
using strange.extensions.command.impl;

namespace tikal.game
{
	public class UpdateScoreCommand : EventCommand
	{
		[Inject]
		public IScore scoreKeeper{get;set;}
		
		public override void Execute()
		{
			int increment = (int)evt.data;
			int newScore = scoreKeeper.AddToScore(increment);
			dispatcher.Dispatch(GameEvent.SCORE_CHANGE, newScore);
		}
	}
}

