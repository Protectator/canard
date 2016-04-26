using UnityEngine;

/// <summary>
/// Comportement des tirs
/// </summary>
public class ShotScript : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Points de dégâts infligés
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Projectile ami ou ennemi ?
    /// </summary>
    public bool isEnemyShot = false;

    void Start()
    {
        // 2 - Destruction programmée
        Destroy(gameObject, 20); // 20sec
    }
}