using UnityEngine;

public class ZoomOnPhone : MonoBehaviour
{
	public float RotationThreshold = 40.0f;
    public float RotationMaxThreshold = 120.0f;
    public float RotateBackThreshold = 15.0f;
    public Transform PhoneZoomedTransform;
    public Transform PhoneAtSideTransform;
    public GameObject PhoneObject;
	public float ZoomFOV = 40;
	
	public bool IsZoomed { get { return _zoomed; }}
	
	private float _normalFOV;
	private Camera _camera;
	private bool _zoomed;
    private Crosshair _crosshair;
	
	void Start()
	{
		_camera = GetComponent<Camera>();
        _crosshair = GetComponent<Crosshair>();
		_normalFOV = _camera.fieldOfView;
	}
	
	void Update()
	{
        Vector3 euler = this.transform.localRotation.eulerAngles;
        if (!_zoomed && euler.x >= this.RotationThreshold && euler.x <= this.RotationMaxThreshold)
		{
			_camera.fieldOfView = this.ZoomFOV;
			_zoomed = true;
            this.PhoneObject.transform.position = this.PhoneZoomedTransform.position;
            this.PhoneObject.transform.rotation = this.PhoneZoomedTransform.rotation;
            _crosshair.Enable(false);
        }
		else if (_zoomed && euler.x <= this.RotateBackThreshold)
		{
			_camera.fieldOfView = _normalFOV;
			_zoomed = false;
            this.PhoneObject.transform.position = this.PhoneAtSideTransform.position;
            this.PhoneObject.transform.rotation = this.PhoneAtSideTransform.rotation;
            _crosshair.Enable(true);
        }
	}
}
