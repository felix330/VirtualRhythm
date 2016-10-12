using UnityEngine;
using System.Collections;

public class TestStartCube : MonoBehaviour {

	public GameObject songSettings;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void don()
	{
		songSettings.GetComponent<SongSettings> ().start = true;
	}
}
