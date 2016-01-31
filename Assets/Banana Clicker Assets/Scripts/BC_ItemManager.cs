using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class BC_ItemManager : MonoBehaviour {


	//These two Text items allow you to drag in a text object and tell the Item Manager where you would like to display the Name and the Count.
	public UnityEngine.UI.Text itemInfo;
	public UnityEngine.UI.Text itemCount;

	//This imports the Click.cs file (Where our click behaviour is) and allows you to call click.bananas and change the value.
	public BC_Click click;

	//Making the cost/tickvalue/count/itemName public allows it be set at the "Inspector level" which means less coding for you.
	public float cost;
	public float tickValue;
	public int count;
	public string itemName;
    public bool IsUnlocked = false;

	//this a private float which keeps track of the starting cost of the item, it is also set by the "Inspector" 
	//However this is private as it is only used when determining the cost of the next upgrade.
	public float baseCost;
    public float costIncrease = 1.15f;


	//This sets 2 colors are public to allow you to change them within the "Inspector" but can still reference it in code.
	//The 2 colors are used for "Not affordbale" and "Affordable" So the items change color when you can afford them.
	public Color standard;
	public Color affordable;


	//This is the slider which is what is used to make the "progress bar" look.
	private Slider _slider;


    public void SetSlider(float valueToStore)
    {
        _slider.value = (float)valueToStore;
    }



	//This function runs as soon as the application begins.
	void Start(){

		//this sets the private var baseCost to be equal to the cost, as the basecost is private and not changeable by the inspector.
		baseCost = cost;

		//this tells the slider where the slider is using a get component in children.
		_slider = GetComponentInChildren<Slider> ();

		//this gets the values we stored earlier for count and cost
		/*count = PlayerPrefs.GetInt(name);
		cost = PlayerPrefs.GetFloat(name + "c");

        if (PlayerPrefs.GetInt(name + "IsUnlocked") == 1)
        {
            IsUnlocked = true;
        }
        else
        {
            IsUnlocked = false;
        }*/

		if (cost == 0) {
			cost = baseCost;
		}

	}



    //This function runs every frame (really really often more than once a second)
    void Update()
    {

        if (click.bananas > (cost / 2) || IsUnlocked == true)
        {

            //this sets the itemName to be itemname and show the cost of the item and the tickvalue and the count. It is done like this as then you do not need to hard code
            // the value for each item or upgrade, this will display whatever details you place into the inspector.

            /*
            itemInfo.text = itemName + "\nCost: " + BC_currencyConverter.Instance.GetCurrencyIntoString(cost, false, false) + "\nBananas: " + tickValue + "/s";
            */

            itemInfo.text = itemName + "\nCost: " + BC_currencyConverter.Instance.GetCurrencyIntoString(cost, false, false);

            itemCount.text = count + " ";
          
            //this converts the double result to a float so that the slider is happy as it only accepts floats as input.
            _slider.value = click.bananas / cost * 100;

            //If the slider value is greater than or equal to 100% then the item is affordale and this sets the color.
            if (_slider.value >= 100)
            {
                GetComponent<Image>().color = affordable;
            }
            else
            {
                GetComponent<Image>().color = standard;
            }

            IsUnlocked = true;


            

        }
        else
        {
            itemInfo.text = "???";
        }
    }



    //This is a public function that takes the cost that was set in the inspector and checks whether or not you can afford it.
    public void PurchasedItem(){
		if (click.bananas >= cost) {
			click.bananas -= cost;
			count += 1;

			//this saves the count and cost to playerprefs incase the user closes the app.
			PlayerPrefs.SetInt(name, count);
			PlayerPrefs.SetFloat(name + "c", (float)cost);

            PlayerPrefs.SetInt(name + "IsUnlocked", 1);
			//This is the same math that cookie clicker uses to determine the cost of the next upgrade.
			cost = Mathf.Round((float)baseCost * Mathf.Pow(this.costIncrease, count));

					
		}
	}



}
