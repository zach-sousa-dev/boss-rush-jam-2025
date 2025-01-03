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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // map the tilt angle based on velocity using built-in Unity functions
        float velocityMappedValue = Mathf.InverseLerp(0, Mathf.Pow(maxVelocity, 2), rb.velocity.sqrMagnitude);
        float angleMappedValue = Mathf.Lerp(0, 30, velocityMappedValue);
        // apply tilt
        tiltTransform.rotation = Quaternion.AngleAxis(angleMappedValue, Vector3.Cross(Vector3.up, rb.velocity.normalized));

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
}
