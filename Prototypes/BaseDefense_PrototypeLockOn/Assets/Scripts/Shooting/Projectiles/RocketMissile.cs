using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMissile : Projectile
{
    //Flies forward while accelerating
    private Rigidbody missileRb;
    [SerializeField] private float _aoeRadius;
    [SerializeField] private float _aoeDamage;

    void OnEnable()
    {
        missileRb = GetComponent<Rigidbody>();
        missileRb.velocity = Vector3.zero;
    }

    void Update()
    {
        missileRb.AddForce(transform.forward * _speed);
    }

    protected override void HandleDestruction()
    {
        AOEDamage();
        base.HandleDestruction();
    }

    private void AOEDamage()
    {
        Collider[] CollidersHit = Physics.OverlapSphere(this.transform.position, _aoeRadius);
        foreach (Collider collider in CollidersHit)
        {
            IDamageable damageTaker = collider.gameObject.GetComponent<IDamageable>();
            if (damageTaker != null)
            {
                damageTaker.TakeDamage(_aoeDamage);
            }
        }
    }


}
