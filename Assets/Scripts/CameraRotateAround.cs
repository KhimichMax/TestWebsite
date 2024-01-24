using UnityEngine;


public class CameraRotateAround : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float sensitivity = 3; // чувствительность мышки
    public float limit = 90; // ограничение вращения по Y
    public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
    public float zoomMax = 10; // макс. увеличение
    public float zoomMin = 3; // мин. увеличение
    private static float X, Y;

    void Start () 
    {
        limit = Mathf.Abs(limit);
        if(limit > 90) limit = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax)/2);
        var rotation = Quaternion.Euler(new Vector3(90, -90,
            transform.localEulerAngles.z));
        transform.position = rotation
            * offset + target.position;
        X = rotation.eulerAngles.x;
        Y = -90;
    }

    void Update ()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
        else if(Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
        offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

        if (Input.GetMouseButton(0))
        {
            X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            Y += Input.GetAxis("Mouse Y") * sensitivity;
            Y = Mathf.Clamp (Y, -limit, 0);
            transform.localEulerAngles = new Vector3(-Y, X, 0);
        }
        transform.position = transform.localRotation * offset + target.position;
    }

    public static float X1
    {
        get => X;
    }

    public static float Y1
    {
        get => Y;
    }
}
