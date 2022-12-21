using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static void InvokeAction(this MonoBehaviour mb, System.Action f, float delay)
    {
        mb.StartCoroutine(InvokeCoroutine(f, delay));
    }
    private static IEnumerator InvokeCoroutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }

    public static void FlickerSprite(this SpriteRenderer sprite, float duration, float flickerInterval)
    {
        sprite.GetComponent<MonoBehaviour>().StartCoroutine(FlickerSpriteCoroutine(sprite, duration, flickerInterval));
    }

    private static IEnumerator FlickerSpriteCoroutine(SpriteRenderer sprite, float duration, float flickerInterval)
    {
        WaitForSeconds wait = new WaitForSeconds(flickerInterval);
        for(float timer = duration; timer > 0; timer -= flickerInterval)
        {
            sprite.enabled = !sprite.enabled;
            yield return wait;
        }
        sprite.enabled = true;
    }
}
