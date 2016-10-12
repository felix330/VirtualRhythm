using UnityEngine;
using System.Collections;
using BMAPI;
using System.IO;
using UnityEngine.UI;

//reads Osu files and generates maps for various gamemodes
public class readOsu : MonoBehaviour {

	public GameMode currentMode;

	public Beatmap[] beatMaps;

	public GameObject don;
	public GameObject kat;
	public GameObject noteField;

	public GameObject titleDisplay;
	public GameObject startButton;


	// Use this for initialization
	void Start () {

		noteField = GameObject.Find ("NoteField");
		//bm = new BMAPI.v1.Beatmap("C:/ikusa.osu");

		findFiles();


	}

	void Update() {

	}

	//Scan Songs Folder for .osu and .mp3 files
	void findFiles()
	{
		Debug.Log (Application.dataPath+"/Songs");

		DirectoryInfo info = new DirectoryInfo (Application.dataPath + "/Songs");
		DirectoryInfo[] allDirs = info.GetDirectories ();

		for (int i = 0; i < allDirs.Length; i++) {
			FileInfo[] folderFiles = allDirs [i].GetFiles ();

			//Find Song mp3 File
			string mp3Path = "";

			for (int j = 0; j < folderFiles.Length; j++) {
				if (folderFiles [j].Name.Contains (".mp3") && !folderFiles [j].Name.Contains (".meta")) {
					mp3Path = "File:///" + folderFiles[j].Directory + "/" + folderFiles [j].Name;
					mp3Path = mp3Path.Replace ('\\', '/');
					Debug.Log (mp3Path);
				}
			}

			//Find Beatmaps for Song
			if (mp3Path == null) {
				Debug.LogWarning ("No MP3 file found. Ignoring folder");
			} else {
				for (int k = 0; k < folderFiles.Length; k++) {
					if (folderFiles [k].Name.Contains (".osu") && !folderFiles [k].Name.Contains (".meta")) {
						Beatmap newBM = new Beatmap (); 
						string tempString;
						tempString = folderFiles [k].Directory + "/" + folderFiles [k].Name;
						tempString = tempString.Replace ('\\', '/');
						Debug.Log (tempString);
						newBM.songDirectory = mp3Path;
						newBM.mapDirectory = tempString;
						BMAPI.v1.Beatmap b = new BMAPI.v1.Beatmap (tempString);
						newBM.name = b.Title + " " + b.Version;
						addToArray (newBM);
					}
				}
			}
		}

		createUI();

	}
	
	void addToArray(Beatmap b)
	{
		if (beatMaps == null) {
			beatMaps = new Beatmap[1];
			beatMaps [0] = b;
		} else {
			Beatmap[] tempArray = beatMaps;

			beatMaps = new Beatmap[beatMaps.Length + 1];

			for (int i = 0; i < tempArray.Length; i++) {
				beatMaps [i] = tempArray [i];
			}
			beatMaps [beatMaps.Length-1] = b;
		}

	}

	void loadBeatmap(Beatmap b)
	{
		SendMessage ("resetScore");
		GetComponent<SongSettings> ().songTime = 0;
		GetComponent<SongSettings> ().testSongTime = 0;
		BMAPI.v1.Beatmap newBM = new BMAPI.v1.Beatmap (b.mapDirectory);
		GetComponent<SongSettings> ().beatmapID = (int)newBM.BeatmapID;
		StartCoroutine ("loadMP3",b.songDirectory);

		titleDisplay.GetComponent<Text> ().text = b.name;

		switch (currentMode) {
		case GameMode.Shield:
			loadShieldMap (newBM);
			break;
		case GameMode.Taiko:
			break;
		}

		startButton.GetComponent<StartButton> ().toggleVisibility (true);
	}

	//creates shield map from osu beatmap
	void loadShieldMap(BMAPI.v1.Beatmap bm)
	{
		//Generate don and kat from HitObjects
		noteField.BroadcastMessage("delete",SendMessageOptions.DontRequireReceiver);
		for (int i = 0; i < bm.HitObjects.Count; i++)
		{

			GameObject newHitObject;

			if (bm.HitObjects [i].Effect == BMAPI.v1.EffectType.Whistle || bm.HitObjects [i].Effect == BMAPI.v1.EffectType.Clap) {
				newHitObject = Instantiate (kat);
			} 
			else {
				newHitObject = Instantiate (don);
			}

			newHitObject.transform.parent = noteField.transform;
			newHitObject.GetComponent<beatPoint> ().hitAt = bm.HitObjects [i].StartTime/ 1000;
		}
	}

	//Loads and converts mp3 files
	IEnumerator loadMP3(string s)
	{
		GetComponent<AudioSource> ().Stop ();
		WWW www = new WWW (s);
		while(!www.isDone){
			yield return 0;
		}
		AudioClip clip = NAudioPlayer.FromMp3Data(www.bytes);
		GetComponent<AudioSource> ().clip = clip;
	}

	public GameObject[] songSelections;
	public int selectPosition;

	void createUI()
	{
		for (int i = selectPosition; i < songSelections.Length+selectPosition; i++) {
			songSelections [i - selectPosition].GetComponent<Text>().text = beatMaps [i].name;
			songSelections [i - selectPosition].GetComponent<SongButton>().associatedBeatmap = beatMaps[i];
		}
	}

	void selectPosUp()
	{
		if (selectPosition < beatMaps.Length - songSelections.Length){
			selectPosition = selectPosition+1;
			createUI ();
		}
	}

	void selectPosDown()
	{
		if (selectPosition >= 0) {
			selectPosition = selectPosition-1;
			createUI ();
		}
	}
}
