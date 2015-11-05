/// MainContext maps the Context for the top-level component.


using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using tikal.game;

namespace tikal.main
{
	public class MainContext : MVCSContext
	{

		public MainContext (MonoBehaviour view) : base(view)
		{
		}

		public MainContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
		{
		}
		
		protected override void mapBindings()
		{
			//Any event that fire across the Context boundary get mapped here.
			crossContextBridge.Bind(MainEvent.GAME_COMPLETE);
			crossContextBridge.Bind(MainEvent.REMOVE_SOCIAL_CONTEXT);
			crossContextBridge.Bind(GameEvent.RESTART_GAME);
			
			commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
			commandBinder.Bind(MainEvent.LOAD_SCENE).To<LoadSceneCommand>();
			
			commandBinder.Bind (MainEvent.GAME_COMPLETE).To<GameCompleteCommand>();
			
			//I'm not actually doing this anywhere in this example (probably I'll add something soon)
			//but here's how you'd cross-context map a shared model or service
			//injectionBinder.Bind<ISomeInterface>().To<SomeInterfaceImplementer>().ToSingleton().CrossContext();
		}
	}
}

