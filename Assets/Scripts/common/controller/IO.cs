using System;
using UnityEngine;
using System.IO;


namespace tikal.common
{
	public enum BYTE_ORDER
	{
		Mac,
		Win
	}
	public static class IO{

		//public static float[][] readHeightmap(string path) {

			/*Texture2D heightmap = null;
			byte[] fileData;
			
			if (File.Exists(path))     {
				//fileData = File.ReadAllBytes(path);
				heightmap = new Texture2D(2, 2);
				heightmap.LoadImage(fileData); //..this will auto-resize the texture dimensions.
			}
			for (int y = 0; y < heightmap.height;y++){ 
				for (int x = 0; x < heightmap.width;x++){

					float color = heightmap.GetPixel(x, y);

					//If pixel data is between 0 and 5 (black) or between 250 and 255 (white) => it's height information	
					if ((color>=0 && color<=5)||(color >=250 && color <= 255)){ 

					}else{
						//grey
					}

				}
			}*/


		//}
	}
}

