using System;
using UnityEngine;
using strange.extensions.command.impl;
using tikal.main;

namespace tikal.game
{
	public class RemoveSocialContextCommand : EventCommand
	{
		
		public override void Execute()
		{
			dispatcher.Dispatch(MainEvent.REMOVE_SOCIAL_CONTEXT);
		}
	}
}

