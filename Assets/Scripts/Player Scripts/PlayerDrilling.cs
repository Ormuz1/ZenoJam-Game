using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDrilling : MonoBehaviour
{
    [SerializeField] private Transform drillPivot;
    [SerializeField] private Collider2D drillHitbox;
    [SerializeField] private LayerMask drillableLayer;
    [SerializeField] private float drillSpeed;
    [SerializeField] private float speedWhenComingOut;
    [SerializeField] private UltEvents.UltEvent OnStartDrilling;
    [SerializeField] private UltEvents.UltEvent OnStopDrilling;
    [SerializeField] private UltEvents.UltEvent OnDrilling;
    public Vector2 drillVelocity {get; set;}
    private ContactFilter2D drillFilter;
    private List<Collider2D> drillOverlapColliders = new List<Collider2D>();
    private bool isDrilling = false;
    private Collider2D playerHitbox;
    private Quaternion drillStartRotation;
    [SerializeField] private SpriteMask drillSpriteMask;
    private SpriteMask lastSpriteMask;
    private void Awake() {
        drillFilter.SetLayerMask(drillableLayer);
        playerHitbox = GetComponent<Collider2D>();
        drillStartRotation = drillPivot.rotation;
    }

    private void Update() {

        if(!Input.GetButton("Fire1"))
        {
            drillPivot.rotation = drillStartRotation;
            return;
        }

        RotateTowardsMouse(drillPivot);
        
        if (drillHitbox.OverlapCollider(drillFilter, drillOverlapColliders) == 0)
        {
            if(isDrilling && playerHitbox.OverlapCollider(drillFilter, drillOverlapColliders) == 0)
            {
                drillVelocity *= 1.5f;
                OnStopDrilling.Invoke();
                isDrilling = false;
                return;
            }
            if(!isDrilling)
                return;
        }

        if(!isDrilling)  
        {
            isDrilling = true;
            OnStartDrilling.Invoke();
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Vector2.Distance(mousePosition, transform.position) < .2)
            return;
        Vector2 mouseDirection = (mousePosition - (Vector2) transform.position).normalized;
        drillVelocity = mouseDirection * drillSpeed;
        transform.Translate(drillVelocity * Time.deltaTime);
        if(lastSpriteMask == null || !lastSpriteMask.bounds.Contains(transform.position))
        {
            lastSpriteMask = Instantiate(drillSpriteMask, transform.position, Quaternion.identity);
            lastSpriteMask.transform.SetParent(drillOverlapColliders[0].transform, true);
        }
    }

    private void RotateTowardsMouse(Transform objectToRotate)
    {
        Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - objectToRotate.position;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        objectToRotate.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
