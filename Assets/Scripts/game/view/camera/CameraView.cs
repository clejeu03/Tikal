using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace tikal.game {
	
	public class CameraView : View {
		
		private const float DISTANCE = 15f;
		private const float HEIGHT = 8f;
		private const float SPEED = 10.0f;
		
		public GameObject target;
		
		// demo controls
		private float cameraDistance = DISTANCE;
		private float cameraHeight = HEIGHT;
		private float cameraSpeed = SPEED;
		private bool cameraLookAt = false;

		private Vector3 nextPosition;
		private Vector3 nextEulerAngles;
		private Transform _transform;
		private CameraState _state;
		private CameraWaypoint _waypoint;
		private float _xAngle;
		private float _yAngle;

		internal void init() {
			_transform = transform;
			nextPosition = new Vector3(cameraDistance, cameraHeight, -cameraDistance);
			_state = CameraState.KEYBOARD;

		}


		void Update (){
			if (_state == CameraState.KEYBOARD) {
				nextPosition = _transform.position;
				nextEulerAngles = _transform.eulerAngles;
				Vector3 camForward = Vector3.Scale (_transform.forward, new Vector3(1,0,1)).normalized;
				Vector3 camRight = Vector3.Scale (_transform.right, new Vector3(1,0,1)).normalized;
				// -------------  Receive keyboard events ------------- //
				if (Input.GetKey (KeyCode.Q) || Input.GetKey (KeyCode.LeftArrow)) {
					nextPosition -= camRight*cameraSpeed;
				}
				if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
					nextPosition += camRight*cameraSpeed;
				}
				if (Input.GetKey (KeyCode.Z) || Input.GetKey (KeyCode.UpArrow)) {
					nextPosition += camForward*cameraSpeed;
				}
				if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
					nextPosition -= camForward*cameraSpeed;
				}
				// -------------  Receive mouse events ------------- //
				// UNITY Conventions :
				// Mouse Button 0 = left click
				// Mouse button 1 = right click
				// Mouse button 2 = middle click

				Vector3 camUp = Vector3.Scale (_transform.up, new Vector3(1,0,1)).normalized;
				if(Input.GetMouseButton(1)){
					_xAngle = _transform.eulerAngles.y;
					_yAngle = _transform.eulerAngles.x;
					_xAngle += Input.GetAxis("Mouse X") * cameraSpeed;
					_yAngle -= Input.GetAxis("Mouse Y") * cameraSpeed;
					
					_yAngle = Mathf.Clamp(_yAngle, -360, 360);
					
					Quaternion rotation = Quaternion.Euler(_yAngle, _xAngle, 0);
					nextPosition += rotation * Vector3.zero;
					
					_transform.rotation = rotation;
				}if(Input.GetKey(KeyCode.A)){ 
					_transform.RotateAround( Input.mousePosition, camUp, 20 * Time.deltaTime);
				}else if (Input.GetKey(KeyCode.E)){
					_transform.RotateAround(Input.mousePosition, camUp, -20 * Time.deltaTime);
				}
			}
		}
		
		void LateUpdate() {
			if (_state == CameraState.KEYBOARD) {
				// apply keyboard events
				updateKeyboardCamera(nextPosition);


				
			} else if (_state == CameraState.CINEMATIC) {
				updateCinematicCamera();
			}
		}
		
		internal void stateChange(CameraState state) {
			_state = state;
		}
		
		internal void flyToWaypoint(CameraWaypoint waypoint) {
			_transform.position = waypoint.from.position;
			_transform.localRotation = waypoint.from.rotation;
			
			_waypoint = waypoint;
		}
		
		internal void beginFlythrough() {
			// demo - disable controls
			//target.GetComponent<ThirdPersonController>().isControllable = false;
		}
		
		internal void attachToTarget() {
			// demo - enable controls
			//target.GetComponent<ThirdPersonController>().isControllable = true;
		}
		
		private void updateCinematicCamera() {
			float t = _waypoint.duration / 10f * Time.deltaTime;
			
			_transform.position = Vector3.Lerp(_transform.position, _waypoint.to.position, t);
			_transform.localRotation = Quaternion.Slerp(_transform.localRotation, _waypoint.to.rotation, t);
		}
		
		private void updateKeyboardCamera(Vector3 target) {
			float t = cameraSpeed * Time.deltaTime;
			
			_transform.position = Vector3.Lerp(_transform.position, target, t);
			
			if (cameraLookAt) {
				_transform.rotation = Quaternion.Slerp (_transform.rotation, Quaternion.LookRotation(target - _transform.position), t);
			} else {
				//_transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(new Vector3(30f, -45f, 0)), t);
			}
		}
		
		//-----------------------------------
		//- DEMO CONTROLS                   -
		//-----------------------------------
		
		internal void setCameraDistance(float distance) {
			cameraDistance = distance;
		}

		internal void setCameraHeight(float height) {
			cameraHeight = height;
		}
		
		internal void setCameraSpeed(float speed) {
			cameraSpeed = speed;
		}
		
		internal void setLookAtTarget(bool lookAt) {
			cameraLookAt = lookAt;
		}
		
	}
	
}