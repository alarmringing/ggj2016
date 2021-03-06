﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ZoomOnPhone))]
public class PhoneTapper : MonoBehaviour
{
	public string TapSenderObjectName = "Phone View";
    public GameObject PhoneScreen;
    public Transform CameraTransform;
	
	private PhoneTapSender _phoneTapSender;
	private ZoomOnPhone _phoneZoom;
    private Collider _phoneScreenCollider;
	
	void Start()
	{
        _phoneTapSender = GameObject.Find(this.TapSenderObjectName).GetComponent<PhoneTapSender>();
		_phoneZoom = GetComponent<ZoomOnPhone>();
        _phoneScreenCollider = PhoneScreen.GetComponent<Collider>();
	}
	
	void Update()
	{
		if (_phoneZoom.IsZoomed)
		{
            Ray ray = new Ray(this.CameraTransform.position, this.CameraTransform.forward);
            RaycastHit hit;

            _phoneScreenCollider.Raycast(ray, out hit, 3.0f);
            Vector2 uvHit = hit.textureCoord;
            
            _phoneTapSender.UpdateCursor(uvHit);

            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("uv tapped: " + uvHit.x + ", " + uvHit.y);
                _phoneTapSender.SendTap(uvHit);
            }
		}
	}
}
