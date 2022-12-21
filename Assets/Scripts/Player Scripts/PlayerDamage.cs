using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public Collider2D hurtbox;
    [SerializeField] private UltEvents.UltEvent OnPlayerHurtEvent;
    private bool isVulnerable = true;

    public void MakePlayerInvulnerable(float duration)
    {
        isVulnerable = false;
        this.InvokeAction(() => isVulnerable = true, duration);
    }

    public Vector2 CalculateKnockback(float knockbackForce)
    {
        List<Collider2D> contacts = new List<Collider2D>();
        hurtbox.GetContacts(contacts);
        foreach (Collider2D contact in contacts)
        {
            if(contact.TryGetComponent<EnemyBase>(out _))
            {
                return Vector2.right * Mathf.Sign(transform.position.x - contact.transform.position.x) * knockbackForce;
            }
        }
        return Vector2.zero;
    }

    public void OnPlayerHurt()
    {
        if(isVulnerable)
            OnPlayerHurtEvent.Invoke();
    }
}