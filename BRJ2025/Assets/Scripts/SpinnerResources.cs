using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerResources : MonoBehaviour
{
    const float momentumDamageScale = 0.00005f;
    const float wearDamageScale = 1.0f;
    const float spinDamageScale = 0.1f;

    [Header("Physics")]
    [SerializeField] private float wearDamageMultiplier = 1.0f;
    [SerializeField] private float spinDamageMultiplier = 1.0f;

    //[SerializeField] private float height;
    //[SerializeField] private float radius;

    [Header("Gameplay")]
    [SerializeField] private float spin;
    [SerializeField] private float wear;
    [SerializeField] private float desiredSpin;
    [SerializeField] private float spinLerpRate = 0.5f;
    [SerializeField] private float spinDeathThreshold = 1.0f;

    [Header("References")]
    public CharacterSpin spinner;
    public Rigidbody rb;
    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxValue(wear);    //  there isn't currently a max wear constant set, so I'm just using the inital value as the 'max'
        healthBar.SetMinValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetCurrentValue(wear);

        //Debug.Log($"Rotational Energy: {GetRotationalEnergy()}J");

        spin = Mathf.Lerp(spin, desiredSpin, spinLerpRate * Time.deltaTime);

        spinner.spinSpeed = spin;

        if (spin <= spinDeathThreshold) {
            SpinDie();
        }
    }

    public void Hit(Vector3 velocity, float mass, Vector3 position, float spinSpeed, float spinBonusMultiplier = 1, float wearBonusMultiplier = 1) {
        Vector3 attackerNormalVelocity = velocity * Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(rb.position - position, velocity));

        Vector3 defenderNormalVelocity = rb.velocity * Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(position - rb.position, rb.velocity));

        Vector3 totalMomentum = attackerNormalVelocity * mass + defenderNormalVelocity * rb.mass;

        Vector3 netVelocity = attackerNormalVelocity - defenderNormalVelocity;

        Vector3 finalVelocity = (totalMomentum + mass * netVelocity) / (mass + rb.mass);

        Vector3 deltaMomentum = rb.mass * (finalVelocity - rb.velocity);

        // |p1| / (|p1| + |p2|)
        float momentumRatio = (attackerNormalVelocity * mass).magnitude / ((attackerNormalVelocity * mass).magnitude + (defenderNormalVelocity * mass).magnitude);

        float spinRatio = spin / (spin + spinSpeed);

        float baseDamage = deltaMomentum.magnitude * momentumDamageScale * (1.0f/momentumRatio) * spinRatio;

        float spinDamage = baseDamage * spinDamageScale * spinBonusMultiplier * spinDamageMultiplier;
        float wearDamage = baseDamage * wearDamageScale * wearBonusMultiplier * wearDamageMultiplier;

        Debug.Log($"{gameObject.name} [Damage: {baseDamage}, Spin Damage: {spinDamage}, Wear Damage: {wearDamage}]");

        desiredSpin = Mathf.Max(0, desiredSpin - spinDamage);
        wear -= wearDamage;

        //if (wear <= 0) {
        //    WearDie();
        //}
    }

    public float GetSpin() {
        return spin;
    }

    private void SpinDie() {
        Destroy(gameObject);
    }

    private void WearDie() {
        Destroy(gameObject);
    }
}
