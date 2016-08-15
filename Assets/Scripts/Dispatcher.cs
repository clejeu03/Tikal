﻿using System;
using UnityEngine;

namespace tikal.game
{
	public class Dispatcher : MonoBehaviour
	{

		private BuildingManager m_buildingManager;

		void Start ()
		{
			m_buildingManager = this.GetComponent<BuildingManager>();
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
			Debug.Log("<<DISPATCHER>> received Create Building Event");
			m_buildingManager.createBuildingShadow(type);
		}

		private void CreateRoad(){
			Debug.Log("<<DISPATCHER>> received Create Road Event");
			//TODO
		}
	}
}

