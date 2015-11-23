/// Just a simple MonoBehaviour Click Detector

using System;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace tikal.game
{
	public class DoubleClickDetector : EventView
	{
		public const string DOUBLE_CLICK = "DOUBLE_CLICK";

		private int numberOfClicks = 0;
		private float timer = 0.0f;

		void IsDoubleClick(){
			bool isDoubleClick = numberOfClicks == 2;
			if(isDoubleClick){
				numberOfClicks = 0;
				dispatcher.Dispatch(DOUBLE_CLICK);
			}
			return isDoubleClick;
		}

		void Update(){
			timer += Time.deltaTime;

			if(timer > 0.3f){
				numberOfClicks = 0;
			}
		}
	}
}

