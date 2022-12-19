using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float smoothing = .2f;
    [SerializeField] private bool followLeft = true;
    [SerializeField] private bool followRight = true;
    [SerializeField] private bool followUp = true;
    [SerializeField] private bool followDown = true;
    private Vector3 velocity;
    private Transform thisTransform;

    private void Awake() {
        thisTransform = transform;
    }

    private void LateUpdate() {
        Vector3 currentPosition = thisTransform.position;
        Vector3 targetPosition = objectToFollow.position + offset;
        if((!followRight && targetPosition.x > currentPosition.x) || (!followLeft && targetPosition.x < currentPosition.x))
            targetPosition.x = currentPosition.x;
        if((!followUp && targetPosition.y > currentPosition.y) || (!followDown && targetPosition.y < currentPosition.y))
            targetPosition.y = currentPosition.y;

        thisTransform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothing);
    }
}
