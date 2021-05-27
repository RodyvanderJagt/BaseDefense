using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionManager : Singleton<ExplosionManager>
{
    [SerializeField] public ObjectPool projectileExplosionPool;
    [SerializeField] public ObjectPool rocketExplosionPool;
    [SerializeField] public ObjectPool tankExplosionPool;
    [SerializeField] public ObjectPool helicopterExplosionPool;

}
