using System;
using System.Collections;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace tikal.game {

	public class CameraMediator : Mediator {

		[Inject]
		public ICamera model { get; set; }
		
		[Inject]
		public CameraView view { get; set; }

		[Inject]
		public CameraStateSignal cameraStateSignal { get; set; }
		[Inject]
		public FlythroughCompleteSignal flythroughCompleteSignal { get; set; }
		
		public override void OnRegister() {
			AddListeners();
			view.init();
			
			// demo
			resetDemoValues();
		}
		
		public override void OnRemove() {
			RemoveListeners();
		}
		
		private void AddListeners() {
			cameraStateSignal.AddListener(onCameraStateChanged);
		}
		
		private void RemoveListeners() {
			cameraStateSignal.RemoveListener(onCameraStateChanged);
		}

		private void onCameraStateChanged(CameraState state) {
			model.SetState(state);
			
			view.stateChange(state);
			if (state == CameraState.CINEMATIC) {
				StartCoroutine(flyToWaypoints());
				// demo
				view.beginFlythrough();
				cinematicStart = true;
				
			} else if (state == CameraState.KEYBOARD) {
				view.attachToTarget();
				// demo
				characterAttach = true;
			}
		}
		
		private IEnumerator flyToWaypoints() {
			CameraWaypoint waypoint;
			int i = 0,
			len = model.waypoints.Count;
			
			for (; i < len; i++) {
				waypoint = model.waypoints[i];
				view.flyToWaypoint(waypoint);
				
				yield return new WaitForSeconds(waypoint.duration + waypoint.delay);
				
				// demo
				currentWaypoint++;
			}
			
			flythroughCompleteSignal.Dispatch();
			
			// demo
			cinematicEnd = true;
			yield return new WaitForSeconds(2f);
			initialSequence = false;
			currentWaypoint = -1;
			
			yield return null;
		}
		
		//-----------------------------------
		//- DEMO DEBUG CONTROLS/INFORMATION -
		//-----------------------------------
		
		private bool initialSequence;
		private bool cinematicStart;
		private bool cinematicEnd;
		private bool characterAttach;
		private int currentWaypoint;
		private float sliderDistance;
		private float sliderHeight;
		private float sliderSpeed;
		private bool lookAtTarget;
		
		private void resetDemoValues() {
			initialSequence = true;
			cinematicStart = false;
			cinematicEnd = false;
			characterAttach = false;
			currentWaypoint = 0;
			sliderDistance = 15f;
			sliderHeight = 8f;
			sliderSpeed = 2.5f;
			lookAtTarget = false;
			
			view.setCameraDistance(sliderDistance);
			view.setCameraHeight(sliderHeight);
			view.setCameraSpeed(sliderSpeed);
			view.setLookAtTarget(lookAtTarget);
		}
		
		void OnGUI() {
						
			GUI.Box(new Rect(10, Screen.height - 50, 100, 40), "cf Camera Mediator");
		}

	}
}
