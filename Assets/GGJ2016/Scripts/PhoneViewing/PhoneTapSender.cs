using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhoneTapSender : MonoBehaviour
{
    public GameObject RootUI;
    public GameObject CursorObject;
    public Vector2 ViewportSize;

    private Camera _camera;
	
	void Awake()
	{
		_camera = GetComponent<Camera>();
	}
	
	public void SendTap(Vector2 relativeOrigin)
    {
        Vector2 cursorPosition = getCursorPositionFromRelativeOrigin(relativeOrigin, false);
        //Ray ray = new Ray((Vector3)cursorPosition + _camera.transform.position, _camera.transform.forward);
        //RaycastHit[] hits = Physics.RaycastAll(ray, 5.0f);

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = cursorPosition;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult result in results)
        {
            //Debug.Log("found result: " + result);
            Button button = result.gameObject.GetComponent<Button>();

            if (button != null)
            {
                ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerClickHandler);
            }
        }

        //ExecuteEvents.ExecuteHierarchy(RootUI, pointer, ExecuteEvents.pointerClickHandler);
    }

    public void UpdateCursor(Vector2 relativeOrigin)
    {
        Vector2 cursorPosition = getCursorPositionFromRelativeOrigin(relativeOrigin);
        this.CursorObject.transform.localPosition = new Vector3(cursorPosition.x, cursorPosition.y, this.CursorObject.transform.localPosition.z);
    }

    private Vector2 getCursorPositionFromRelativeOrigin(Vector2 relativeOrigin, bool offset = true)
    {
        if (offset)
        {
            return new Vector2(this.ViewportSize.x * relativeOrigin.x - this.ViewportSize.x / 2.0f,
                               this.ViewportSize.y * relativeOrigin.y - this.ViewportSize.y / 2.0f);
        }
        else
        {

            return new Vector2(this.ViewportSize.x * relativeOrigin.x,
                               this.ViewportSize.y * relativeOrigin.y);
        }
    }
}
