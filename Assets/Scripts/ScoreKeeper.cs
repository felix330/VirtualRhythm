using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;
using System.IO;

public class ScoreKeeper : MonoBehaviour {

	public GameObject scorePanel;
	public int totalScore;
	public int shieldHitScore;
	public int shieldWrongScore;
	public int shieldMissedScore;
	public HighScoresCollection highScoresCollection;

	public GameObject scoreText1;
	public GameObject scoreText2;
	public GameObject scoreText3;
	public GameObject scoreText4;
	public GameObject scoreText5;

	// Use this for initialization
	void Start () {
		loadAllScores ();
	}
	
	// Update is called once per frame
	void Update () {
		scorePanel.GetComponent<Text> ().text = totalScore.ToString();
	}

	void resetScore() {
		totalScore = 0;
	}

	void shieldHit() {
		totalScore += shieldHitScore;
	}

	void shieldWrong() {
		totalScore += shieldWrongScore;
	}

	void shieldMissed() {
		totalScore += shieldMissedScore;
	}

	void songComplete() {
		if (highScoresCollection == null) {
			highScoresCollection = new HighScoresCollection ();

			highScoresCollection.highScores = new HighScore[1];
			highScoresCollection.highScores [0] = new HighScore ();
			highScoresCollection.highScores [0].scores = new int[1];
			highScoresCollection.highScores [0].scores [0] = totalScore;
			highScoresCollection.highScores [0].beatmapID = GetComponent<SongSettings> ().beatmapID;
		} else {
			bool found = false;
			for (var i = 0; i < highScoresCollection.highScores.Length; i++) {
				if (highScoresCollection.highScores [i].beatmapID == GetComponent<SongSettings> ().beatmapID) {
					addScore (i);
					found = true;
				}
			}

			if (found == false) {
				addNewScore ();
			}


		}
		saveAllScores ();
		showScores ();
	}

	void addScore(int toScore)
	{
		int[] temp = highScoresCollection.highScores [toScore].scores;

		highScoresCollection.highScores [toScore].scores = new int[temp.Length + 1];

		for (int i = 0; i < temp.Length; i++) {
			highScoresCollection.highScores [toScore].scores [i] = temp [i];
		}

		highScoresCollection.highScores [toScore].scores [highScoresCollection.highScores [toScore].scores.Length - 1] = totalScore;

	}

	void addNewScore()
	{
		HighScore[] temp = highScoresCollection.highScores;

		highScoresCollection.highScores = new HighScore[temp.Length + 1];

		for (int i = 0; i < temp.Length; i++) {
			highScoresCollection.highScores [i] = temp [i];
		}

		highScoresCollection.highScores [highScoresCollection.highScores.Length - 1] = new HighScore();
		highScoresCollection.highScores [highScoresCollection.highScores.Length - 1].beatmapID = GetComponent<SongSettings> ().beatmapID;
		highScoresCollection.highScores [highScoresCollection.highScores.Length - 1].scores = new int[1];
		highScoresCollection.highScores [highScoresCollection.highScores.Length - 1].scores [0] = totalScore;
	}

	void showScores()
	{
		scoreText1.GetComponent<Text>().text = "None";
		scoreText2.GetComponent<Text>().text = "None";
		scoreText3.GetComponent<Text>().text = "None";
		scoreText4.GetComponent<Text>().text = "None";
		scoreText5.GetComponent<Text>().text = "None";

		//Check if any scores exist
		if (highScoresCollection == null) {
			return;
		} else {
			bool found = false;
			for (var i = 0; i < highScoresCollection.highScores.Length; i++) {
				if (highScoresCollection.highScores [i].beatmapID == GetComponent<SongSettings> ().beatmapID) {
					found = true;
				}
			}

			if (found == false) {
				return;
			}
		}


		var query = from highScores in highScoresCollection.highScores
		            where highScores.beatmapID == GetComponent<SongSettings> ().beatmapID
		            select highScores;
		
		HighScore h = (HighScore)query.ElementAt(0);

		int[] tempScores = h.scores;

		Array.Sort (tempScores);

		Array.Reverse (tempScores);

		Debug.Log (tempScores[tempScores.Length-1]);

		for (int i = 0; i < tempScores.Length; i++) {
			switch (i) {
			case 0:
				scoreText1.GetComponent<Text> ().text = tempScores [i].ToString ();
				break;
			case 1:
				scoreText2.GetComponent<Text> ().text = tempScores [i].ToString ();
				break;
			case 2:
				scoreText3.GetComponent<Text> ().text = tempScores [i].ToString ();
				break;
			case 3:
				scoreText4.GetComponent<Text> ().text = tempScores [i].ToString ();
				break;
			case 4:
				scoreText5.GetComponent<Text> ().text = tempScores [i].ToString ();
				break;
			}
		}
	}

	void saveAllScores()
	{
		string serialized = JsonUtility.ToJson (highScoresCollection);
		System.IO.File.WriteAllText (Application.dataPath+"/Highscores.txt", serialized);
	}

	void loadAllScores()
	{
		if (System.IO.File.ReadAllText(Application.dataPath+"/Highscores.txt") != null)
		{
			string serialized = System.IO.File.ReadAllText (Application.dataPath+"/Highscores.txt");
			highScoresCollection = JsonUtility.FromJson<HighScoresCollection>(serialized);
		}
		
	}
}
