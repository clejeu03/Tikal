using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tikal.game
{
	public class BuildingManager
	{

		private GameObject m_shadowGO;
		private GameObject m_housePrefab;

		private List<GameObject> m_buildingList ;

		public BuildingManager ()
		{
			m_housePrefab = (GameObject)Resources.Load ("Prefabs/Baker_house");
			m_buildingList = new List<GameObject>();

		}

		public void createBuildingShadow(BuildingType type){
			Debug.Log("Creation of a blueprint for the type : " + type.ToString());
			m_shadowGO = (GameObject)GameObject.Instantiate(m_housePrefab, Input.mousePosition, m_housePrefab.transform.rotation);
			m_shadowGO.AddComponent<BuildingShadow>();
			BuildingShadow.OnConstructBuilding += ConstructBuilding;
			BuildingShadow.OnCancelConstruction += CancelConstruction;
		}

		private void ConstructBuilding(){
			GameObject building = (GameObject)GameObject.Instantiate(m_housePrefab, m_shadowGO.transform.position, m_housePrefab.transform.rotation);
			m_buildingList.Add(building);
			GameObject.Destroy(m_shadowGO);
			BuildingShadow.OnConstructBuilding -= ConstructBuilding;
		}

		private void CancelConstruction(){
			GameObject.Destroy(m_shadowGO);
			BuildingShadow.OnConstructBuilding -= ConstructBuilding;
		}
			
	}
}

