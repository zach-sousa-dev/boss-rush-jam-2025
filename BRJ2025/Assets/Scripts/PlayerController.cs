using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float maxVelocity;
    private Rigidbody rb;

    [Header("Config")]
    [SerializeField] private Vector3 directionalHitboxOffset;
    [SerializeField] private float directionalHitboxRadius;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // used for any physics based actions
    void FixedUpdate()
    {
        // only apply force if not at max velocity
        if(rb.velocity.sqrMagnitude < maxVelocity*maxVelocity)
        {
            // add a force to the rigidbody based on the WASD * speed NOTE: ForceMode.Force takes into consideration the mass, use ForceMode.Acceleration if you want to ignore it
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed, ForceMode.Force);
        }
        //Debug.Log(rb.velocity.magnitude);
    }

    /// <summary>
    /// Used to get the max velocity that the player can travel at - regardless of 
    /// any factors like wear or health. The maximum possible velocity.
    /// </summary>
    /// <returns>
    /// The max velocity float
    /// </returns>
    public float GetMaxVelocity()
    {
        return maxVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(rb.velocity);
    }

    // Draw Gizmos
    private void OnDrawGizmosSelected()
    {
        /*
        // Draw directional collision spheres
        // Right side
        Gizmos.color = Color.white; // The XYZ on the right sphere is 'true' to the actual value of directionalHitboxOffset, so make it white to differentiate
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + directionalHitboxOffset.x, transform.position.y + directionalHitboxOffset.y, transform.position.z + directionalHitboxOffset.z), directionalHitboxRadius);
        Gizmos.color = Color.blue;
        // Left side
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + -directionalHitboxOffset.x, transform.position.y + directionalHitboxOffset.y, transform.position.z + -directionalHitboxOffset.z), directionalHitboxRadius);
        // Top side
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + -directionalHitboxOffset.z, transform.position.y + directionalHitboxOffset.y, transform.position.z + directionalHitboxOffset.x), directionalHitboxRadius);
        // Bottom side
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + directionalHitboxOffset.z, transform.position.y + directionalHitboxOffset.y, transform.position.z + -directionalHitboxOffset.x), directionalHitboxRadius);
        */
    }
}
