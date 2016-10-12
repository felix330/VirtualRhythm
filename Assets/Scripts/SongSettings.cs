using UnityEngine;
using System.Collections;

//handles Settings and timing in all game modes
public class SongSettings : MonoBehaviour {

	public GameObject noteField;

	public float noteDelay;

	public bool start;
	public double songTime;
	public double testSongTime;
	public float offset;
	public int bpm;

	private double startTime;

	public AudioClip song;
	public string songTitle;
	public int beatmapID;

	bool hasStarted;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (start == true) {
			GetComponent<AudioSource> ().Play ();
			startTime = AudioSettings.dspTime;
			hasStarted = true;
			start = false;
		}

		if (GetComponent<AudioSource> ().isPlaying) {
			songTime = AudioSettings.dspTime - startTime;
			testSongTime = GetComponent<AudioSource> ().time;
		}

		if (GetComponent<AudioSource>().isPlaying == false && hasStarted == true) {
			SendMessage ("songComplete");
			hasStarted = false;
		}
	}

}
