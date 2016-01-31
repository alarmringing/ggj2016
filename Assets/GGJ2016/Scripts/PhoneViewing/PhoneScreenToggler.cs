using UnityEngine;
using UnityEngine.UI;

public class PhoneScreenToggler : MonoBehaviour
{
    public GameObject MessagesScreen;
    public GameObject GameScreen;
    public GameObject ToggleText;
    public VirtualFriends FriendsManager;
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
                _toggleText.text = this.GameText;
                this.MessagesScreen.SetActive(true);
                this.GameScreen.SetActive(false);
                this.FriendsManager.MenubarButton();
                break;
            case ScreenState.Game:
                _toggleText.text = this.MessagesText;
                this.MessagesScreen.SetActive(false);
                this.GameScreen.SetActive(true);
                break;
        }
    }
}
