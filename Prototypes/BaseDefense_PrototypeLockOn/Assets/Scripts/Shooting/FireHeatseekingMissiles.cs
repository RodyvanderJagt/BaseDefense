using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHeatseekingMissiles : MonoBehaviour
{
    [SerializeField] Projectile _missile;
    [SerializeField] GameObject[] _missilePlaceholder;

    [SerializeField] float _missileCooldown;
    [SerializeField] float lockonTime;

    [SerializeField] private List<GameObject> _crosshairs = new List<GameObject>();

    [SerializeField] private List<EnemyUnit> _validTargets = new List<EnemyUnit>();
    [SerializeField] private EnemyUnit[] _targets;

    private bool _bCanFire = true;

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
            _bCanFire = true;
        }
        //fire missiles
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MissileFireProtocol();
        }
    }

    IEnumerator AcquireTargets()
    {
        _bCanFire = false;

        GetValidTargets();

        for (int i = 0; i < Mathf.Min(_crosshairs.Count, _validTargets.Count); i++)
        {
            if (_validTargets[i] != null)
            {
                GameObject ch = _crosshairs[i];
                ch.gameObject.SetActive(true);
                ch.transform.SetParent(_validTargets[i].transform);
                ch.transform.localPosition = Vector3.zero; 
            }
        }

        yield return new WaitForSeconds(lockonTime);

        _bCanFire = true;
    }

    private void GetValidTargets()
    {
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
        _validTargets.Sort();
    }

    private void ClearTargets()
    {
        _validTargets.Clear();
        foreach (GameObject ch in _crosshairs)
        {
            ch.SetActive(false);
        }
    }

    private void MissileFireProtocol()
    {
        if (!_bCanFire) { return; }
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
            ClearTargets();
        }
        else if (_missilePlaceholder[missileIndex ^ 1].gameObject.activeSelf)
        {
            FireMissile(missileIndex ^ 1, _validTargets[0]);
            ClearTargets();
        }
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
