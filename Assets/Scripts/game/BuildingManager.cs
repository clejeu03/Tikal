using System;
using UnityEngine;

namespace tikal.game
{
	public class BuildingManager
	{

		[SerializeField]private GameObject shadowPrefab;

		private BuildingShadow m_shadow;

		public BuildingManager ()
		{
		}

		public void createBuildingShadow(BuildingType type){
			//TODO
			Debug.Log("Creation of a blueprint for the type : " + type.ToString());
			GameObject shadowPrefab = (GameObject)Resources.Load ("Prefabs/BuildingShadow");
			GameObject shadowGO = (GameObject)GameObject.Instantiate(shadowPrefab, Input.mousePosition, Quaternion.identity);
			m_shadow = shadowGO.GetComponent<BuildingShadow> ();
			m_shadow.setType (type);
		}


	}
}

