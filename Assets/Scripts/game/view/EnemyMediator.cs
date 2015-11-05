/// Ship mediator
/// =====================
/// Make your Mediator as thin as possible. Its function is to mediate
/// between view and app. Don't load it up with behavior that belongs in
/// the View (listening to/controlling interface), Commands (business logic),
/// Models (maintaining state) or Services (reaching out for data).

using System;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.mediation.impl;

namespace tikal.game
{
	public class EnemyMediator : EventMediator
	{
		[Inject]
		public EnemyView view{ get; set;}
		
		public override void OnRegister()
		{
			UpdateListeners(true);
			view.init ();
		}
		
		public override void OnRemove()
		{
			UpdateListeners(false);
		}
		
		private void UpdateListeners(bool value)
		{
			view.dispatcher.UpdateListener(value, EnemyView.CLICK_EVENT, onViewClicked);
			dispatcher.UpdateListener( value, GameEvent.GAME_UPDATE, onGameUpdate);
			dispatcher.UpdateListener( value, GameEvent.GAME_OVER, onGameOver);
			
			dispatcher.AddListener(GameEvent.RESTART_GAME, onRestart);
		}
		
		private void onViewClicked()
		{
			dispatcher.Dispatch(GameEvent.ADD_TO_SCORE, 10);
		}
		
		private void onGameUpdate()
		{
			view.updatePosition();
		}
		
		private void onGameOver()
		{
			UpdateListeners(false);
		}
		
		private void onRestart()
		{
			OnRegister();
		}
	}
}

