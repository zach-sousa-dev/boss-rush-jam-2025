using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpin : MonoBehaviour
{
    public Transform characterTransform;
    public float spinSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        characterTransform.RotateAround(characterTransform.position, Vector3.up, GetDegrees(spinSpeed));
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
