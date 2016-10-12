using System;

//Stores all highscores of a song for serialization
[Serializable]
public class HighScore {

	public int beatmapID;
	public int[] scores;

}
