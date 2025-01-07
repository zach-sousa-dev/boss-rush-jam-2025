using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNormalFinder : MonoBehaviour
{
    private Vector3 axis;
    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.GetContact(0).thisCollider.transform.gameObject.tag == "CollisionNormalFinder"))
        {
            return;
        }
        axis = collision.GetContact(0).normal;
        Debug.Log("axis");
    }

    /// <summary>
    /// Get the normal vector of the surface of the collision and reset the stored value
    /// </summary>
    /// <returns>
    /// The vector, its equal to Vector3.zero if there is no collision
    /// </returns>
    public Vector3 GetNormalToSurface()
    {
        return axis;
    }
}
