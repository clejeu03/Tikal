using System;
using UnityEngine;

namespace tikal.game
{
	public class BuildingManager
	{

		private GameObject shadowPrefab;

		private BuildingShadow m_shadow;

		public BuildingManager ()
		{
			GameObject shadowPrefab = (GameObject)Resources.Load ("Prefabs/BuildingShadow");
			GameObject shadowGO = (GameObject)GameObject.Instantiate(shadowPrefab, Input.mousePosition, Quaternion.identity);
			m_shadow = shadowGO.GetComponent<BuildingShadow> ();
		}

		public void createBuildingShadow(BuildingType type){
			
			Debug.Log("Creation of a blueprint for the type : " + type.ToString());
			m_shadow.setType (type);
		}


	}
}

