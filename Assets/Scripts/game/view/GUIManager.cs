using System;
using UnityEngine;

namespace tikal.game
{
	public class GUIManager : MonoBehaviour
	{

		public delegate void CreateBuildingShadow(BuildingType type);
		public static event CreateBuildingShadow OnCreateBuildingShadow;


		public void createBuilding1Order(){
			if (OnCreateBuildingShadow != null)
				OnCreateBuildingShadow (BuildingType.Building1);
		}

		public void createBuilding2Order(){
			if (OnCreateBuildingShadow != null)
				OnCreateBuildingShadow (BuildingType.Building2);
		}
	}
}

