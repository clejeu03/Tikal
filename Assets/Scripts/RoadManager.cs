using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tikal.game
{
	public class RoadManager
	{
		private GameObject m_roadPrefab;
		private GameObject m_roadShadow;

		public RoadManager ()
		{
			m_roadPrefab = (GameObject)Resources.Load("Prefabs/Road");
			m_roadShadow = (GameObject)GameObject.Instantiate(m_roadPrefab, Input.mousePosition, m_roadPrefab.transform.rotation);
			m_roadShadow.SetActive(false);
		}

		public void createRoadShadow(){
			//TODO
			m_roadShadow.SetActive(true);
		}
	}
}

