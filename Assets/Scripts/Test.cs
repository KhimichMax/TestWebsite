using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);
        if (Physics.Raycast(ray))
        {
            Debug.Log(gameObject.name);
        }
    }
}
