using UnityEngine;

/// <summary>
/// Parallax scrolling
/// </summary>
public class Scrolling : MonoBehaviour
{
    /// <summary>
    /// Vitesse du défilement
    /// </summary>
    public Vector2 speed = new Vector2(2, 2);

    /// <summary>
    /// Direction du défilement
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    /// <summary>
    /// Appliquer le mouvement de scrolling à la caméra ?
    /// </summary>
    public bool isLinkedToCamera = false;

    void Update()
    {
        // Mouvement
        Vector3 movement = new Vector3(
          speed.x * direction.x,
          speed.y * direction.y,
          0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Déplacement caméra
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }
    }
}