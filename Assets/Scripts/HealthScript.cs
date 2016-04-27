using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gestion des points de vie et des dégâts
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Points de vies
    /// </summary>
    public int hp = 5;

    /// <summary>
    /// Ennemi ou joueur ?
    /// </summary>
    public bool isEnemy = true;

	private Text textLife;
	private Text textScore;
	private Text textHighScore;

	public Transform popcornPrefab;

	public GameObject deadPanel;
	public GameObject endPanel;
    public GameObject background;

	void Start() 
	{
		GameModel.score = 0;
        GameModel.cannetonValue = 10000;
		textLife = GameObject.Find("Vie chiffre").GetComponent<Text>();
		textLife.text = hp.ToString ();
		textScore = GameObject.Find("Score chiffre").GetComponent<Text>();
		textHighScore = GameObject.Find ("HighScore chiffre").GetComponent<Text> ();
		textHighScore.text = GameModel.highScore.ToString ();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Est-ce un tir ?
        ShotScript shot = collider.gameObject.GetComponent<ShotScript>();
		if (shot != null) {
			// Tir allié
			if (shot.isEnemyShot != isEnemy) {
				hp -= shot.damage;

				// Destruction du projectile
				// On détruit toujours le gameObject associé
				// sinon c'est le script qui serait détruit avec ""this""
				Destroy (shot.gameObject);

				if (!isEnemy) {
					textLife.text = hp.ToString ();
				} else {
					GameModel.score += GameModel.ennemyValue;
					textScore.text = GameModel.score.ToString ();
					var popcorn = Instantiate (popcornPrefab) as Transform;
					popcorn.position = transform.position;
				}

				if (hp <= 0) {
					// 'Splosion!
					SpecialEffectsHelper.Instance.Explosion (transform.position);

					SoundEffectsHelper.Instance.MakeExplosionSound ();
					if (!isEnemy) {
						deadPanel.SetActive (true);
						if (GameModel.score > GameModel.highScore) {
							GameModel.highScore = GameModel.score;
							textHighScore.text = GameModel.highScore.ToString ();
						}
                        background.gameObject.GetComponent<ScrollingScript>().enabled = false;
                    }
                    // Destruction !
                    Destroy (gameObject);
				}
			}
		} else if (collider.gameObject.tag == "Caneton") {
			GameModel.score += GameModel.cannetonValue;
			GameModel.cannetonValue += GameModel.cannetonIncrement;
			textScore.text = GameModel.score.ToString ();
			Destroy (collider.gameObject);
		} else if (collider.gameObject.tag == "Pop-Corn") {
			GameModel.score += GameModel.popcornValue;
			GameModel.cannetonValue += GameModel.popcornIncrement;
			textScore.text = GameModel.score.ToString ();
			Destroy (collider.gameObject);
		} else if (collider.gameObject.tag == "Enemy") {
			textLife.text = hp.ToString();
            Destroy(collider.gameObject);
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();
        } else if (collider.gameObject.name == "endLine" && !isEnemy) {
			endPanel.SetActive (true);
            GameModel.score += 20000 * hp;
            background.gameObject.GetComponent<ScrollingScript>().enabled = false;
            Destroy(gameObject);
            if (GameModel.score > GameModel.highScore)
            {
                GameModel.highScore = GameModel.score;
                textHighScore.text = GameModel.highScore.ToString();
            }
        }
    }
}