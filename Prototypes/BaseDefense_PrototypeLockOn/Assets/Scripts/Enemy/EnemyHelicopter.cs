using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopter : EnemyUnit
{
    //Helicopter
    private Rigidbody helicopterRb;

    //Rotors
    public GameObject mainRotor;
    public GameObject tailRotor;
    private readonly float mainRotorSpeed = 55;
    private readonly float tailRotorSpeed = 35;

    //Crash
    [Header("Crash")]
    [SerializeField] private Vector3 _crashAngularVelocity = Vector3.zero;

    protected override void OnEnable()
    {
        base.OnEnable();

        helicopterRb = GetComponent<Rigidbody>();
        helicopterRb.velocity = transform.forward * _speed;
        helicopterRb.useGravity = false;
        mainRotor.transform.rotation = Quaternion.identity;
    }

    protected override void Update()
    {
        base.Update();

        mainRotor.transform.Rotate(transform.up * mainRotorSpeed);
        tailRotor.transform.Rotate(transform.right * tailRotorSpeed);
    }

    protected override void HandleDestruction()
    {
        helicopterRb.angularVelocity = _crashAngularVelocity;
        helicopterRb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.HandleDestruction();
    }

}
