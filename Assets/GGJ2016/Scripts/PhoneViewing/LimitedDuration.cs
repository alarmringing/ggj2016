using UnityEngine;
using UnityEngine.UI;

public class LimitedDuration : MonoBehaviour
{
    public float Duration = 2.0f;
    public bool VisibleAtStart = false;

    private string text = "";
    private float remainingDuration;
    private bool visible = false;

    void Awake()
    {
        text = GetComponent<Text>().text;
        if (!this.VisibleAtStart)
        {
            GetComponent<Text>().text = "";
        }
        else
        {
            visible = true;
            remainingDuration = this.Duration;
        }
    }

    void Update()
    {
        if (visible)
        {
            if (remainingDuration <= 0.0f)
                DeactivateText();
            else
                remainingDuration -= Time.deltaTime;
        }
    }

    public void ActivateText()
    {
        GetComponent<Text>().text = text;
        visible = true;
        remainingDuration = this.Duration;
    }

    public void DeactivateText()
    {
        GetComponent<Text>().text = "";
        visible = false;
    }
}
