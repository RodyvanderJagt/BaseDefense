using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : EnemyUnit
{
    protected override GameObject GetExplosion()
    {
        return ExplosionManager.Instance.tankExplosionPool.GetAvailableObject();
    }
}
