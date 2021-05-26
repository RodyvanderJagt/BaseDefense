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
    private ParticleSystem _helicopterSmoke;

    protected override void OnEnable()
    {
        base.OnEnable();

        _helicopterSmoke = GetComponent<ParticleSystem>();

        helicopterRb = GetComponent<Rigidbody>();
        helicopterRb.velocity = transform.forward * _speed;
        helicopterRb.useGravity = false;
        //mainRotor.transform.rotation = Quaternion.identity;
    }

    protected override void Update()
    {
        base.Update();

        mainRotor.transform.Rotate(Vector3.up * mainRotorSpeed, Space.Self);
        tailRotor.transform.Rotate(Vector3.right * tailRotorSpeed, Space.Self);
    }

    protected override void HandleDestruction()
    {
        helicopterRb.angularVelocity = _crashAngularVelocity;
        helicopterRb.useGravity = true;
        _helicopterSmoke.Play();
    }

    protected override GameObject GetExplosion()
    {
        return ExplosionManager.Instance.helicopterExplosionPool.GetAvailableObject();
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.HandleDestruction();
    }

}
