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

    private ParticleSystem _helicopterSmoke;
    private AudioSource _audioSource;


    protected override void OnEnable()
    {
        base.OnEnable();

        _helicopterSmoke = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = 1;

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
        helicopterRb.angularVelocity = GenerateRandomAngularVelocity();
        helicopterRb.useGravity = true;
        _helicopterSmoke.Play();
        _audioSource.pitch = Random.Range(1.8f, 2.2f);
    }

    private Vector3 GenerateRandomAngularVelocity()
    {
        return new Vector3(Random.Range(-2, 2), Random.Range(5, 5), 0);
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
