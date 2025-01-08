using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSpin : MonoBehaviour
{
    public Transform tiltTransform;
    public Transform spinTransform;
    public float spinSpeed = 5f;
    public float maxVelocity;   //  NOTE: This should be the same as on PlayerController.cs, and also should prob not be coded in right here and instead referenced in some way
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float tiltSpeed;
    [SerializeField] CollisionNormalFinder collisionNormalFinder;
    [SerializeField] private Vector3 dirNormalized;

    private Vector3 surfaceNormal = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // update to the latest non-zero heading direction
        dirNormalized = rb.velocity.normalized == Vector3.zero ? dirNormalized : rb.velocity.normalized;

        // find the axis which is normal to the surface we are on
        surfaceNormal = collisionNormalFinder.GetNormalToSurface();

        // map the tilt angle based on velocity using built-in Unity functions
        float angleMappedValue = Utils.Map(rb.velocity.sqrMagnitude, 0, maxVelocity*maxVelocity, 0, 30);
        //Debug.Log((Mathf.Acos(Vector3.Dot(surfaceNormal, Vector3.up))) * Mathf.Rad2Deg);

        // apply tilt with slope offset
        //tiltTransform.rotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
        //Debug.Log(Quaternion.FromToRotation(Vector3.up, surfaceNormal).eulerAngles);
        tiltTransform.rotation = Quaternion.Lerp(tiltTransform.rotation, Quaternion.AngleAxis(angleMappedValue, Vector3.Cross(Vector3.up, dirNormalized)) * Quaternion.FromToRotation(Vector3.up, surfaceNormal), tiltSpeed * Time.deltaTime);

        // spin
        spinTransform.RotateAround(tiltTransform.position, tiltTransform.up, GetDegrees(spinSpeed));
    }

    /// <summary>
    /// calculates the degrees to rotate based on the angular velocity
    /// </summary>
    /// <param name="velocity">angular velocity</param>
    /// <returns>degrees to rotate</returns>
    float GetDegrees(float velocity) {
        return (velocity * Time.deltaTime) * Mathf.Rad2Deg;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, surfaceNormal * 10);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dirNormalized * 10);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.Cross(Vector3.up, dirNormalized) * 10);
    }
}
