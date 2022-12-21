using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    Collider2D thisHurtbox;
    Collider2D playerHurtbox;
    [SerializeField] float destroyDelay;
    [SerializeField] private float fallDelay;
    [SerializeField] private float maxFallSpeed = 13f;
    [SerializeField] private float timeToMaxFallSpeed = 1f;
    private float fallTime;
    private bool isFalling = false;
    private ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        thisHurtbox = GetComponent<Collider2D>();
        playerHurtbox = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().hurtbox;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(isFalling && _rigidbody2D.bodyType == RigidbodyType2D.Kinematic)
        {
            transform.Translate(Vector3.down * (fallTime / timeToMaxFallSpeed * maxFallSpeed) * Time.deltaTime);
            fallTime = Mathf.Clamp(fallTime + Time.deltaTime, 0, timeToMaxFallSpeed);
        }

        if(!isFalling && thisHurtbox.IsTouching(playerHurtbox) && playerHurtbox.transform.position.y > transform.position.y)
        {
            isFalling = true;
            Invoke(nameof(PlatformFall), fallDelay);
        }
    }   
    public void PlatformFall()
    { 
        fallTime = 0;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        Destroy(gameObject, destroyDelay);
    }
}
