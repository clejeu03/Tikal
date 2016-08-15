using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tikal.game {
	
	public class CameraView : MonoBehaviour {

		internal const string DOUBLE_CLICK_EVENT = "DOUBLE_CLICK_EVENT";

		// -------------------------- Configuration --------------------------
		public Terrain terrain;
		public float panSpeed = 15.0f;
		public float zoomSpeed = 100.0f;
		public float rotationSpeed = 50.0f;

		public float mousePanMultiplier = 0.1f;
		public float mouseRotationMultiplier = 0.2f;
		public float mouseZoomMultiplier = 5.0f;

		public float minZoomDistance = 20.0f;
		public float maxZoomDistance = 200.0f;
		public float smoothingFactor = 0.1f;
		public float goToSpeed = 0.1f;

		public bool useKeyboardInput = true;
		public bool useMouseInput = true;
		public bool adaptToTerrainHeight = true;
		public bool increaseSpeedWhenZoomedOut = true;
		public bool correctZoomingOutRatio = true;
		public bool smoothing = true;
		public bool allowDoubleClickMovement = false;

		public GameObject objectToFollow;
		public GameObject objectToGoTo;
		public Vector3 cameraTarget;

		// -------------------------- Private Attributes --------------------------
		private float currentCameraDistance;
		private Vector3 lastMousePos;
		private Vector3 lastPanSpeed = Vector3.zero;
		private Vector3 goingToCameraTarget = Vector3.zero;
		private bool doingAutoMovement = false;
		private DoubleClickDetector doubleClicker;

		// -------------------------- Public Methods --------------------------
		void Start(){
			currentCameraDistance = minZoomDistance + ((maxZoomDistance - minZoomDistance) / 2.0f);
			lastMousePos = Vector3.zero;
			gameObject.AddComponent<DoubleClickDetector>();
			doubleClicker = gameObject.GetComponent<DoubleClickDetector>();
		}

		private void OnDoubleClick()
		{
			updateDoubleClick ();
		}

		void Update(){
			updatePanning();
			updateRotation();
			updateZooming();
			updatePosition();
			updateAutoMovement();
			lastMousePos = Input.mousePosition;

		}

		public void goTo(Vector3 position){
			doingAutoMovement = true;
			goingToCameraTarget = position;
			objectToFollow = null;
		}

		public void follow(GameObject gameObjectToFollow){
			objectToFollow = gameObjectToFollow;
		}

		public void setSmoothingFactor(float x) {
			Debug.Log("plop" + x );
			smoothingFactor = x;
		}
		
		public void setPanSpeed(float x) {
			panSpeed = x;
		}
		
		public void setRotationSpeed(float x) {
			rotationSpeed = x;
		}
		
		public void setZoomSpeed(float x) {
			zoomSpeed = x;
		}
		
		public void setGoToSpeed(float x) {
			goToSpeed = x;
		}
		
		public void setDoubleClickMovement(bool x) {
			allowDoubleClickMovement = x;
		}

		
		// -------------------------- Private Methods --------------------------
		private void updateDoubleClick(){
			if( (allowDoubleClickMovement == true) && (terrain != null) && (terrain.GetComponent<Collider>() != null)){
				float cameraTargetY = cameraTarget.y;
				Collider collider = terrain.GetComponent<Collider>();
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				Vector3 pos;
				
				if(collider.Raycast(ray, out hit, Mathf.Infinity)){
					pos = hit.point;
					pos.y = cameraTargetY;
					goTo(pos);
				}
			}
		}

		private void updatePanning(){
			Vector3 moveVector = Vector3.zero;
			if(useKeyboardInput){
				if(Input.GetKey(KeyCode.Q)){
					moveVector += new Vector3(-1,0,0);
				}
				if(Input.GetKey(KeyCode.S)){
					moveVector += new Vector3(0, 0, -1);
				}
				if(Input.GetKey(KeyCode.D)){
					moveVector += new Vector3(1, 0, 0);
				}
				if(Input.GetKey(KeyCode.Z)){
					moveVector += new Vector3(0, 0, 1);
				}
				if (Input.GetKey(KeyCode.T)) {
					follow(objectToFollow);
				}
				if (Input.GetKey(KeyCode.G)) {
					goTo(objectToGoTo.transform.position);
				}
			}

			if(useMouseInput){
				if(Input.GetMouseButton(1) && Input.GetKey (KeyCode.LeftShift)){
					Vector3 deltaMousePos = (Input.mousePosition - lastMousePos);
					moveVector += new Vector3(-deltaMousePos.x, 0, -deltaMousePos.y)* mousePanMultiplier;
				}
			}

			if(moveVector != Vector3.zero){
				objectToFollow = null;
				doingAutoMovement = false;
			}

			Vector3 effectivePanSpeed = moveVector;
			if(smoothing){
				effectivePanSpeed = Vector3.Lerp(lastPanSpeed, moveVector, smoothingFactor);
				lastPanSpeed = effectivePanSpeed;
			}

			float oldRotation = transform.localEulerAngles.x;
			Vector3 localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
			transform.localEulerAngles = localEulerAngles;
			float panMultiplier = increaseSpeedWhenZoomedOut ? (Mathf.Sqrt (currentCameraDistance )) : 1.0f;
			cameraTarget  = cameraTarget + transform.TransformDirection(effectivePanSpeed) * panSpeed * panMultiplier * Time.deltaTime;
			transform.localEulerAngles = new Vector3(oldRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}

		private void updateRotation(){
			float deltaAngleH = 0.0f;
			float deltaAngleV = 0.0f;

			if(useKeyboardInput){
				if(Input.GetKey(KeyCode.A)){
					deltaAngleH = 1.0f;
				}
				if(Input.GetKey (KeyCode.E)){
					deltaAngleV = -1.0f;
				}
			}

			if(useMouseInput){
				if(Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift)){
					Vector3 deltaMousePos = (Input.mousePosition - lastMousePos);
					deltaAngleH += deltaMousePos.x * mouseRotationMultiplier;
					deltaAngleV -= deltaMousePos.y * mouseRotationMultiplier;
				}
			}

			float angleY  = transform.localEulerAngles.y + deltaAngleH * Time.deltaTime * rotationSpeed;
			float angleX  = Mathf.Min (80.0f, Mathf.Max (5.0f, transform.localEulerAngles.x + deltaAngleV * Time.deltaTime * rotationSpeed));
			transform.localEulerAngles = new Vector3(angleX, angleY, transform.localEulerAngles.z);
		}

		private void updateZooming(){
			float deltaZoom = 0.0f;
			if(useKeyboardInput){
				if(Input.GetKey (KeyCode.F)){
					deltaZoom = 1.0f;
				}
				if(Input.GetKey(KeyCode.R)){
					deltaZoom = -1.0f;
				}
			}

			if(useMouseInput){
				float scroll = Input.GetAxis ("Mouse ScrollWheel");
				deltaZoom -= scroll * mouseZoomMultiplier;
			}

			float zoomedOutRatio = correctZoomingOutRatio ? (currentCameraDistance - minZoomDistance ) / (maxZoomDistance - minZoomDistance) : 0.0f;
			currentCameraDistance = Mathf.Max (minZoomDistance, Mathf.Min(maxZoomDistance, currentCameraDistance + deltaZoom * Time.deltaTime * zoomSpeed * (zoomedOutRatio * 2.0f + 1.0f)));
		}

		private void updatePosition(){
			if(objectToFollow != null){
				cameraTarget = Vector3.Lerp (cameraTarget, objectToFollow.transform.position, goToSpeed);
			}

			transform.position = cameraTarget;
			transform.Translate(Vector3.back * currentCameraDistance);

			if(adaptToTerrainHeight && terrain != null){
				float newHeight = Mathf.Max (terrain.SampleHeight(transform.position) + terrain.transform.position.y + 10.0f, transform.position.y);
				transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
			}
		}

		private void updateAutoMovement(){
			if(doingAutoMovement){
				cameraTarget = Vector3.Lerp (cameraTarget, goingToCameraTarget, goToSpeed);
				if(Vector3.Distance(goingToCameraTarget, cameraTarget) < 1.0f){
					doingAutoMovement = false;
				}
			}
		}

	}
}