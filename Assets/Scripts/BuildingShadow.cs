using System;
using UnityEngine;

namespace tikal.game
{
	public class BuildingShadow : MonoBehaviour
	{

		private string m_message;
		private bool m_okForConstruction = true;

		private Material m_blueprintOK;
		private Material m_blueprintNO;

		public delegate void ConstructBuilding();
		public static event ConstructBuilding OnConstructBuilding;

		public delegate void CancelConstruction();
		public static event CancelConstruction OnCancelConstruction;

		void Start(){
			m_blueprintOK = (Material)Resources.Load ("Materials/BuildingOKBlueprint");
			m_blueprintNO = (Material)Resources.Load ("Materials/BuildingNOBlueprint");

			GetComponent<MeshRenderer>().material = m_blueprintNO;
		}

		void Update(){
			this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50));

			//Change the material color //
			if(m_okForConstruction){
				GetComponent<MeshRenderer>().material = m_blueprintOK;
			}else{
				GetComponent<MeshRenderer>().material = m_blueprintNO;
			}

			// MOUSE INPUTS //
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

