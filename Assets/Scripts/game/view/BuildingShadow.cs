using System;
using UnityEngine;

namespace tikal.game
{
	public class BuildingShadow : MonoBehaviour
	{

		private int m_width;
		private int m_lenght;
		private BuildingType m_type = BuildingType.None;
		private string m_message;
		private Color m_allowedColor = Color.green;
		private Color m_forbiddenColor = Color.red;
		private bool m_creationAsked = false;

		void Start(){
			
		}

		public void setType (BuildingType type)
		{
			m_type = type;
			if (m_type == BuildingType.Building1) {
				m_width = 20;
				m_lenght = 20;
			} else if (m_type == BuildingType.Building2) {
				m_width = 30;
				m_lenght = 30;
			}

			transform.localScale = new Vector3 (m_lenght, 1, m_width);
		}

		void Update(){
			if (m_type != BuildingType.None) {
				this.transform.position = Input.mousePosition;

				if (!m_creationAsked)
					checkTerrain ();
			}
		}

		public void checkTerrain(){
			
		}

		public void setAllowed(){
			//Apply the allowedcolor
		}

		public void setForbidden(){
			//Apply the forbiddenColor
		}
	}
}

