using UnityEngine;

public class ShadowWalls : MonoBehaviour
{
    [SerializeField] private GameObject[] _bottomWalls;
    [SerializeField] private GameObject[] _rightWalls;
    [SerializeField] private GameObject[] _upperWalls;
    [SerializeField] private GameObject[] _leftWalls;
    [SerializeField] private GameObject[] _allWalls;

    private Color _color;
    private bool flag;
    
    private void ShadowWallsLookAtCam()
    {
        flag = TpInside.Flag;
        
        if (CameraRotateAround.Y1 >= -69)
        {
            if (CameraRotateAround.X1 <= 57 || CameraRotateAround.X1 >= 303)
            {
                Invisible(_bottomWalls);
            }
            else
            {
                Visible(_bottomWalls);
            }
        
            if (CameraRotateAround.X1 >= 85 && CameraRotateAround.X1 <= 230)
            {
                Invisible(_upperWalls);
            }
            else
            {
                Visible(_upperWalls);
            }

            if (CameraRotateAround.X1 <= 335 && CameraRotateAround.X1 >= 204)
            {
                Invisible(_rightWalls);
            }
            else
            {
                Visible(_rightWalls);
            }

            if (CameraRotateAround.X1 <= 166 && CameraRotateAround.X1 >= 27)
            {
                Invisible(_leftWalls);
            }
            else
            {
                Visible(_leftWalls);
            }
        }
        else if (CameraRotateAround.Y1 <= -70)
        {
            Visible(_allWalls);
        }
        // Сомнительно, но окэй... 
        if(flag)
        {
            CameraRotateAround.Y1 = -90;
            Visible(_allWalls);
        }
    }

    private void Invisible(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            //Вынести GetComponent в Start
            var renderer = arr[i].gameObject.GetComponent<Renderer>();
            _color = renderer.material.color;
            if (_color.a > 0.08f)
            {
                _color.a -= 2 * Time.deltaTime;
                renderer.material.color = _color;
            }
        }
    }

    private void Visible(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            //Вынести GetComponent в Start
            var renderer = arr[i].gameObject.GetComponent<Renderer>();
            _color = renderer.material.color;
            if (_color.a != 1f)
            {
                _color.a += 2 * Time.deltaTime;
                _color.a = Mathf.Clamp(_color.a,0,1);
                renderer.material.color = _color;
            }
        }
    }

    private void Update()
    {
        ShadowWallsLookAtCam();
    }
}
