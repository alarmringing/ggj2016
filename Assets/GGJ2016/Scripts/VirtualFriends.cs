using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VirtualFriends : MonoBehaviour {
	
	[System.Serializable]
	public class VirtualFriend
	{
		public string name = "Tom";
		/// <summary>
		/// When someone first messages you
		/// </summary>
		public bool met = false;
		/// <summary>
		/// When someone adds you as a friend.
		/// </summary>
		public bool friends = false;
		/// <summary>
		/// When someone thinks you are awesome.
		/// </summary>
		public bool impressed = true;
		/// <summary>
		/// When you let someone down.
		/// </summary>
		public bool pissed = false;
		public Texture2D portrait;
	}
	
	public List<VirtualFriend> realFriends;
	public List<VirtualFriend> gameFriends;
    public Texture2D defaultPortrait;
	
	[System.Serializable]
	public class VirtualMessage
	{
		public float timeReceived = 10f;
		public bool received = false;
		public bool read = false;
		public bool helped = false;
		public string VirtualFriendName = "Tom";
		public string messageSubject = "Hi";
        [TextArea(3, 10)]
        public string messageText = "Hi there.";
		public bool requestForHelp = false;
		public int powerNeededToHelp = 100;
		
		public string triggerTargetName = "";
		public string triggerTargetMethod = "DoActivateTrigger";
		/// <summary>
		/// The time when the message will be received.
		/// </summary>
		public float timeLimitForHelp = 30f;
        public string successResponse = "";
        public string failureResponse = "";
        public VirtualMessage parentMessage = null;
	}
	
	public List<VirtualMessage> futureMessages;
	public List<VirtualMessage> inbox;
	public List<VirtualMessage> oldMessages;
	VirtualMessage nextMessage = null;
	public float nextMessageTime = 1f;
	
	public void ReceiveMessages ()
	{
		if(nextMessage != null)
		{
			if (Time.time > nextMessageTime)
			{
				nextMessage.received = true;
				inbox.Add(nextMessage);
				futureMessages.Remove(nextMessage);
				
				MessageNotification();
				DetermineNextMessage();
			}
		}
	}
	
	public void DetermineNextMessage ()
	{
		nextMessage=null;
		//float soonestMessageTime = 0f;
		//VirtualMessage soonestMessage = null;
		if ( futureMessages!=null && futureMessages.Count>0)
		{
		    nextMessage = futureMessages[0];
			nextMessageTime = nextMessage.timeReceived;
		}
		/*
		foreach (VirtualMessage vm in futureMessages)
		{
			// If this message is sooner than the soonest previously seen
			if (soonestMessageTime > vm.timeReceived)
			{
				soonestMessageTime = vm.timeReceived;
				soonestMessage = vm;
			}
		}
		if (soonestMessage != null)
		{
			nextMessage = soonestMessage;
			nextMessageTime = soonestMessageTime;
			Debug.Log("Next message subject is " + nextMessage.messageSubject);
		}
		*/
	}

    public void InsertMessage(VirtualMessage message)
    {
        int i = 0;
        for (; i < this.futureMessages.Count; ++i)
        {
            if (futureMessages[i].timeReceived > message.timeReceived)
                break;
        }
        futureMessages.Insert(i, message);
    }
	
	public AudioClip messageReceivedClip;
	
	public void MessageNotification()
	{
		if(nextMessage.triggerTargetName!="")
		{
			GameObject go = GameObject.Find(nextMessage.triggerTargetName);
			if(go==null)
			{
				Debug.Log("nextMessage.triggerTargetName" + nextMessage.triggerTargetName + " not found.");
			}
			else
			{
				go.BroadcastMessage(nextMessage.triggerTargetMethod,SendMessageOptions.DontRequireReceiver);
			}
		}
		
		gameObject.GetComponent<AudioSource>().PlayOneShot(messageReceivedClip);
		unreadMessages++;
		unreadMessagesText.text = unreadMessages+"";

        if (nextMessage.requestForHelp)
        {
            VirtualMessage failureMessage = new VirtualMessage();
            failureMessage.timeReceived = nextMessage.timeReceived + nextMessage.timeLimitForHelp;
            failureMessage.VirtualFriendName = nextMessage.VirtualFriendName;
            failureMessage.messageSubject = "Defenses failed!";
            failureMessage.messageText = nextMessage.failureResponse;
            failureMessage.successResponse = nextMessage.successResponse;
            failureMessage.triggerTargetName = "PhoneGameManager";
            failureMessage.triggerTargetMethod = "Attack";
            failureMessage.parentMessage = nextMessage;

            InsertMessage(failureMessage);
        }

        if (nextMessage.parentMessage != null)
        {
            this.inbox.Remove(nextMessage.parentMessage);
        }
		
        if (this.buttonGridLayoutGroup.IsActive())
		{
			this.LoadInbox();
		}
	}
	
	public Text unreadMessagesText;
	
	public int unreadMessages = 0;

	// Use this for initialization
	void Start () {
		DetermineNextMessage();
	}
	
	// Update is called once per frame
	void Update () {
		ReceiveMessages();
	}
	
	public GridLayoutGroup buttonGridLayoutGroup;
	public GameObject messageButtonTemplate;
    public MessageScreenStateToggler messageToggler;
    public MessageDisplayHandler messageDisplay;
	
	public void LoadInbox()
	{
		if(buttonGridLayoutGroup != null)
		{
			Button[] buttons = buttonGridLayoutGroup.GetComponentsInChildren<Button>();
			foreach (Button b in buttons)
			{
				GameObject.Destroy(b.gameObject);
			}
			int i = 0;
			foreach (VirtualMessage vm in inbox)
			{
				GameObject newButton = GameObject.Instantiate(messageButtonTemplate);
				newButton.SetActive(true);
				newButton.transform.SetParent(buttonGridLayoutGroup.transform);
				newButton.gameObject.GetComponentInChildren<Text>().text = vm.messageSubject;
				AddButtonListenerInt(newButton.GetComponent<Button>(),i);
				i++;
			}
		}
	}
	
	/// <summary>
	/// Necessary to avoid the foreach loop only using the last value. See http://answers.unity3d.com/questions/791573/46-ui-how-to-apply-onclick-handler-for-button-gene.html
	/// </summary>
	/// <param name="b"></param>
	/// <param name="intArg"></param>
	public void AddButtonListenerInt(Button b, int intArg)
	{
		b.onClick.AddListener( () => { MessageButton(intArg); } );
	}
	
	private VirtualMessage displayedMessage;
	public int displayedMessageIndex = 0;
	
	public Text fromText;
	public Text subjectText;
	public Text bodyText;
	public RawImage portraitImage;
	
	public void MessageButton(int messageIndex)
	{
		Debug.Log("MessageButton()");
		if(inbox.Count >= messageIndex+1)
		{
			displayedMessage = inbox[messageIndex];
		
			fromText.text = displayedMessage.VirtualFriendName;
			subjectText.text = displayedMessage.messageSubject;
			bodyText.text = displayedMessage.messageText;
            VirtualFriend friend = friendForName(displayedMessage.VirtualFriendName);
            portraitImage.texture = friend != null && friend.portrait != null ? friend.portrait : this.defaultPortrait;
			
			if (!displayedMessage.read)
			{
				displayedMessage.read = true;
				unreadMessages--;
                unreadMessagesText.text = unreadMessages + "";
                UnityEngine.Debug.Log("unreadMessages--");
			}

            messageDisplay.ConfigureForMessage(displayedMessage);
            messageDisplay.ReplyCallback = this.SuccessfulMessageReply;
            messageToggler.FocusOnMessage();
		}
		else
		{
			Debug.Log("inbox["+messageIndex+"] !>= messageIndex+1");
		}
	}

    public void SuccessfulMessageReply(VirtualMessage message)
    {
        inbox.Remove(message);

        foreach (VirtualMessage futureMessage in futureMessages)
        {
            if (futureMessage.parentMessage == message)
            {
                futureMessage.messageSubject = "Success!";
                futureMessage.messageText = message.successResponse;
                break;
            }
        }
    }

    private VirtualFriend friendForName(string friendName)
    {
        foreach (VirtualFriend friend in this.gameFriends)
        {
            if (friend.name == friendName)
                return friend;
        }
        foreach (VirtualFriend friend in this.realFriends)
        {
            if (friend.name == friendName)
                return friend;
        }
        return null;
    }
}
