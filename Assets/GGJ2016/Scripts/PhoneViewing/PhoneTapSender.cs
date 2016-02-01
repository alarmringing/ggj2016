using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhoneTapSender : MonoBehaviour
{
    public GameObject RootUI;
    public GameObject CursorObject;
    public Vector2 ViewportSize;
	
	public void SendTap(Vector2 relativeOrigin)
    {
        Vector2 cursorPosition = getCursorPositionFromRelativeOrigin(relativeOrigin, false);
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = cursorPosition;
        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();

            if (button != null)
            {
                ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerClickHandler);
                break;
            }
            else
            {
                Image image = result.gameObject.GetComponent<Image>();
                if (image != null && image.raycastTarget)
                    break;
            }
        }
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
