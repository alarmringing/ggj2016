using UnityEngine;

public class ZoomOnPhone : MonoBehaviour
{
	public float RotationThreshold = 40.0f;
    public float RotationMaxThreshold = 120.0f;
    public float RotateBackThreshold = 15.0f;
	public float ZoomFOV = 40;
	
	public bool IsZoomed { get { return _zoomed; }}
	
	private float _normalFOV;
	private Camera _camera;
	private bool _zoomed;
	
	void Start()
	{
		_camera = GetComponent<Camera>();
		_normalFOV = _camera.fieldOfView;
	}
	
	void Update()
	{
        Vector3 euler = this.transform.localRotation.eulerAngles;
        if (!_zoomed && euler.x >= this.RotationThreshold && euler.x <= this.RotationMaxThreshold)
		{
			_camera.fieldOfView = this.ZoomFOV;
			_zoomed = true;
		}
		else if (_zoomed && euler.x <= this.RotateBackThreshold)
		{
			_camera.fieldOfView = _normalFOV;
			_zoomed = false;
		}
	}
}
