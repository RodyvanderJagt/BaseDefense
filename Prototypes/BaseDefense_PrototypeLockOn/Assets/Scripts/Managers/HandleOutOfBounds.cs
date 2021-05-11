using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleOutOfBounds : MonoBehaviour
{
    [SerializeField] static float maxZ = 1000;
    [SerializeField] static float minZ = -50;

    public delegate void OnBaseDamaged(int _damageToBase);
    public static event OnBaseDamaged OnUnitInBase;


    void Update()
    {
        if (transform.position.z < minZ)
        {
            if (GetComponent<EnemyUnit>())
            {
                OnUnitInBase?.Invoke(GetComponent<EnemyUnit>().DamageToBase);
                Debug.Log("Base!");
            }
            this.gameObject.SetActive(false);
        }
    }
}
