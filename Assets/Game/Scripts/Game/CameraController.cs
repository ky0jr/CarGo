using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform LookPosition;

    private Camera _camera;

    private void Start()
    {
        if (_camera is null)
            _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (LookPosition is null)
            return;
        
        _camera.transform.LookAt(LookPosition);
    }
}
