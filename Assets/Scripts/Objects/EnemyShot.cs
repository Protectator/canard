using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

    public int damage = 1;
    public Vector2 speed = new Vector2(10, 0);
    public Vector2 direction = new Vector2(1, 0);
    public bool isEnemyShot = false;

    private Vector2 movement;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, 6); // Destroy it in 6 sec
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
        GetComponent<Rigidbody2D>().velocity = movement;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Destroy(gameObject);
            player.TakeDamage();
        }
    }
}
