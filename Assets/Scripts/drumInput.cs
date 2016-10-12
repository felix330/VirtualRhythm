using UnityEngine;
using System.Collections;

public class drumInput : MonoBehaviour {

	public GameObject noteField;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown("DonL") || Input.GetButtonDown("DonR")) {
			hitDon ();
		}

		if (Input.GetButtonDown("KatL") || Input.GetButtonDown("KatR")) {
			hitKat ();
		}

	}

	void hitDon(){
		Debug.Log ("Don");
		noteField.BroadcastMessage ("don");
	}

	void hitKat(){
		Debug.Log ("kat");
		noteField.BroadcastMessage ("kat");
	}
}
