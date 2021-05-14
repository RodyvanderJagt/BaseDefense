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
        if(damageTaker != null)
        {
            damageTaker.TakeDamage(_damage);
        }
        HandleDestruction();
    }

    protected virtual void HandleDestruction()
    {
        ParticleSystem explosionToPlay = ExplosionManager.Instance.projectileExplosionPool.GetAvailableObject().GetComponent<ParticleSystem>();
        if (explosionToPlay != null)
        {
            explosionToPlay.transform.position = this.transform.position;
            explosionToPlay.gameObject.SetActive(true);
            explosionToPlay.Play();
        }
        gameObject.SetActive(false);
        
    }

}
