using UnityEngine;

public class Bonus : MonoBehaviour
{
    public bool isCaneton = false;
    public int score = 100;
    public int canetonValueIncrement = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Est-ce que c'est le joueur ?
        Player player = collider.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (isCaneton)
            {
                player.AddScore(player.canetonValue);
                player.AddCanetonValue(10000);
            } else {
                player.AddScore(score);
                player.AddCanetonValue(canetonValueIncrement);
            }
            Destroy(gameObject);
        }
    }
}
