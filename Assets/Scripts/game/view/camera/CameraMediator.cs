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
			/*model.SetState(state);
			
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
			}*/
		}
	
	}
}
