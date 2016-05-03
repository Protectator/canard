using UnityEngine;

/// <summary>
/// Comportement générique pour les méchants
/// </summary>
public class Enemy : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    public GameObject bonus;

    private Vector2 realSpeed;
    private bool hasSpawn;
    private Weapon[] weapons;

    private Vector2 movement;

    void Awake()
    {
        // Récupération de toutes les armes de l'ennemi
        weapons = GetComponentsInChildren<Weapon>();
    }

    // 1 - Disable everything
    void Start()
    {
        hasSpawn = false;
        realSpeed = new Vector2(0, 0);

        // On désactive tout
        // -- collider
        GetComponent<Collider2D>().enabled = false;
        // -- Tir
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {
        // 2 - On vérifie si l'ennemi est apparu à l'écran
        if (hasSpawn == false)
        {
            if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
            movement = new Vector2(
            realSpeed.x * direction.x,
            realSpeed.y * direction.y);
            // On fait tirer toutes les armes automatiquement
            foreach (Weapon weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);

                    SoundEffectsHelper.Instance.MakeEnemyShotSound();
                }
            }

            // 4 - L'ennemi n'a pas été détruit, il faut faire le ménage
            if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }

    // 3 - Activation
    private void Spawn()
    {
        hasSpawn = true;
        realSpeed = speed;

        // On active tout
        // -- Collider
        GetComponent<Collider2D>().enabled = true;
        // -- Tir
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}