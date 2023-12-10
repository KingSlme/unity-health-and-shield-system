using UnityEngine;

public class LookAtCamera : MonoBehaviour
{   
    [SerializeField] private bool _invert;
    private Transform _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        LookAt();
    }

    private void OnEnable()
    {
        LookAt();
    }

    private void LookAt()
    {
        if (_invert)
        {
            Vector3 dir = (transform.position - _mainCamera.position).normalized;
            transform.LookAt(transform.position + dir);
        } 
        else
        {
            transform.LookAt(_mainCamera.position);
        }
    }
}
