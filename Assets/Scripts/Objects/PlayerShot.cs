using UnityEngine;

/// <summary>
/// Comportement des tirs
/// </summary>
public class PlayerShot : MonoBehaviour
{
    public int damage = 1;
    public Vector2 speed = new Vector2(10, 0);
    public Vector2 direction = new Vector2(1, 0);
    public bool isEnemyShot = false;
    public Player owner;

    private Vector2 movement;

    void Start()
    {
        Destroy(gameObject, 6); // Destroy it in 6 sec
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            GameObject.Instantiate(enemy.bonus, enemy.transform.position, new Quaternion());
            Destroy(enemy.gameObject);
            Destroy(gameObject);
            Player player = owner.GetComponent<Player>();
            player.AddScore(1000);
            SpecialEffectsHelper.Instance.Explosion(enemy.transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();
        }
    }
}