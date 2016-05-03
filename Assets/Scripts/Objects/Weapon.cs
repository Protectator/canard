using UnityEngine;

/// <summary>
/// Crée des projectiles
/// </summary>
public class Weapon : MonoBehaviour
{
    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Prefab du projectile
    /// </summary>
    public Transform shotPrefab;
    public Vector2 shotSpeed;
    public Vector2 shotDirection;

    /// <summary>
    /// Temps de rechargement entre deux tirs
    /// </summary>
    public float shootingRate = 0.25f;

    //--------------------------------
    // 2 - Rechargement
    //--------------------------------

    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Tirer depuis un autre script
    //--------------------------------

    /// <summary>
    /// Création d'un projectile si possible
    /// </summary>
    public void Attack(bool isEnemy, Player owner = null)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Création d'un objet copie du prefab
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Position
            shotTransform.position = transform.position;

            if (isEnemy)
            {
                EnemyShot eShot = shotPrefab.GetComponent<EnemyShot>();
                eShot.speed = shotSpeed;
                eShot.direction = shotDirection;
            }

            // Propriétés du script
            PlayerShot shot = shotTransform.gameObject.GetComponent<PlayerShot>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            /*
            // On saisit la direction pour le mouvement
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // ici la droite sera le devant de notre objet
            }*/

            // Son
            if (!isEnemy)
            {
                SoundEffectsHelper.Instance.MakePlayerShotSound();
                shot.owner = owner;
            }
        }
    }

    /// <summary>
    /// L'arme est chargée ?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}