using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathBox : MonoBehaviour
{
    private Collider2D playerHitbox;
    private BoxCollider2D deathbox;
    private string PlayerDeathMessage {get => "The player has died";}
    [SerializeField] private UltEvents.UltEvent OnPlayerDeath;
    private void Awake() {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player") ?? throw new UnassignedReferenceException("There needs to be an object with the Player tag.");
        if(!playerGO.TryGetComponent<Collider2D>(out playerHitbox))
            throw new UnassignedReferenceException("The player must have a Collider2D");
        deathbox = GetComponent<BoxCollider2D>();
    }

    private void LateUpdate() {
        if(deathbox.IsTouching(playerHitbox))
            OnPlayerDeath.Invoke();
    }
}
