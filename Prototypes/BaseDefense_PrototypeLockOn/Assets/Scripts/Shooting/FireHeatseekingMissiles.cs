using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHeatseekingMissiles : MonoBehaviour
{
    [SerializeField] Projectile _missile;
    [SerializeField] GameObject[] _missilePlaceholder;

    [SerializeField] float _missileCooldown;
    [SerializeField] float lockonTime;

    private List<EnemyUnit> _validTargets = new List<EnemyUnit>();
    private EnemyUnit[] _targets;

    private void Start()
    {
        _targets = FindObjectsOfType<EnemyUnit>();
    }

    private void Update()
    {
        //Lock on targets
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(nameof(AcquireTargets));
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopCoroutine(nameof(AcquireTargets));
        }
        //fire missiles
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MissileFireProtocol();
        }
    }

    IEnumerator AcquireTargets()
    {
        yield return new WaitForSeconds(lockonTime);

        _validTargets.Clear();
        foreach (EnemyUnit t in _targets)
        {
            if (t != null)
            {
                if (t.isValidTarget())
                {
                    _validTargets.Add(t);
                }
            }
        }
    }

    private void MissileFireProtocol()
    {
        if (_validTargets == null) { return; }

        if (_validTargets.Count == 0) { return; }

        int missileIndex = Random.Range(0, 2);

        if (_missilePlaceholder[missileIndex].gameObject.activeSelf)
        {
            FireMissile(missileIndex, _validTargets[0]);
            if (_missilePlaceholder[missileIndex ^ 1].gameObject.activeSelf && _validTargets.Count > 1)
            {
                FireMissile(missileIndex ^ 1, _validTargets[1]);
            }
        }
        else if (_missilePlaceholder[missileIndex ^ 1].gameObject.activeSelf)
        {
            FireMissile(missileIndex ^ 1, _validTargets[0]);
        }
        _validTargets.Clear();
    }

    private void FireMissile(int missileIndex, EnemyUnit target)
    {
        GameObject missilePlaceholder = _missilePlaceholder[missileIndex];
        missilePlaceholder.gameObject.SetActive(false);
        Projectile missileToFire = Instantiate(_missile, missilePlaceholder.transform.position, missilePlaceholder.transform.rotation);
        missileToFire.GetComponent<HeatseekingMissile>().Target = target;
        StartCoroutine(nameof(MissileCooldown), missileIndex);
    }

    IEnumerator MissileCooldown()
    {
        yield return new WaitForSeconds(_missileCooldown);
        _missilePlaceholder[0].gameObject.SetActive(true);
        _missilePlaceholder[1].gameObject.SetActive(true);
    }
}
