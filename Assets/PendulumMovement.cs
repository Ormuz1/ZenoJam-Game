using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    [SerializeField] private float maxAngleDeflection = 30.0f;
    [SerializeField] private float speedOfPendulum = 1;
    float lastAngle = 0;
    private LineRenderer lineRenderer;


    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.parent.position);
    }

    void Update()
    {
        float angle = maxAngleDeflection * Mathf.Sin( Time.time * speedOfPendulum );
        transform.parent.localRotation = Quaternion.Euler( 0, 0, angle);

        Vector3 direction = angle > lastAngle ? Vector3.right : Vector3.left;
        transform.Translate(direction * 1 * Time.deltaTime);
        lastAngle = angle;
        lineRenderer.SetPosition(1, transform.position);    
    }
}
