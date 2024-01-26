using UnityEngine;


public class CameraRotateAround : MonoBehaviour
{
    [SerializeField] private Transform _pointer;
    [SerializeField] private float _heightCameraInside;
    public Transform target;
    public Vector3 offset;
    public float sensitivity = 3; // чувствительность мышки
    public float limit = 90; // ограничение вращения по Y
    public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
    public float zoomMax = 10; // макс. увеличение
    public float zoomMin = 3; // мин. увеличение
    private static float X, Y, mouseX, mouseY;
    private bool _step;
    private Vector3 _last;
    private Vector3 _new;
    
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

    private void RotateCameraInside()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            _pointer.position = hit.point;
        }
        
        if (Input.GetMouseButton(0))
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

        if (Input.GetMouseButtonDown(0))
        {
            _last = _pointer.position;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _new = _pointer.position;
            if (_last == _new)
            {
                _new.y = _heightCameraInside;
                transform.position = _new;
            }
        }
    }

    void Update ()
    {
        if (!TpInside.Flag)
        {
            _pointer.transform.gameObject.SetActive(false);
            RotateCameraOutside();
        }else if (TpInside.Flag)
        {
            if (!_step)
            {
                TransformCameraInside();
                _pointer.transform.gameObject.SetActive(true);
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
