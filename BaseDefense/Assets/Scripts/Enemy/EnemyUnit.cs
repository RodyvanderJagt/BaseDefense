using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour, IDamageable, IComparable
{
    [Header("Unit properties")]
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] protected float _speed = 100;
    [SerializeField] private int _scoreOnDestroy = 100;
    [SerializeField] private int _damageToBase = 100;

    [SerializeField] private float _minRangeZ = 150;

    public delegate void ChangeActive(EnemyUnit _unit);
    public event ChangeActive OnInvalid;

    public delegate void UpdateInt(int _points);
    public static event UpdateInt OnUnitDied;

    private Rigidbody _unitRb;
    private float _health;
    private bool _isTargetable = true;

    public float Health => _health;
    public bool IsValidTarget => _isTargetable;
    public int DamageToBase => _damageToBase;

    protected virtual void OnEnable()
    {
        _health = _maxHealth;

        //Reset velocity
        _unitRb = GetComponent<Rigidbody>();
        _unitRb.velocity = Vector3.zero;
        _unitRb.angularVelocity = Vector3.zero;
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        //Moved out of range of target selection
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
        OnUnitDied?.Invoke(_scoreOnDestroy);

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
        return null;
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

    public int CompareTo(object obj)
    {
        EnemyUnit otherEnemy = obj as EnemyUnit;
        return this.transform.position.z.CompareTo(otherEnemy.transform.position.z);
    }
}
