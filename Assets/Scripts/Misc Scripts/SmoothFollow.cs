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
    private Vector3 velocity = Vector3.zero;
    private Transform thisTransform;
    private Vector3 targetPosition;
    private Vector3 smoothedMovement;
    private Vector3 currentPosition;
    private void Awake() {
        thisTransform = transform;
        Vector3 currentPosition = thisTransform.position;
        targetPosition = objectToFollow.position + offset;
        if((!followRight && targetPosition.x > currentPosition.x) || (!followLeft && targetPosition.x < currentPosition.x))
            targetPosition.x = currentPosition.x;
        if((!followUp && targetPosition.y > currentPosition.y) || (!followDown && targetPosition.y < currentPosition.y))
        {
            targetPosition.y = currentPosition.y;
        }
        thisTransform.position = targetPosition;
    }

    private void LateUpdate() {
        currentPosition = thisTransform.position;
        targetPosition = objectToFollow.position + offset;
        if((!followRight && targetPosition.x > currentPosition.x) || (!followLeft && targetPosition.x < currentPosition.x))
            targetPosition.x = currentPosition.x;
        if((!followUp && targetPosition.y > currentPosition.y) || (!followDown && targetPosition.y < currentPosition.y))
        {
            targetPosition.y = currentPosition.y;
        }
        
        smoothedMovement = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothing);
        thisTransform.position = smoothedMovement;
    }

    public void SetFollowUp(bool value)
    {
        followUp = value;
    }
}
