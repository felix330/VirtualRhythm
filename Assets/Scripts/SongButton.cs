using UnityEngine;
using System.Collections;

public class SongButton : MonoBehaviour {

	public GameObject gamemaster;
	public Beatmap associatedBeatmap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick()
	{
		gamemaster.SendMessage ("loadBeatmap", associatedBeatmap);
	}
}
