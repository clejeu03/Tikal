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
				this.enabled = true;
				m_width = 20;
				m_lenght = 20;
			} else if (m_type == BuildingType.Building2) {
				this.enabled = true;
				m_width = 30;
				m_lenght = 30;
			} else if (m_type == BuildingType.None) {
				this.enabled = false;
				m_width = 0;
				m_lenght = 0;
			}

			transform.localScale = new Vector3 (m_lenght/10, 1, m_width/10);
		}

		void Update(){
			if (m_type != BuildingType.None) {
				this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50));

				if (!m_creationAsked)
					checkTerrain ();
			}

			if (Input.GetMouseButtonDown (1)) {
				setType (BuildingType.None);
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

