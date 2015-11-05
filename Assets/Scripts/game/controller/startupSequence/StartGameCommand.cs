/// Kicks off the app, directly after context binding

using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

namespace tikal.game
{
	public class StartGameCommand : Command
	{
		[Inject]
		public IGameTimer timer{get;set;}
		
		public override void Execute()
		{
			timer.Start();
		}
	}
}

