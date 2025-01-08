using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNormalFinder : MonoBehaviour
{
    private Vector3 axis;
    private void OnCollisionStay(Collision collision)
    {
        if (!(collision.GetContact(0).thisCollider.transform.gameObject.tag == "CollisionNormalFinder"))    //  if not the bottom of the spinner collided
        {
            return;
        }
        axis = collision.GetContact(0).normal;
    }

    private void OnCollisionExit(Collision collision)
    {
        List<ContactPoint> cpList = new List<ContactPoint>();
        collision.GetContacts(cpList);
        foreach (ContactPoint contacts in cpList)
        {
            if((collision.GetContact(0).thisCollider.transform.gameObject.tag == "CollisionNormalFinder"))  //  if the bottom of the spinner is colliding with something
            {
                return;
            }
        }
        axis = Vector3.up;
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
