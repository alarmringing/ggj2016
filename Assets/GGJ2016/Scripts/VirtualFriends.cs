using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VirtualFriends : MonoBehaviour {
	
	[System.Serializable]
	public class VirtualFriend
	{
		public string name = "Tom";
		public bool met = false;
		public bool impressed = true;
		public bool pissed = false;
		public Texture2D portrait;
	}
	
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
	public VirtualMessage nextMessage = null;
	public float nextMessageTime = 1f;
	
	public void ReceiveMessages ()
	{
		if (Time.time > nextMessageTime)
		{
			VirtualMessage vm = nextMessage;
			nextMessage.received = true;
			inbox.Add(nextMessage);
			futureMessages.Remove(nextMessage);
			MessageNotification();
			DetermineNextMessage();
		}
		
		
	}
	
	public void DetermineNextMessage ()
	{
		float soonestMessageTime = 0f;
		VirtualMessage soonestMessage = null;
		foreach (VirtualMessage vm in futureMessages)
		{
			// IF this message is sooner than the soonest previously seen
			if (soonestMessageTime > vm.timeReceived)
			{
				soonestMessageTime = vm.timeReceived;
				soonestMessage = vm;
			}
		}
		if (soonestMessage != null)
		{
			nextMessage = soonestMessage;
		}
	}
	
	public AudioClip messageReceivedClip;
	
	public void MessageNotification()
	{
		gameObject.GetComponent<AudioSource>().PlayOneShot(messageReceivedClip);
	}
	
	
	public List<VirtualFriend> realFriends;
	public List<VirtualFriend> gameFriends;
	
	// Use this for initialization
	void Start () {
		DetermineNextMessage();
	}
	
	// Update is called once per frame
	void Update () {
		ReceiveMessages();
	}
}
