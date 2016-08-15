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

		private AudioSource m_audio;
		private AudioClip m_constructionCompleteSound;
		private AudioClip m_constructionImpossibleSound;
		private float distanceToGround = 100f;

		public delegate void ConstructBuilding();
		public static event ConstructBuilding OnConstructBuilding;

		public delegate void CancelConstruction();
		public static event CancelConstruction OnCancelConstruction;

		void Start(){
			m_blueprintOK = (Material)Resources.Load ("Materials/BuildingOKBlueprint");
			m_blueprintNO = (Material)Resources.Load ("Materials/BuildingNOBlueprint");

			m_constructionCompleteSound = (AudioClip)Resources.Load ("Sounds/ConstructionComplete");
			m_constructionImpossibleSound = (AudioClip)Resources.Load ("Sounds/ConstructionImpossible");

			m_audio = Camera.main.GetComponent<AudioSource>();
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
				if (OnConstructBuilding != null){
					OnConstructBuilding ();
					m_audio.clip = m_constructionCompleteSound;
					m_audio.Play();
				}
			}else if(Input.GetMouseButtonDown (0) && !m_okForConstruction){
				m_audio.clip = m_constructionImpossibleSound;
				m_audio.Play();
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

