/// Turns off the AudioListener in this scene. Fired only if the present Context isn't firstContext

using System;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.context.api;

namespace tikal.common
{
	public class KillAudioListenerCommand : Command
	{
		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{ get; set; }

		public override void Execute()
		{
			AudioListener[] audioListeners = contextView.GetComponentsInChildren<AudioListener> ();
			int aa = audioListeners.Length;
			for (int a = 0; a < aa; a++)
			{
				audioListeners[a].enabled = false;
			}
		}
	}
}