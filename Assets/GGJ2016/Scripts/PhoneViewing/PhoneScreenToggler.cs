using UnityEngine;
using UnityEngine.UI;

public class PhoneScreenToggler : MonoBehaviour
{
    public GameObject MessagesScreen;
    public GameObject GameScreen;
    public GameObject ToggleText;
    public VirtualFriends FriendsManager;
    public RectTransform MessagesTab;
    public RectTransform GameTab;
    public string MessagesText;
    public string GameText;
    public ScreenState DefaultState;

    [System.Serializable]
    public enum ScreenState
    {
        Messages,
        Game
    }

    void Awake()
    {
        if (this.ToggleText != null)
            _toggleText = this.ToggleText.GetComponent<Text>();
        _screenState = this.DefaultState;
    }

    void Start()
    {
        activateScreen();
    }

    public void ToggleActiveScreen()
    {
        _screenState = (_screenState == ScreenState.Messages) ? ScreenState.Game : ScreenState.Messages;
        activateScreen();
    }

    /**
     * Private
     */
    private ScreenState _screenState;
    private Text _toggleText;

    private void activateScreen()
    {
        switch (_screenState)
        {
            default:
            case ScreenState.Messages:
                if (_toggleText != null)
                    _toggleText.text = this.GameText;
                if (this.MessagesTab != null && this.GameTab != null)
                {
                    this.MessagesTab.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 102);
                    this.GameTab.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 78);
                }
                this.MessagesScreen.SetActive(true);
                this.GameScreen.SetActive(false);
	            this.FriendsManager.LoadInbox();
                break;
            case ScreenState.Game:
                if (_toggleText != null)
                    _toggleText.text = this.MessagesText;
                if (this.MessagesTab != null && this.GameTab != null)
                {
                    this.MessagesTab.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 78);
                    this.GameTab.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 102);
                }
                this.MessagesScreen.SetActive(false);
                this.GameScreen.SetActive(true);
                break;
        }
    }
}
