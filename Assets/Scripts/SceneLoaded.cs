using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoaded : MonoBehaviour {

	void OnLevelWasLoaded(int level) {
		if (level == 0) {
			Text highScore = GameObject.Find ("HighScore chiffre").GetComponent<Text> ();
			highScore.text = GameModel.highScore.ToString ();
		} else if (level == 1) {
			
		}
	}

}
