using UnityEngine;

public class DamageHealthComponent : MonoBehaviour
{
    [SerializeField]
    private float _changeHealth;
    [SerializeField]
    private string _tagForInteraction;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag.Contains(_tagForInteraction))
        {
            if (collision.collider.gameObject.TryGetComponent<HealthComponent>(out var healthComponent))
            {
                healthComponent.ChangeHealth(_changeHealth);
            }
        }
    }
}
