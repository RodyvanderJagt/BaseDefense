using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairLockOn : MonoBehaviour
{
    private Image _crosshairImage;

    [SerializeField] private Color _beginColor = Color.black;
    [SerializeField] private Color _finalColor = Color.red;

    private float t = 0;

    private void Start()
    {
        _crosshairImage = GetComponent<Image>();
    }
    private void Update()
    {
        if (t < 1)
        {
            _crosshairImage.color = Color.Lerp(_beginColor, _finalColor, t);
            t += Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        t = 0;
        gameObject.transform.rotation = Quaternion.identity;
    }
}
