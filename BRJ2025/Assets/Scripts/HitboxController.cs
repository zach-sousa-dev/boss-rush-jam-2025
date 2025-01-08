using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (!collision.collider.CompareTag("Hitbox") || !collision.collider.CompareTag("CollisionNormalFinder")) {
            return;
        }

        if (!collision.GetContact(0).thisCollider.gameObject.TryGetComponent(out ActiveHitbox hitbox)) {
            return;
        }

        SpinnerResources sr = collision.collider.gameObject.GetComponentInParent<SpinnerResources>();

        if (sr == null) {
            return;
        }

        hitbox.Attack(sr);
    }
}
