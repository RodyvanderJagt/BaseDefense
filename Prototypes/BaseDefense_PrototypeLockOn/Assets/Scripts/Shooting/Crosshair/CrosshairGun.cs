using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairGun : MonoBehaviour
{
    [Header("sizes")]
    [SerializeField] float _OnHitScale = 1;
    private Vector3 _defaultScale;
    [SerializeField] float _hitTimer;

    private void Start()
    {
        FireGun.OnHitUnit += OnHitUnitHandler;
        _defaultScale = transform.localScale;
    }

    private void OnHitUnitHandler()
    {
        StopCoroutine(nameof(ChangeSize));
        StartCoroutine(nameof(ChangeSize));
    }
     
    IEnumerator ChangeSize()
    {
        transform.localScale = Vector3.one * _OnHitScale;
        yield return new WaitForSeconds(_hitTimer);
        transform.localScale = _defaultScale;
    }
}
