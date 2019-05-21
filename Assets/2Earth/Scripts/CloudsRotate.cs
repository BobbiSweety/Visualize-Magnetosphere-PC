using UnityEngine;
using System.Collections;

public class CloudsRotate : MonoBehaviour {
	
	public float cloudSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
    // Slowly rotate the object around its X axis at 1 degree/second.
    transform.Rotate(Vector3.up * cloudSpeed * Time.deltaTime/Time.timeScale);

	}
}
