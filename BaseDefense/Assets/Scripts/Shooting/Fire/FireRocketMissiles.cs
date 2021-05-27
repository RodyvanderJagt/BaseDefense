using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRocketMissiles : FireMissiles
{
    private void Update()
    {
        //fire missiles
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MissileFireProtocol(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            MissileFireProtocol(1);
        }
    }

    private void MissileFireProtocol(int index)
    {
        if(_missilePlaceholder[index].gameObject.activeSelf)
        {
            FireMissile(index);
        }
    }
}
