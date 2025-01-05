using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHitbox : MonoBehaviour
{
    public float spinBonusMultiplier = 1;
    public float wearBonusMultiplier = 1;

    [SerializeField] private SpinnerResources spinner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(SpinnerResources enemy) {
        //Debug.Log($"Current Velocity: {spinner.rb.velocity}");
        enemy.Hit(spinner.GetKineticEnergy(), spinner.GetRotationalEnergy(), spinBonusMultiplier, wearBonusMultiplier);
    }
}
