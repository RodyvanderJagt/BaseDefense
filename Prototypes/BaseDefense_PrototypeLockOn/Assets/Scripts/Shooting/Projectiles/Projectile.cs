using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _damage;


    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageTaker = other.GetComponent<IDamageable>();
        if(damageTaker != null)
        {
            damageTaker.TakeDamage(_damage);
        }
        HandleDestruction();
    }

    protected virtual void HandleDestruction()
    {
        gameObject.SetActive(false);
    }

}
