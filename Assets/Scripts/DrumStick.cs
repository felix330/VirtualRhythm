using UnityEngine;
using System.Collections;

//For drumstick used in Taiko mode
public class DrumStick : MonoBehaviour {

	public GameObject donR;
	public GameObject donL;
	public GameObject katL;
	public GameObject katR;
	private bool alreadyHit;

	public GameObject noteField;
	public GameObject songSettings;

	SteamVR_TrackedObject trackedObj;

	void Awake () {
		trackedObj = transform.parent.parent.parent.gameObject.GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
	
	}

	void Update () {

		//transform.position = transform.parent.position;
		//transform.rotation = transform.parent.rotation;


	}

	void OnCollisionEnter(Collision c)
	{
		Debug.Log ("colliding with " + c.collider.gameObject);
		if (c.collider.gameObject == donR && alreadyHit == false) {
			donR.GetComponent<Renderer> ().material.color = Color.red;
			donR.GetComponent<AudioSource> ().Play();
			//Use to start if song isn't playing
			if (songSettings.GetComponent<SongSettings> ().start == false && songSettings.GetComponent<AudioSource>().isPlaying == false) {
				songSettings.GetComponent<SongSettings> ().start = true;
			}
			SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse (700);
			alreadyHit = true;
		}
		if (c.collider.gameObject == donL && alreadyHit == false) {
			donL.GetComponent<Renderer> ().material.color = Color.red;
			donL.GetComponent<AudioSource> ().Play();
			//Use to start if song isn't playing
			if (songSettings.GetComponent<SongSettings> ().start == false && songSettings.GetComponent<AudioSource>().isPlaying == false) {
				songSettings.GetComponent<SongSettings> ().start = true;
			}
			SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(700);
			alreadyHit = true;
		}
		if (c.collider.gameObject == katR && alreadyHit == false) {
			katR.GetComponent<Renderer> ().material.color = Color.blue;
			katR.GetComponent<AudioSource> ().Play();
			SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(700);
			alreadyHit = true;
		}
		if (c.collider.gameObject == katL && alreadyHit == false) {
			katL.GetComponent<Renderer> ().material.color = Color.blue;
			katL.GetComponent<AudioSource> ().Play();
			SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(700);
			alreadyHit = true;
		}
	}

	void OnCollisionExit(Collision c)
	{
		Debug.Log ("colliding with " + c.collider.gameObject);

		if (c.collider.gameObject == donR) {
			donR.GetComponent<Renderer> ().material.color = Color.white;
			noteField.BroadcastMessage ("don");
			alreadyHit = false;
		}
		if (c.collider.gameObject == donL) {
			donL.GetComponent<Renderer> ().material.color = Color.white;
			noteField.BroadcastMessage ("don");
			alreadyHit = false;
		}
		if (c.collider.gameObject == katR) {
			katR.GetComponent<Renderer> ().material.color = Color.white;
			noteField.BroadcastMessage ("kat");
			alreadyHit = false;
		}
		if (c.collider.gameObject == katL) {
			katL.GetComponent<Renderer> ().material.color = Color.white;
			noteField.BroadcastMessage ("kat");
			alreadyHit = false;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		Debug.Log ("colliding with ");

	}
}
