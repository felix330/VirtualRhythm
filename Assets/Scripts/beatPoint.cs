using UnityEngine;
using System.Collections;

//Spheres in shield-mode
public class beatPoint : MonoBehaviour {

	public GameObject songSettings;
	public GameObject goal;

	public int type; //0 = don, 1 = kat

	private double spawnAt; //Time Beatpoint starts moving (Calculated automatically)
	public double hitAt; //Time Beatpoint should be hit

	private double songPos; //Current Song Position
	private double startSongPos; //Song Position on Start
	public bool start;
	private Vector3 transformStartPosition; //Position Beatpoint starts from

	private bool playedSound;
	private bool hitCorrectly;

	private Behaviour halo;

	void Start () {
		songSettings = GameObject.Find ("GameMaster");
		goal = GameObject.Find ("Goal");
		transform.localPosition = Vector3.zero;
		transform.LookAt (goal.transform);

		halo = (Behaviour)GetComponent ("Halo");

		spawnAt = hitAt-songSettings.GetComponent<SongSettings> ().noteDelay;

		transform.RotateAround(goal.transform.position,Vector3.up,Random.Range(-30f,30f));
		transform.RotateAround(goal.transform.position,Vector3.right,Random.Range(-30f,0));

		transformStartPosition = transform.position;

		halo.enabled = false;
		GetComponent<MeshRenderer> ().enabled = false;
		start = false;
	}

	void Update () {
		songPos = songSettings.GetComponent<SongSettings> ().songTime;

		if (songPos >= spawnAt && start == false) {
			startSongPos = songPos;
			halo.enabled = true;
			GetComponent<MeshRenderer> ().enabled = true;
			start = true;
		}

		if (start == true)
		{	
			transform.position = Vector3.Lerp (transformStartPosition, goal.transform.position, (float)((songPos-startSongPos)/(hitAt-startSongPos)));
		}

		if ((float)((songPos - startSongPos) / (hitAt - startSongPos)) >= 1 && GetComponent<MeshRenderer>().enabled == true) {
			songSettings.SendMessage ("shieldMissed");
			Destroy (gameObject,0.1f);
		}
			
		if (GetComponent<MeshRenderer> ().enabled == false && playedSound == false && (float)((songPos - startSongPos) / (hitAt - startSongPos)) >= 1 && hitCorrectly == true) {
			GetComponent<AudioSource> ().Play ();
			Destroy (gameObject,GetComponent<AudioSource>().clip.length); //only Destroy once Soundeffect is done
			playedSound = true;
		}

	}

	void don()
	{
		if (type == 0) {
			songSettings.SendMessage ("shieldHit");
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<SphereCollider> ().enabled = false;
			GetComponent<Rigidbody> ().detectCollisions = false;
			BroadcastMessage ("playParticle");
			halo.enabled = false;
			hitCorrectly = true;
		} else {
			songSettings.SendMessage ("shieldWrong");
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<SphereCollider> ().enabled = false;
			GetComponent<Rigidbody> ().detectCollisions = false;
			halo.enabled = false;
			hitCorrectly = false;
		}

	}

	void kat()
	{
		if (type == 1) {
			songSettings.SendMessage ("shieldHit");
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<SphereCollider> ().enabled = false;
			GetComponent<Rigidbody> ().detectCollisions = false;
			BroadcastMessage ("playParticle");
			halo.enabled = false;
			hitCorrectly = true;
		} else {
			songSettings.SendMessage ("shieldWrong");
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<SphereCollider> ().enabled = false;
			GetComponent<Rigidbody> ().detectCollisions = false;
			halo.enabled = false;
			hitCorrectly = false;
		}
	}

	void delete()
	{
		Destroy (gameObject);
	}
}
