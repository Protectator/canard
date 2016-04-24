using UnityEngine;

/// <summary>
/// Effets de particules sans efforts
/// </summary>
public class SpecialEffectsHelper : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static SpecialEffectsHelper Instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    void Awake()
    {
        // On garde une référence du singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }

        Instance = this;
    }

    /// <summary>
    /// Création d'une explosion à l'endroit indiqué
    /// </summary>
    /// <param name="position"></param>
    public void Explosion(Vector3 position)
    {
        // Smoke on the water
        instantiate(smokeEffect, position);

        // Tu tu tu, tu tu tudu

        // Fire in the sky
        instantiate(fireEffect, position);
    }

    /// <summary>
    /// Création d'un effet de particule depuis un prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;

        // Destruction programmée
        Destroy(
          newParticleSystem.gameObject,
          newParticleSystem.startLifetime
        );

        return newParticleSystem;
    }
}