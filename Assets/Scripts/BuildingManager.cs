using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tikal.game
{
	public class BuildingManager : MonoBehaviour
	{

		private GameObject m_shadowGO;
		private GameObject m_housePrefab;

		private bool okForConstruction;

		private Material m_blueprintOK;
		private Material m_blueprintNO;

		private List<GameObject> m_buildingList ;

		void Start ()
		{
			m_shadowGO = null;
			m_housePrefab = (GameObject)Resources.Load ("Prefabs/Baker_house");
			m_buildingList = new List<GameObject>();
			m_blueprintOK = (Material)Resources.Load ("Materials/BuildingOKBlueprint");
			m_blueprintNO = (Material)Resources.Load ("Materials/BuildingNOBlueprint");
		}

		public void createBuildingShadow(BuildingType type){
			Debug.Log("Creation of a blueprint for the type : " + type.ToString());
			m_shadowGO = (GameObject)GameObject.Instantiate(m_housePrefab, Input.mousePosition, m_housePrefab.transform.rotation);
			m_shadowGO.GetComponent<MeshRenderer>().material = m_blueprintNO;
		}

		void Update(){
			if (m_shadowGO != null) {
				m_shadowGO.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50));

				okForConstruction = checkTerrain ();

				//Change the material color //
				if(okForConstruction){
					m_shadowGO.GetComponent<MeshRenderer>().material = m_blueprintOK;
				}else{
					m_shadowGO.GetComponent<MeshRenderer>().material = m_blueprintNO;
				}

				// MOUSE INPUTS //
				if (Input.GetMouseButtonDown (1)) {
					GameObject.Destroy(m_shadowGO);
					m_shadowGO = null;
				}

				if (Input.GetMouseButtonDown (0) && okForConstruction) {
					constructBuilding();
				}
			}
		}

		private bool checkTerrain(){
			//TODO

			return true;

		}

		private void constructBuilding(){
			GameObject building = (GameObject)GameObject.Instantiate(m_housePrefab, m_shadowGO.transform.position, m_housePrefab.transform.rotation);
			m_buildingList.Add(building);
			GameObject.Destroy(m_shadowGO);
			m_shadowGO = null;
		}
			
	}
}

