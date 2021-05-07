using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatseekingMissile : Projectile
{
    [SerializeField] float rotationSpeed;

    private EnemyUnit _target;
    private Vector3 _targetCom;

    public EnemyUnit Target
    {
        set { _target = value; }
    }

    void Start()
    {
        if (_target != null)
        {
            _targetCom = _target.GetComponent<Rigidbody>().centerOfMass;
        }
    }
    void Update()
    {
        if (_target != null)
        {
            //Move to target like a heatseeking missile
            Vector3 targetDirection = (_target.transform.position + _targetCom - transform.position);

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.position += transform.forward * _speed * Time.deltaTime;
        }
        else
        {
            //Without target, remove itself from game.
            Destroy(gameObject);
        }

    }
}
