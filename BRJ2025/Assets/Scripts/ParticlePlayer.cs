using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    //[SerializeField] private Transform particleParent;
    //[SerializeField] private ParticleSystem particlePlayer;

    [SerializeField] private GameObject particleSpawner;

    //private Vector3 lastPoint;
    //private Vector3 lastPosition;
    //private Vector3 lastParticlePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        GameObject spark = Instantiate(particleSpawner, collision.GetContact(0).point, Quaternion.AngleAxis(-90, gameObject.transform.up) * Quaternion.LookRotation(collision.GetContact(0).normal, gameObject.transform.up));//fix rotation
        Destroy(spark, 5);
        //TODO: implement object pooling

        /* old implementation
        Vector3 particlePlayerDirection = particleParent.right * -1;//left

        Vector3 collisionDirection = collision.GetContact(0).point - particleParent.transform.position;
        collisionDirection.Normalize();

        collisionDirection.y = particlePlayerDirection.y;

        //Debug.Log(collisionDirection);

        particleParent.rotation *= Quaternion.FromToRotation(particlePlayerDirection, collisionDirection);

        particlePlayer.Play();

        lastPosition = particleParent.position;
        lastPoint = collision.GetContact(0).point;
        lastParticlePosition = particlePlayerDirection * 6 + particleParent.position;
        */
    }
}
