using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerResources : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private float KEScaling;
    [SerializeField] private float KESpinScaling;

    [SerializeField] private float wearDamageScale;
    [SerializeField] private float spinDamageScale;

    //[SerializeField] private float height;
    [SerializeField] private float radius;

    [Header("Gameplay")]
    [SerializeField] private float spin;
    [SerializeField] private float wear;

    [Header("References")]
    public CharacterSpin spinner;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Rotational Energy: {GetRotationalEnergy()}J");

        spinner.spinSpeed = spin;
    }

    public void Hit(Vector3 velocity, float mass, float rotationalEnergy, float spinBonusMultiplier = 1, float wearBonusMultiplier = 1) {
        Vector3 relativeVelocity = rb.velocity - velocity;
        float reducedMass = (rb.mass * mass) / (rb.mass + mass);
        float relativeEnergy = 0.5f * relativeVelocity.sqrMagnitude * reducedMass;

        float netRotationalEnergy = (GetRotationalEnergy() - rotationalEnergy) * KESpinScaling;

        Debug.Log($"Energy: {relativeEnergy}J, Rotational Energy: {netRotationalEnergy}J");

        float baseDamage = relativeEnergy * KEScaling + netRotationalEnergy;

        float spinDamage = baseDamage * spinDamageScale;
        float wearDamage = baseDamage * wearDamageScale;

        Debug.Log($"Damage: {baseDamage}, Spin Damage: {spinDamage}, Wear Damage: {wearDamage}");
    }

    public float GetSpin() {
        return spin;
    }

    private float GetRotationalEnergy() {
        //Debug.Log($"Inertia: {Utils.ConeInertiaMoment(rb.mass, radius)}");
        return 0.5f * Utils.ConeInertiaMoment(rb.mass, radius) * Mathf.Pow(spin * Mathf.Deg2Rad, 2);
    }
}
