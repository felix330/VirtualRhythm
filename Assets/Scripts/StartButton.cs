using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public GameObject songSettings;
	// Use this for initialization
	void Start () {
		toggleVisibility (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleVisibility(bool b)
	{
		gameObject.SetActive (b);
	}

	public void clicked()
	{
		Invoke ("start", 1);
		toggleVisibility (false);
	}

	void start()
	{
		songSettings.GetComponent<SongSettings> ().start = true;
	}
}
