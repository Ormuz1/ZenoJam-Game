using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLock : MonoBehaviour
{

    [SerializeField] private Transform anchor;
    [SerializeField] private Vector3 maxDistance = Vector3.positiveInfinity;

    private void Update() {
        Vector3 newPosition = transform.position;
        Vector3 currentDistance = anchor.position - newPosition;
        
        if(Mathf.Abs(currentDistance.x) > maxDistance.x)
            newPosition.x = anchor.position.x - maxDistance.x;
        if(Mathf.Abs(currentDistance.y) > maxDistance.y)
            newPosition.y = anchor.position.y - maxDistance.y;
        if(Mathf.Abs(currentDistance.z) > maxDistance.z)
            newPosition.z = anchor.position.z - maxDistance.z;
        
        transform.position = newPosition;
    }
}
