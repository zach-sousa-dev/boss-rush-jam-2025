using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private float pitchVariance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSrc.pitch = 1 - Random.Range(-pitchVariance, pitchVariance);
        audioSrc.PlayOneShot(collisionSound);
    }
}
