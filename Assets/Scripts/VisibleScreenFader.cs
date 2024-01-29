using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class VisibleScreenFader : MonoBehaviour
{
    private Image _img;

    private void Start()
    {
        _img = GetComponent<Image>();
    }

    IEnumerator VisibleImage()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color color = _img.material.color;
            color.a = f;
            _img.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartVisible()
    {
        StartCoroutine("VisibleImage");
    }
}
