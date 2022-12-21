using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatInfinetlyUpward : MonoBehaviour
{
    private Transform player;
    private float childYExtents;
    private float highestChildPosition;
    private float lowestChildPosition;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SpriteRenderer childSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        highestChildPosition = childSpriteRenderer.bounds.max.y;
        lowestChildPosition = childSpriteRenderer.bounds.min.y;
        childYExtents = childSpriteRenderer.bounds.extents.y;
    }

    private void Update() {
 
        if (highestChildPosition - player.position.y < 10)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = highestChildPosition + childYExtents;
            Transform newChild = Instantiate(transform.GetChild(0), newPosition, Quaternion.identity, transform);
            SpriteRenderer newChildRenderer = newChild.GetComponent<SpriteRenderer>();
            highestChildPosition = newChildRenderer.bounds.max.y;
        }
        else if (lowestChildPosition - player.position.y < -50)
        {
            lowestChildPosition = transform.GetChild(1).GetComponent<SpriteRenderer>().bounds.min.y;
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
