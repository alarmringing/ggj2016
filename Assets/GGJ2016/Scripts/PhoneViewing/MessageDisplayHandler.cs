using UnityEngine;
using UnityEngine.UI;

public class MessageDisplayHandler : MonoBehaviour
{
    public VirtualFriends.VirtualMessage Message;
    public Text ReplyText;
    public BC_Click Click;
    public MessageScreenStateToggler ScreenStateToggler;
    public SuccessfulReplyCallback ReplyCallback;
    public LimitedDuration NotEnoughHandler;

    public delegate void SuccessfulReplyCallback(VirtualFriends.VirtualMessage message);

    private AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void ConfigureForMessage(VirtualFriends.VirtualMessage message)
    {
        this.Message = message;
        if (message.requestForHelp)
        {
            this.ReplyText.text = "Spend " + message.powerNeededToHelp;
        }
        else
        {
            this.ReplyText.text = "OK";
        }
    }

    public void AttemptReply()
    {
        if (this.Message.requestForHelp)
        {
            if (Time.time >= this.Message.timeReceived + this.Message.timeLimitForHelp)
            {
                this.ScreenStateToggler.ViewInbox();
                _audio.Play();
            }
            else if (this.Click.bananas >= this.Message.powerNeededToHelp)
            {
                this.Click.bananas -= this.Message.powerNeededToHelp;
                this.ReplyCallback(this.Message);
                this.ScreenStateToggler.ViewInbox();
            }
            else
            {
                _audio.Play();
                this.NotEnoughHandler.ActivateText();
            }
        }
        else
        {
            this.ReplyCallback(this.Message);
            this.ScreenStateToggler.ViewInbox();
        }
    }
}
