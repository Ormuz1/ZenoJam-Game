using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            PlatformFall(1, 0.5f);
        }
    }

    void PlatformFall(float fallDelay, float destroyDelay)
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        Invoke(nameof(PlatformFall), fallDelay);
        Destroy(gameObject, destroyDelay);
    }
}
