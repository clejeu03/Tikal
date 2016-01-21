using System;
using UnityEngine;

namespace tikal.game
{
	public class Dispatcher : MonoBehaviour
	{

		private BuildingManager m_buildingManager;

		void Start ()
		{
			m_buildingManager = new BuildingManager ();
		}

		void OnEnable(){
			GUIManager.OnCreateBuilding += CreateBuilding;
		}

		private void CreateBuilding(BuildingType type){
			m_buildingManager.createBuildingBlueprint(type);
		}
	}
}

