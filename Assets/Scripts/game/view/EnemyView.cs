using System;
using System.Collections;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace tikal.game
{
	public class EnemyView : EventView
	{
		internal const string CLICK_EVENT = "CLICK_EVENT";
		
		private float theta = 0f;
		private Vector3 basePosition;
		
		//Publicly settable from Unity3D
		public float edx_WobbleForce = .4f;
		public float edx_WobbleIncrement = .1f;

		private ClickDetector clicker;
		
		internal void init()
		{
			gameObject.AddComponent<ClickDetector>();
			clicker = gameObject.GetComponent<ClickDetector>();
			StartCoroutine (addClicker ());
		}

		private IEnumerator addClicker()
		{
			yield return null;
			clicker.dispatcher.AddListener(ClickDetector.CLICK, onClick);
		}
		
		internal void updatePosition()
		{
			wobble();
		}
		
		void onClick()
		{
			dispatcher.Dispatch(CLICK_EVENT);
		}
		
		void wobble()
		{
			theta += edx_WobbleIncrement;
			gameObject.transform.Rotate(Vector3.forward, edx_WobbleForce * Mathf.Sin(theta));
		}
	}
}

