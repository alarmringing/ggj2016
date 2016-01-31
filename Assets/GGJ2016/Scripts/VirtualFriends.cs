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
	
	/*public enum Mode {
		Home,
		Messages,
		Message,
		Game
	}
	
	Mode mode = Mode.Home;*/
	
	
	
	[System.Serializable]
	public class VirtualMessage
	{
		public float timeReceived = 10f;
		public bool received = false;
		public bool read = false;
		public bool helped = false;
		public string VirtualFriendName = "Tom";
		public string messageSubject = "Hi";
		public string messageText = "Hi there.";
		public bool requestForHelp = false;
		public int powerNeededToHelp = 100;
		/// <summary>
		/// The time when the message will be received.
		/// </summary>
		public float timeLimitForHelp = 30f;
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
		float soonestMessageTime = 0f;
		VirtualMessage soonestMessage = null;
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
	
	public AudioClip messageReceivedClip;
	
	public void MessageNotification()
	{
		gameObject.GetComponent<AudioSource>().PlayOneShot(messageReceivedClip);
		unreadMessages++;
		unreadMessagesText.text = unreadMessages+"";
		//if (mode == Mode.Messages)
        if (this.buttonGridLayoutGroup.IsActive())
		{
			MenubarButton();
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
	
	/*public GameObject messagesUICanvas;
	public GameObject messageUICanvas;
	public GameObject gameUICanvas;*/
	
	public GridLayoutGroup buttonGridLayoutGroup;
	public GameObject messageButtonTemplate;
    public MessageScreenStateToggler messageToggler;
	
	public void MenubarButton()
	{
		//mode = Mode.Messages;
		if(buttonGridLayoutGroup!=null)
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
		/*messagesUICanvas.SetActive(true);
		gameUICanvas.SetActive(false);
		messageUICanvas.SetActive(false);*/
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
			//mode = Mode.Message;
		
			displayedMessage = inbox[messageIndex];
		
			fromText.text = displayedMessage.VirtualFriendName;
			subjectText.text = displayedMessage.messageSubject;
			bodyText.text = displayedMessage.messageText;
			//portraitImage.texture = displayedMessage. 
			// Need to have a portrait getter for a given name
			
			if (!displayedMessage.read)
			{
				displayedMessage.read = true;
				unreadMessages--;
				UnityEngine.Debug.Log("unreadMessages--");
			}

            messageToggler.FocusOnMessage();

			/*messagesUICanvas.SetActive(false);
			gameUICanvas.SetActive(false);
			messageUICanvas.SetActive(true);*/
		}
		else
		{
			Debug.Log("inbox["+messageIndex+"] !>= messageIndex+1");
		}
	}

	/*public void GameButton()
	{
		mode = Mode.Game;
		messagesUICanvas.SetActive(false);
		gameUICanvas.SetActive(true);
		messageUICanvas.SetActive(false);
	}*/
}
