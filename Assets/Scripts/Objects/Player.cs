using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50);
    public int hp = 5;
    public int score = 0;
    public int canetonValue = 10000;

    public GameObject deadPanel;
    public GameObject endPanel;
    public GameObject background;

    public Scrolling scrollingObject;

    private Vector2 movement;

    private Text textLife;
    private Text textScore;
    private Text textHighScore;

    void Start()
    {
        hp = 5;
        score = 0;
        canetonValue = 10000;
        PlayerPrefs.SetInt("score", score);
        textLife = GameObject.Find("Vie chiffre").GetComponent<Text>();
        textScore = GameObject.Find("Score chiffre").GetComponent<Text>();
        textHighScore = GameObject.Find("HighScore chiffre").GetComponent<Text>();
        UpdateUI();
    }

    void Update()
    {
        // 3 - Récupérer les informations du clavier/manette
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // 4 - Calcul du mouvement
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);

        // 5 - Tir
        bool shoot = Input.GetButton("Fire1");
        shoot |= Input.GetButton("Fire2");
        // Astuce pour ceux sous Mac car Ctrl + flèches est utilisé par le système

        if (shoot)
        {
            Weapon weapon = GetComponent<Weapon>();
            if (weapon != null)
            {
                // false : le joueur n'est pas un ennemi
                weapon.Attack(false, this);
            }
        }

        // 6 - Déplacement limité au cadre de la caméra
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );
    }

    void FixedUpdate()
    {
        // 5 - Déplacement
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    public void AddScore(int value)
    {
        score += value;
        PlayerPrefs.SetInt("score", score);
        int high = PlayerPrefs.GetInt("highScore");
        if (score > high)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
        UpdateUI();
    }

    public void AddCanetonValue(int value)
    {
        canetonValue += value;
    }

    public void UpdateUI()
    {
        textScore.text = PlayerPrefs.GetInt("score").ToString();
        textHighScore.text = PlayerPrefs.GetInt("highScore").ToString();
        textLife.text = hp.ToString();
    }

    public void TakeDamage(int amount = 1)
    {
        hp--;
        if (hp <= 0)
        {
            deadPanel.SetActive(true);
            scrollingObject.speed = new Vector2(0,0);
            Destroy(gameObject);
        }
        UpdateUI();
    }
}