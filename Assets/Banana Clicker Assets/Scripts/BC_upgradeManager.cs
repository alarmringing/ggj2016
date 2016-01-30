using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BC_upgradeManager : MonoBehaviour {


	//These two Text items allow you to drag in a text object and tell the Upgrade Manager where you would like to display the Name and the Count.
	public string itemName;

	public UnityEngine.UI.Text itemInfo;

    public bool IsUnlocked = false;   

    public GameObject Myself;


	//This allows you to import the Click.cs File and access it's functions.
	public BC_Click click;


	//This allows you to change the cost, count and clickPower in the Inspector.
	public float cost;
	public int count = 0;
	public int clickPower;


	//private variable used only for the math in increasing the cost of each upgrade after you buy one.
	public float baseCost;


	//allows you to reference the 2 colors in the in the inspector and set the colors that they upgrades become when affordbale.
	public Color standard;
	public Color affordable;


	//this is the slider which determines the value of the progress style bar.
	private Slider _slider;


    public void SetSlider(double valueToStore)
    {
        _slider.value = (float)valueToStore;
    }



    //this function runs as soon as the application starts.

    void Start(){

		//sets basecost to cost as basecost is private and not accesible from the inspector and cost is.
		baseCost = cost;
		_slider = GetComponentInChildren<Slider> ();
		count = PlayerPrefs.GetInt(name);
		cost = PlayerPrefs.GetFloat(name + "c");
		if (cost == 0) {
			cost = baseCost;
		}

        if (PlayerPrefs.GetInt(name + "IsUnlocked") == 1)
        {
            IsUnlocked = true;
        }
        else
        {
            IsUnlocked = false;
        }


    }

	//this functions runs alot like really really fast, so it's useful for displaying information that changes a lot.
	void Update(){

        if (click.bananas > (cost / 2) || IsUnlocked == true)
        {

            //this sets the item name of the item to whatever is written in the inspector plus it adds on the cost and the clickpower from the inspector.
            itemInfo.text = itemName + " " + count + "\nCost: " + BC_currencyConverter.Instance.GetCurrencyIntoString(cost, false, false) + "\nPower: +" + clickPower;
            _slider.value = (float)(click.bananas / cost * 100);

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


	//same as the itemManager one, checks if you can afford the upgrade then takes the cost from your banana count and increase the clickpower based on the click power of the upgrade.
	public void PurchasedUpgrade()
	{
		if (click.bananas >= cost) 
		{
			click.bananas -= cost;
			count += 1;

			//this saves the count under each items own name.
			PlayerPrefs.SetInt(name, count);
            PlayerPrefs.SetInt(name + "IsUnlocked", 1);

            click.bananasPerClick += clickPower;
			cost = Mathf.Round(baseCost * Mathf.Pow (1.15f, count));
		}
	}

}
