using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DeleteFile : MonoBehaviour {

	
	public void DeleteMag()
    {
        File.Delete(Application.persistentDataPath + "/MagFile" + ".txt");   
    } 
    public void DeleteLine()
    {
        File.Delete(Application.persistentDataPath + "/LineFile" + ".txt");
        Destroy(GameObject.Find("TimeLine"));
    }
}
