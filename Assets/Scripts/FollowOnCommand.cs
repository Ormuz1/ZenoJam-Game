using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOnCommand : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothing = 0.2f;
    [SerializeField] private bool followLeft = true;
    [SerializeField] private bool followRight = true;
    [SerializeField] private bool followUp = true;
    [SerializeField] private bool followDown = true;
    private Vector3 velocity;
    private Transform thisTransform;
    private Coroutine runningCoroutine;

     private void Awake() {
        thisTransform = transform;
    }

    public void Follow()
    {
        if (!(runningCoroutine is null))
            StopCoroutine(runningCoroutine);
        runningCoroutine = StartCoroutine(GoToObject());
    }

    private IEnumerator GoToObject()
    {
        Debug.Log("Empezando movimiento.");
        Vector3 startPosition = thisTransform.position;
        Vector3 targetPosition = objectToFollow.position + offset;
        if((!followRight && targetPosition.x > startPosition.x) || (!followLeft && targetPosition.x < startPosition.x))
            targetPosition.x = startPosition.x;
        if((!followUp && targetPosition.y > startPosition.y) || (!followDown && targetPosition.y < startPosition.y))
            targetPosition.y = startPosition.y;
        while (Vector3.Distance(thisTransform.position, targetPosition) > 0.01f)
        {
            thisTransform.position = Vector3.SmoothDamp(thisTransform.position, targetPosition, ref velocity, smoothing);
            yield return null;
        }
        thisTransform.position = targetPosition;
    }
}
