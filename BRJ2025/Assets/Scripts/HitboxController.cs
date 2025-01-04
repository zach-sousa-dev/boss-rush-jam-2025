using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (!collision.collider.gameObject.TryGetComponent<ActiveHitbox>(out ActiveHitbox hitbox)) {
            return;
        }

        hitbox.Attack();
    }
}
