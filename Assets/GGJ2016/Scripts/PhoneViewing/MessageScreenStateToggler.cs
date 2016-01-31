using UnityEngine;
using UnityEngine.UI;

public class MessageScreenStateToggler : MonoBehaviour
{
    public GameObject InboxPanel;
    public GameObject MessagePanel;
    public VirtualFriends FriendsManager;
    public State DefaultState;

    [System.Serializable]
    public enum State
    {
        Inbox,
        Message
    }

    void Awake()
    {
        _state = this.DefaultState;
    }

    void Start()
    {
        activateScreen();
    }

    public void ViewInbox()
    {
        _state = State.Inbox;
        activateScreen();
    }

    public void FocusOnMessage()
    {
        _state = State.Message;
        activateScreen();
    }

    /**
     * Private
     */
    private State _state;

    private void activateScreen()
    {
        switch (_state)
        {
            default:
            case State.Inbox:
                this.InboxPanel.SetActive(true);
                this.MessagePanel.SetActive(false);
                this.FriendsManager.MenubarButton();
                break;
            case State.Message:
                this.InboxPanel.SetActive(false);
                this.MessagePanel.SetActive(true);
                break;
        }
    }
}
