using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{
    public float Left {get => transform.localPosition.x - Size / 2;}
    public float Right {get => transform.localPosition.x + Size / 2;}
    public float Size { get => spriteRenderer.bounds.size.x; }
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
