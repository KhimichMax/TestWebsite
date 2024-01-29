using UnityEngine;

public class TpInside : MonoBehaviour
{
    private static bool flag;
    private static Transform _positionBtn;

    private void Awake()
    {
        _positionBtn = GetComponent<Transform>();
    }

    public void OnPressButton()
    {
        if (!flag)
        {
            flag = true;
            _positionBtn.position = gameObject.transform.position;
        }else if (flag)
        {
            flag = false;
        }
    }

    public static bool Flag
    {
        get => flag;
    }

    public static Transform PositionBtn
    {
        get => _positionBtn;
    }
}
