using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissiles : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] protected GameObject[] _missilePlaceholder;
    [SerializeField] float _missileCooldown;

    protected virtual void FireMissile(int missileIndex, EnemyUnit target = null)
    {
        GameObject missileToFire = _objectPool.GetAvailableObject();
        if (missileToFire == null) { return; }

        GameObject missilePlaceholder = _missilePlaceholder[missileIndex];
        missilePlaceholder.gameObject.SetActive(false);

        missileToFire.transform.position = missilePlaceholder.transform.position;
        missileToFire.transform.rotation = missilePlaceholder.transform.rotation;
        if (target != null)
        {
            missileToFire.GetComponent<HeatseekingMissile>().Target = target;
        }
        missileToFire.gameObject.SetActive(true);

        StartCoroutine(nameof(MissileCooldown), missileIndex);
    }

    IEnumerator MissileCooldown(int index)
    {
        yield return new WaitForSeconds(_missileCooldown);
        _missilePlaceholder[index].gameObject.SetActive(true);
    }
}
