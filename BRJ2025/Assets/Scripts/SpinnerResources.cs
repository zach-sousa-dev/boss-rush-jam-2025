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

    public void Hit(float kineticEnergy, float rotationalEnergy, float spinBonusMultiplier = 1, float wearBonusMultiplier = 1) {
        float netEnergy = (GetKineticEnergy() - kineticEnergy) * KEScaling;

        float netRotationalEnergy = (GetRotationalEnergy() - rotationalEnergy) * KESpinScaling * -1;//invert to make faster speed stronger

        Debug.Log($"Energy: {netEnergy}J, Rotational Energy: {netRotationalEnergy}J");

        float baseDamage = Mathf.Max(netEnergy + netRotationalEnergy, 0);

        float spinDamage = baseDamage * spinDamageScale;
        float wearDamage = baseDamage * wearDamageScale;

        Debug.Log($"Damage: {baseDamage}, Spin Damage: {spinDamage}, Wear Damage: {wearDamage}");

        desiredSpin = Mathf.Max(0, desiredSpin - spinDamage);
        wear -= wearDamage;

        if (wear <= 0) {
            WearDie();
        }
    }

    public float GetSpin() {
        return spin;
    }

    public float GetRotationalEnergy() {
        //Debug.Log($"Inertia: {Utils.ConeInertiaMoment(rb.mass, radius)}");
        return 0.5f * Utils.ConeInertiaMoment(rb.mass, radius) * Mathf.Pow(spin * Mathf.Deg2Rad, 2);
    }

    public float GetKineticEnergy() {
        return 0.5f * rb.mass * rb.velocity.sqrMagnitude;
    }

    private void SpinDie() {
        Destroy(gameObject);
    }

    private void WearDie() {
        Destroy(gameObject);
    }
}
