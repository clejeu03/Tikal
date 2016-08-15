using System;
using UnityEngine;

namespace tikal.game
{
	public class Dispatcher : MonoBehaviour
	{

		private BuildingManager m_buildingManager;
		private RoadManager m_roadManager;

		void Start ()
		{
			m_buildingManager = new BuildingManager();
			m_roadManager = new RoadManager();
		}

		void OnEnable(){
			GUIManager.OnCreateBuildingShadow += CreateBuilding;
			GUIManager.OnCreateRoad += CreateRoad;
		}

		void OnDisable(){
			GUIManager.OnCreateBuildingShadow -= CreateBuilding;
			GUIManager.OnCreateRoad -= CreateRoad;

		}

		private void CreateBuilding(BuildingType type){
			m_buildingManager.createBuildingShadow(type);
		}

		private void CreateRoad(){
			m_roadManager.createRoadShadow();
		}
	}
}

