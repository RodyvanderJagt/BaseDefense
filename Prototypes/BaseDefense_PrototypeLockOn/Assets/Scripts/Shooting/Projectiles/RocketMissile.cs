using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMissile : Projectile
{
    //Flies forward while accelerating
    private Rigidbody missileRb;

    void OnEnable()
    {
        missileRb = GetComponent<Rigidbody>();
        missileRb.velocity = Vector3.zero;
    }

    void Update()
    {
        missileRb.AddForce(transform.forward * _speed);
    }


}
