using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Menu : MonoBehaviour
{
    TextMeshProUGUI[] _textMeshes;


    private void Awake()
    {
        _textMeshes = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        UIManager.Instance.OnFontSizeChangedRatio += HandleFontSizeChanged;
    }

    protected void HandleFontSizeChanged(float changeRatio)
    {
        if (changeRatio != 1.0f)
        {
            foreach (TextMeshProUGUI tm in _textMeshes)
            {
                tm.fontSize *= changeRatio;
            }
        }
    }

}
