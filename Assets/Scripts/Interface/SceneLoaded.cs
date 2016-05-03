using UnityEngine;
using UnityEngine.UI;

public class SceneLoaded : MonoBehaviour {

	void OnLevelWasLoaded(int level) {
		if (level == 0) {
			Text highScore = GameObject.Find ("HighScore chiffre").GetComponent<Text> ();
            highScore.text = PlayerPrefs.GetInt("highScore").ToString();
		} else if (level == 1) {
			Text textLife = GameObject.Find("Vie chiffre").GetComponent<Text>();
			textLife.text = GameModel.initialLife.ToString();
		}
	}

}
