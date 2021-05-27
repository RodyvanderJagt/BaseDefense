using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHeatseekingMissiles : FireMissiles
{
    [SerializeField] float lockonTime;

    [SerializeField] private List<GameObject> _crosshairs = new List<GameObject>();
    [SerializeField] private Vector3 _crosshairOffset = Vector3.zero;

    private List<EnemyUnit> _validTargets = new List<EnemyUnit>();
    private EnemyUnit[] _targets;

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

        //fire missiles
        if (Input.GetKeyDown(KeyCode.Space) && _bCanFire)
        {
            MissileFireProtocol();
        }
    }
    #region Target selection

    IEnumerator AcquireTargets()
    {
        _bCanFire = false;

        _targets = FindObjectsOfType<EnemyUnit>();

        GetValidTargets();

        yield return new WaitForSeconds(lockonTime);

        _bCanFire = true;
    }

    private void GetValidTargets()
    {
        ClearTargets();
        foreach (EnemyUnit t in _targets)
        {
            if (t != null)
            {
                if (t.IsValidTarget)    
                {
                    _validTargets.Add(t);
                }
            }
        }
        _validTargets.Sort();

        TrackTargets();
    }   

    private void TrackTargets()
    {
        for (int i = 0; i < Mathf.Min(_crosshairs.Count, _validTargets.Count); i++)
        {
            GameObject ch = _crosshairs[i];
            if (_validTargets[i] != null)
            {
                ch.gameObject.SetActive(true);
                ch.transform.SetParent(_validTargets[i].transform);
                ch.transform.localPosition = _crosshairOffset;

                _validTargets[i].OnInvalid += RemoveFromValidTargets;
            }
            else
            {
                ch.transform.SetParent(null);
                ch.gameObject.SetActive(false);
            }
        }
    }
    private void RemoveFromValidTargets(EnemyUnit _removedUnit)
    {
        _removedUnit.OnInvalid -= RemoveFromValidTargets;
        if(_validTargets.Remove(_removedUnit))
        {
            _validTargets.Insert(Mathf.Min(1, _validTargets.Count), null);
        }
        TrackTargets();
    }

    private void ClearTargets()
    {
        _validTargets.Clear();
        foreach (GameObject ch in _crosshairs)
        {
            ch.SetActive(false);
        }
    }

    #endregion

    private void MissileFireProtocol()
    {
        if (!_bCanFire) { return; }
        if (_validTargets == null) { return; }

        if (_validTargets.Count == 0) { return; }

        int missileIndex = Random.Range(0, 2);

        if (_missilePlaceholder[missileIndex].gameObject.activeSelf)
        {
            if(_validTargets[0] != null)
                FireMissile(missileIndex, _validTargets[0]);

            if (_missilePlaceholder[missileIndex ^ 1].gameObject.activeSelf && _validTargets.Count > 1)
            {
                if (_validTargets[1] != null)
                    FireMissile(missileIndex ^ 1, _validTargets[1]);
            }
            ClearTargets();
        }
        else if (_missilePlaceholder[missileIndex ^ 1].gameObject.activeSelf)
        {
            if (_validTargets[0] != null)
                FireMissile(missileIndex ^ 1, _validTargets[0]);
            ClearTargets();
        }
    }
}
