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
    private static float X, Y, mouseX, mouseY;
    private bool _isMoving;
    private bool _step;
    private float _speedMove = 0.5f;
    private float _timeClick = 0.05f;
    private Vector3 _targetPosition;
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

    private void RotateCameraOutside()
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

    private void TransformCameraInside()
    {
        transform.position = TpInside.PositionBtn.position;
    }

    private void SetTargetPosition()
    {
        _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(_targetPosition);
    }

    private void RotateCameraInside()
    {
        if (Input.GetMouseButton(0))
        {
            _timeClick -= Time.deltaTime;
            if (_timeClick <= 0)
            {
                SetTargetPosition();
                _timeClick = 0.05f;
            }
            else
            {
                /*float mouseX = Input.GetAxis(("Mouse X")) * (sensitivity + 600f) * Time.deltaTime;
           float mouseY = Input.GetAxis(("Mouse Y")) * (sensitivity + 600f) * Time.deltaTime;

           xRotation -= mouseY;
           xRotation = Mathf.Clamp(xRotation, -90, 90);
           transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  */ 
                mouseX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * (sensitivity - 1);
                mouseY += Input.GetAxis("Mouse Y") * (sensitivity - 1);
                mouseY = Mathf.Clamp (mouseY, -90, 90);
                transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);
            }
        }
        
    }

    void Update ()
    {
        if (!TpInside.Flag)
        {
            RotateCameraOutside();
        }else if (TpInside.Flag)
        {
            if (!_step)
            {
                TransformCameraInside();
                _step = true;
            }
            RotateCameraInside();
        }
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
