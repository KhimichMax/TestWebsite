using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InvisibleScreenFader : MonoBehaviour
{
    private Image _img;

    private void Start()
    {
        _img = GetComponent<Image>();
        Color color = _img.color;
        color.a = 0f;
        _img.color = color;
    }

    IEnumerator InvisibleImage()
    {
        for (float f = 0.05f; f <= 1f; f += 0.05f)
        {
            Color color = _img.color;
            color.a = f;
            _img.color = color;
            yield return null;
        }
    }

    public void StartInvisible()
    {
        StartCoroutine("InvisibleImage");
    }
}
