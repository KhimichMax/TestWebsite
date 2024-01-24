using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private float _fadeSpeed = 1f;

    IEnumerator Start()
    {
        Image fadeImage = GetComponent<Image>();
        Color color = fadeImage.color;

        while (color.a < 1f)
        {
            color.a += _fadeSpeed * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
    }
}
