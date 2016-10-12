using UnityEngine;
using System.Collections;

public class shield : MonoBehaviour {

	//0 = don, 1 = kat
	public int type;


	//Send message to Beatpoints
	void OnTriggerEnter(Collider coll)
	{
		if (type == 0) {
			coll.gameObject.SendMessage ("don");
		}

		if (type == 1) {
			coll.gameObject.SendMessage ("kat");
		}
	}
}
