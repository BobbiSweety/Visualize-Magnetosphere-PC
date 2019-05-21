using UnityEngine;
using System.Collections;

public class EarthRotate2 : MonoBehaviour {
	public float EarthSpeed = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("Sun").transform.Rotate(Vector3.up * Time.deltaTime * 0.5f);

        // Slowly rotate the object around its X axis at 1 degree/second.
        transform.Rotate(Vector3.down * EarthSpeed * Time.deltaTime/0.09f);
        GameObject.Find("Earth").transform.RotateAround(new Vector3(8, 0, 0), new Vector3(0, 1, 0), 1f * Time.deltaTime);  
    }
  
}
