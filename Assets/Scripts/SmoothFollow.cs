using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float smoothing = .2f;
    [SerializeField] private bool followHorizontally = false;
    [SerializeField] private bool followVertically = true;
    private Vector3 velocity;
    private Transform thisTransform;

    private void Awake() {
        thisTransform = transform;
    }
    private void LateUpdate() {
        Vector3 currentPosition = thisTransform.position;
        Vector3 targetPosition = objectToFollow.position + offset;
        if(!followHorizontally)
            targetPosition.x = currentPosition.x;
        if(!followVertically)
            targetPosition.y = currentPosition.y;
        
        thisTransform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothing);
    }
}
