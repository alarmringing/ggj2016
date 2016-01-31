using UnityEngine;
using System.Collections;
using System;

public class BC_bananaPerSec : MonoBehaviour {

	DateTime currentDate;
	DateTime oldDate;
	int minutes;
	int seconds;
	int hours;
	int days;
	float totalSeconds;
	float revenue;

    public bool canISave = false;



	//allows you to refered the Bananas per second display in the inspector.
	public UnityEngine.UI.Text BpsDisplay;
	public UnityEngine.UI.Text TimePassed;
	public GameObject revenueHolder;
	public GameObject revenueButton;
	public GameObject revenuePanel;

	//reference the Click.cs file to access its functions.
	public BC_Click click;


	//reference the upgradeManager.cs file to access its functions.
	public BC_upgradeManager upgrademanager;

	//creates an array to hold the items that you own, which helps as we need to calculate how many bananas per second we are calculating.
	public BC_ItemManager[] items;

	//creates an array to hold the upgrades that you own, which helps as we need to calculate how many bananas per click we have.
	public BC_upgradeManager[] upgrades;


	//this starts as soon as the application starts.
	void Start(){
       

		/*if (PlayerPrefs.GetFloat ("BananaCount") > 0) {
			loadGame ();

		}*/



		//this starts a coroutine autotick (below) which will run continuously.
		StartCoroutine (AutoTick ());
        

    }

	//this updates every frame. and updates the amount of Bananas per second by calling the function GetBananaspersec and then sending it into the currency convertor.
	//see the currency converter script for more info on that,and see below for the get bananas per sec function.
	void Update(){

        if (!canISave)
        {
            CheckIfICanSave();
        }

		BpsDisplay.text = BC_currencyConverter.Instance.GetCurrencyIntoString (GetBananasPerSec (), true, false);


	}

    //this magic function just adds a delay to the game so it does not try to save in the first 10 seconds of the game running, by doing this we can avoid having to hard code any fixes.
    //due to OnApplicationPause running twice.
    void CheckIfICanSave()
    {
        float timer = 0f;
        

        if (timer < 10)
        {
            timer += Time.deltaTime;
            if (timer >= 10)
            {
                canISave = true;
            }
            
        }
    }


    //this function goes through the array of items (You need to set in itemmanager) and multiplies the amount you have of each times each ones tick value and returns a "Tickvalue"
    public float GetBananasPerSec(){

		float tick = 0.0f;
		foreach (BC_ItemManager item in items) {

			tick += item.count * item.tickValue;
					
		}
		return tick; 
	}

	//This adds the amount of bananas per second to your total, but because we have the total updating every 10 milliseconds instead of every second we need to divide the total by 10.
	public void AutoBananasPerSec(){

		click.bananas += GetBananasPerSec () / 10;

	}


	//this runs constantly and we make it wait until the time below is reached, in this case we have it set to run every 0.10 seconds (10ms) to update the total counter really fast.
	IEnumerator AutoTick(){
		while (true) {
			AutoBananasPerSec();
			yield return new WaitForSeconds(0.10f);
				
		}

	}

	//This saves the game and is run by the function Onapplicationquit()

	public void saveGame(){

		//This is all that is needed to save a value to PlayerPrefs
		//This line saves the value "bananas" from the "click" file to playerprefs under the name "BananaCount"
		//Notice it says setFloat there is also setInt and set String to be used accordingly.
		PlayerPrefs.SetFloat("BananaCount", click.bananas);


        //This saves the time that the app was closed and converts it to binary and then into a string!
        //This is required as you need to be able to read from it again later.
        PlayerPrefs.SetString("closeTime", System.DateTime.Now.ToBinary().ToString());

		//This just prints to console the time that was logged, I left it in for testing.
		//You are free to remove it once you understand how it work.
		print("Saving this date to prefs: " + System.DateTime.Now);

		//This creates a float to keep track of how many bananas per second are being produed
		float tickPerSec = 0.0f;

		float bPerClick = 1.0f;

        

		//This steps through each item in the Itemmanager array we created up above.
		//Notice the array we created above appears in the Inspector so you need to drag in each item you want counted
		//this script then looks for the count in each item and adds the totals up to be saved in playerprefs.
		foreach (BC_ItemManager item in items) {

			tickPerSec += item.count * item.tickValue;

			
		}

		//this stores the value we just got in playerprefs under the name tickPerSec
        
		PlayerPrefs.SetFloat("tickPerSec", tickPerSec);

		//this just prints what we did to the console so i can visually see how much was saved.
		print ("TickPerSec Stored in prefs = " + tickPerSec);

		foreach (BC_upgradeManager upgrade in upgrades) {
			bPerClick += upgrade.count * upgrade.clickPower;
			
			
		}

		//this stores the value we just got in playerprefs under the name tickPerSec
		PlayerPrefs.SetFloat("bPerClick", bPerClick);
		
		//this just prints what we did to the console so i can visually see how much was saved.
		print ("bPerClick Stored in prefs = " + bPerClick);


				
		
		
	}


