using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _damage;


    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageTaker = other.GetComponent<IDamageable>();
        if (damageTaker != null)
        {
            damageTaker.TakeDamage(_damage);
        }
        HandleDestruction();
    }

    protected virtual void HandleDestruction()
    {
        GameObject explosion = GetExplosion();
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    protected virtual GameObject GetExplosion()
    {
        return ExplosionManager.Instance.projectileExplosionPool.GetAvailableObject();
    }
}


