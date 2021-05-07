using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 100;

    public float Health => _health;

    public void TakeDamage(float damageTaken)
    {
        _health -= damageTaken; 
        if (_health <= 0)
        {
            HandleDestruction();
        }
    }

    protected virtual void HandleDestruction()
    {
        Destroy(gameObject);
    }

    public bool isValidTarget() //Don't like this very much
    {
        if(!gameObject.activeSelf) { return false; }
        if(_health <= 0) { return false;  }
        if(transform.position.z > 1000 || 100 > transform.position.z) { return false; }
        return true;
    }

}
