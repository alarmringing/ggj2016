using UnityEngine;

public class ZoomOnPhone : MonoBehaviour
{
	public float RotationThreshold = 40.0f;
	public float ZoomFOV = 40;
	
	private float _normalFOV;
	private Camera _camera;
	
	void Start()
	{
		_camera = GetComponent<Camera>();
		_normalFOV = _camera.fieldOfView;
	}
	
	void Update()
	{
		if (this.transform.localRotation.eulerAngles.x >= this.RotationThreshold)
		{
			_camera.fieldOfView = this.ZoomFOV;
		}
		else
		{
			_camera.fieldOfView = _normalFOV;
		}
	}
}
