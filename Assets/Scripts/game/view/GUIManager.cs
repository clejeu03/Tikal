using System;
using UnityEngine;

namespace tikal.game
{
	public class GUIManager : MonoBehaviour
	{

		public delegate void CreateBuilding(BuildingType type);
		public static event CreateBuilding OnCreateBuilding;


		public void createBuilding1Order(){
			if (OnCreateBuilding != null)
				OnCreateBuilding (BuildingType.Building1);
			//m_dispatcher.createBuildingBlueprint (BuildingType.Building1);
		}

		public void createBuilding2Order(){
			if (OnCreateBuilding != null)
				OnCreateBuilding (BuildingType.Building2);
			//m_dispatcher.createBuildingBlueprint (BuildingType.Building2);
		}
	}
}