	//This method clears the playerprefs and resets all costs and counters.
	public void clearGame()
	{

		//this function easily deletes everything stored in playerprefs.
		PlayerPrefs.DeleteAll ();

		//this sets the banana value back to 0, notice it has to call click first so it can access it.
		//The reason we call click first is because we made a public click up above and then dragged in the click script so we can reference that script easier.
		click.bananas = 0;
		click.bananasPerClick = 1;

		//same as above this steps through each item in the array of items.
		//notice this only works for items that are dragged into the inspector file under itemmanager.
		foreach (BC_ItemManager item in items) {
			item.count = 0;
			item.cost = item.baseCost;
            item.IsUnlocked = false;
            
            item.itemCount.text = item.count.ToString();
            item.SetSlider(0);

        }

		//same as above this steps through each item in the array of items.
		//notice this only works for upgrades that are dragged into the inspector file under upgrademanager.
		foreach (BC_upgradeManager upgrade in upgrades) {
			upgrade.count = 0;
			upgrade.cost = upgrade.baseCost;
			upgrade.IsUnlocked = false;
            upgrade.SetSlider(0);
          
        }




	}



	public void loadGame(){


		//On load sets banana value to the value stored in Playerprefs
		if (PlayerPrefs.GetFloat ("BananaCount") > 0) 
		{
			click.bananas = PlayerPrefs.GetFloat ("BananaCount");


		} else {

			click.bananas = 0;
		}


		//this grabs the current time and stores it
		currentDate = System.DateTime.Now;

		//this is required to read what we saved earlier
		//we get the string we saved earlier from playerprefs and converts to a long Int
		long temp = Convert.ToInt64(PlayerPrefs.GetString("closeTime"));

		//This converts the long int into a date which we can use!
		DateTime oldDate = DateTime.FromBinary(temp);

		//now that we have a date we can access the days, hours, seconds, minutes
		//lets store them so we can use them where we want.
		minutes = currentDate.Minute - oldDate.Minute;
		seconds = currentDate.Second - oldDate.Second;
		hours = currentDate.Hour - oldDate.Hour;
		days = currentDate.Day - oldDate.Day;

		//1 * 24 = 24hrs * 60 = 1440 minutes * 60 = 86400 seconds

		//this is slowly adding up each individual amount
		//mathf.abs just means the absolute number incase you end up with a negative number.
		totalSeconds = (Mathf.Abs(days) * 24);
		totalSeconds += (Mathf.Abs(hours) * 60);
		totalSeconds += (Mathf.Abs(minutes) * 60);
		totalSeconds += Mathf.Abs(seconds);

		//this just prints to console so i can see whats going on.
		print ("TickPerSec Retrieved = " + PlayerPrefs.GetFloat ("tickPerSec"));

		//this just prints to console so i can see whats going on.
		print ("bPerClick Retrieved = " + PlayerPrefs.GetFloat ("bPerClick"));

		if ((PlayerPrefs.GetFloat ("bPerClick") > 1)){
			click.bananasPerClick = (PlayerPrefs.GetFloat ("bPerClick"));
		} else {
			click.bananasPerClick = 1;
		}



		//this stores the tickspersec we stored in playerrefs and stores it in revenue.
		revenue = (PlayerPrefs.GetFloat ("tickPerSec") * totalSeconds);

		if (revenue >= 0) {

            if (revenueHolder != null)
			    revenueHolder.SetActive (true);

            if (revenueButton != null)
			    revenueButton.SetActive (true);

            if (revenuePanel != null)
			    revenuePanel.SetActive (true);

            if (TimePassed != null)
			    TimePassed.text = "Time since you last played: " + "\n" + Mathf.Abs (days) + " days, " + Mathf.Abs (hours) + " hours, " + Mathf.Abs (minutes) + " minutes, " + Mathf.Abs (seconds) + " seconds, " + "\n" + "Total Seconds Passed: " + totalSeconds + "\n" + "You made: " + BC_currencyConverter.Instance.GetCurrencyIntoString (revenue, false, false) + " Bananas!";
			
			
		}





		//This is the white text that is displayed below your banana count when you open the app up.

		//this is set by dragging a text element into the inspector for "TimePassed" and this updates that.


						
	}


	//this is a function for the button to hide the message that pops up when you open the app.
	public void ClearRevenue()
	{

		//this adds the revenue we stored earlier to the running total
		click.bananas += revenue;
		//this clears revenue
		revenue = 0;
		//this hides the text and button
		revenueHolder.SetActive (false);
		revenueButton.SetActive (false);
		revenuePanel.SetActive (false);

	}


	//this runs when you quit the app.
	//On ios this is only called when the user "closes the app" not just minimising it.

	//you can call on application pause for that or just set the app to never pause and to exit on pause.
	//this should be under build options for IOS
	void OnApplicationQuit() {


        Debug.Log("SaveGame OnApplicationQuit");
        saveGame();
		
		
		
	}

    //this function runs when the app is minimized on an android.
    //the debug are here so u know when they run.
    void OnApplicationPause()
    {
        Debug.Log("SaveGame OnApplicationPause");

        Application.Quit();
        //saveGame();
    }


}
