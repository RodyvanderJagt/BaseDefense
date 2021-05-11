using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour, IDamageable, IComparable
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] protected float _speed = 100;
    [SerializeField] private float _minRangeZ = 150;

    public delegate void ChangeActive(EnemyUnit _unit);
    public event ChangeActive OnInvalid;

    private float _health;
    [SerializeField] private bool _isTargetable = true;

    private Rigidbody _unitRb;

    public float Health => _health;

    protected virtual void OnEnable()
    {
        _health = _maxHealth;
        _unitRb = GetComponent<Rigidbody>();
        _unitRb.velocity = Vector3.zero;
        _unitRb.angularVelocity = Vector3.zero;
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        if(transform.position.z < _minRangeZ && _isTargetable)
        {
            MakeUntargetable();
        }
    }

    public void TakeDamage(float damageTaken)
    {
        _health -= damageTaken; 
        if (_health <= 0)
        {
            MakeUntargetable();
            HandleDestruction();
        }
    }

    protected virtual void HandleDestruction()
    {
        gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        _isTargetable = true;
    }
    private void OnBecameInvisible()
    {
        MakeUntargetable();
    }

    private void MakeUntargetable()
    {
        if (_isTargetable)
        {
            OnInvalid?.Invoke(this);
            _isTargetable = false;
        }
    }

    public bool IsValidTarget() 
    {
        if(!gameObject.activeSelf) { return false; }
        if(_health <= 0) { return false;  }
        return _isTargetable;
    }

    public int CompareTo(object obj)
    {
        EnemyUnit otherEnemy = obj as EnemyUnit;
        return this.transform.position.z.CompareTo(otherEnemy.transform.position.z);
    }
}
