using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour, IDamageable, IComparable
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _speed = 100;

    public delegate void ChangeActive(EnemyUnit _unit);
    public event ChangeActive OnDestruction;

    private float _health;
    private bool _isVisible = true;

    public float Health => _health;

    private void OnEnable()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

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
        OnDestruction?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        _isVisible = true;
    }
    private void OnBecameInvisible()
    {
        _isVisible = false;
    }

    public bool isValidTarget() 
    {
        if(!gameObject.activeSelf) { return false; }
        if(_health <= 0) { return false;  }
        if(transform.position.z < 150) { return false; }
        return _isVisible;
    }

    public int CompareTo(object obj)
    {
        EnemyUnit otherEnemy = obj as EnemyUnit;
        return this.transform.position.z.CompareTo(otherEnemy.transform.position.z);
    }
}
