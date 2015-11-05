/// The Root is the entry point to a strange-enabled Unity3D app.
/// ===============
/// 
/// Attach this MonoBehaviour to a GameObject at the top of a scene in main.unity.
/// 
/// Main is responsible for setting up the main context and loading the other components.

using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;

namespace tikal.main
{
	public class MainRoot : ContextView
	{
	
		void Awake()
		{
			//Instantiate the context, passing it this instance.
			context = new MainContext(this);
		}
	}
}

