using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class NormalEnemy : EnemyBase
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private LayerMask groundLayer;
    private int direction = 1;
    private Renderer sprite;
    private Vector3 bottomLeftCorner, bottomRightCorner;
    private Collider2D thisCollider;
    private PlayerDamage playerDamage;

    private void Awake() {
        thisCollider = GetComponent<BoxCollider2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerDamage = playerObject.GetComponent<PlayerDamage>();
        sprite = GetComponent<Renderer>();

    }

    private void Update() {
        bottomLeftCorner = sprite.bounds.min;
        bottomRightCorner = bottomLeftCorner;
        bottomRightCorner.x += sprite.bounds.size.x;

        if(EnemyReachedTheCorner())
            direction *= -1;
        transform.position += Vector3.right * speed * direction * Time.deltaTime;
        if(thisCollider.IsTouching(playerDamage.hurtbox))
            playerDamage.OnPlayerHurt();
    }

    private bool EnemyReachedTheCorner()
    {
        Vector3 rayOrigin = direction == 1 ? bottomRightCorner : bottomLeftCorner;
        Debug.DrawRay(rayOrigin, Vector3.down, direction == 1 ? Color.red : Color.blue);
        return Physics2D.Raycast(rayOrigin, Vector3.down, 1, groundLayer).collider is null;
    }
}
