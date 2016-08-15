using System;
using UnityEngine;

namespace tikal.game
{
	public class BuildingShadow : MonoBehaviour
	{

		private string m_message;
		private bool m_okForConstruction = true;
		private GameObject goTerrain;

		private Material m_blueprintOK;
		private Material m_blueprintNO;
		private float distanceToGround = 100f;

		public delegate void ConstructBuilding();
		public static event ConstructBuilding OnConstructBuilding;

		public delegate void CancelConstruction();
		public static event CancelConstruction OnCancelConstruction;

		void Start(){
			m_blueprintOK = (Material)Resources.Load ("Materials/BuildingOKBlueprint");
			m_blueprintNO = (Material)Resources.Load ("Materials/BuildingNOBlueprint");
			goTerrain = GameObject.FindGameObjectWithTag("Terrain");
			GetComponent<MeshRenderer>().material = m_blueprintNO;
		}

		void Update(){
			// ------------------ POSITION -------------------- //
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50));
			RaycastHit hit;
			if(goTerrain != null){
				if (goTerrain.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
					this.transform.position = hit.point;
				}
			}
				
			// ----------------- COLOR FEEDBACK ---------------- //
			if(m_okForConstruction){
				GetComponent<MeshRenderer>().material = m_blueprintOK;
			}else{
				GetComponent<MeshRenderer>().material = m_blueprintNO;
			}

			// -------------- MOUSE INPUTS --------------------- //
			if (Input.GetMouseButtonDown (1)) {
				if (OnCancelConstruction != null)
					OnCancelConstruction ();
			}

			if (Input.GetMouseButtonDown (0) && m_okForConstruction) {
				if (OnConstructBuilding != null)
					OnConstructBuilding ();
			}
				
		}

		void OnTriggerEnter(Collider col) {
			m_okForConstruction = false;
		}

		void OnTriggerExit(Collider col) {
			m_okForConstruction = true;
		}
	}
}

