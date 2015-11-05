/// MainContext maps the Context for the top-level component.
/// ===========
/// I'm assuming here that you've already gone through myfirstproject, or that
/// you're experienced with strange.

using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using tikal.common;

namespace tikal.game
{
	public class GameContext : MVCSContext
	{

		public GameContext (MonoBehaviour view) : base(view)
		{
		}

		public GameContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
		{
		}
		
		protected override void mapBindings()
		{
			injectionBinder.Bind<IScore>().To<ScoreModel>().ToSingleton();
			
			mediationBinder.Bind<ShipView>().To<ShipMediator>();
			mediationBinder.Bind<EnemyView>().To<EnemyMediator>();
			mediationBinder.Bind<ScoreboardView>().To<ScoreboardMediator>();

			//We can do different things depending on whether or not this is the first context to instantiate
			//In this case, when we're operating within a larger context we want to kill the AudioListener
			if (this == Context.firstContext)
			{
				commandBinder.Bind (ContextEvent.START)
					.To<StartAppCommand> ()
					.To<StartGameCommand> ()
					.Once ().InSequence ();
			}
			else
			{
				commandBinder.Bind (ContextEvent.START)
					.To<KillAudioListenerCommand>()
					.To<StartAppCommand> ()
					.To<StartGameCommand> ()
					.Once ().InSequence ();
			}

			
			commandBinder.Bind(GameEvent.ADD_TO_SCORE).To<UpdateScoreCommand>();
			commandBinder.Bind(GameEvent.SHIP_DESTROYED).To<ShipDestroyedCommand>();
			commandBinder.Bind(GameEvent.GAME_OVER).To<GameOverCommand>();
			commandBinder.Bind(GameEvent.REPLAY).To<ReplayGameCommand>();
			commandBinder.Bind(GameEvent.REMOVE_SOCIAL_CONTEXT).To<RemoveSocialContextCommand>();
		}
	}
}

