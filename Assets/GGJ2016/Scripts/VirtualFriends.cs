using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	
	[System.Serializable]
	public class VirtualMessage
	{
		public float timeReceived = 10f;
		public bool received = false;
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
		}
	}
	
	public AudioClip messageReceivedClip;
	
	public void MessageNotification()
	{
		gameObject.GetComponent<AudioSource>().PlayOneShot(messageReceivedClip);
	}
	
	

	// Use this for initialization
	void Start () {
		nextMessage=null;
		DetermineNextMessage();
	}
	
	// Update is called once per frame
	void Update () {
		ReceiveMessages();
	}
	
	GameObject messagesUICanvas;
	GameObject messageUICanvas;
	GameObject gameUICanvas;
	
	public void MenubarButton()
	{
		messagesUICanvas.SetActive(true);
		gameUICanvas.SetActive(false);
		messageUICanvas.SetActive(false);
	}
}
