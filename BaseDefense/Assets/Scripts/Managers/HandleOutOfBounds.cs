using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleOutOfBounds : MonoBehaviour
{
    [SerializeField] static float maxZ = 1000;
    [SerializeField] static float minZ = -50;

    public static event Events.OnDamageToBase OnDamageToBase;

    void Update()
    {
        if (transform.position.z < minZ || transform.position.z > maxZ)
        {
            if (gameObject.GetComponent<EnemyUnit>())
            {
                OnDamageToBase?.Invoke(gameObject.GetComponent<EnemyUnit>().DamageToBase);
            }
            this.gameObject.SetActive(false);
        }

    }
}
