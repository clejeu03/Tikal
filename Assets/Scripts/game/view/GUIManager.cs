using System;
using UnityEngine;

namespace tikal.game
{
	public class GUIManager : MonoBehaviour
	{

		[SerializeField] private BuildingManager m_buildingManager;

		public GUIManager ()
		{
		}

		public void createBuilding1Order(){
			m_buildingManager.createBuildingBlueprint (BuildingType.Building1);
		}

		public void createBuilding2Order(){
			m_buildingManager.createBuildingBlueprint (BuildingType.Building2);
		}
	}
}

