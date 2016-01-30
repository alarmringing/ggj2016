using UnityEngine;
using System.Collections;

public class BC_currencyConverter : MonoBehaviour {


	private static BC_currencyConverter instance;
	public static BC_currencyConverter Instance
    {
		get{
			return instance;
		}
	}

	//this runs as soon as the applications starts and create an instance of itself so that it may be called at any time.
	void Awake(){
		CreateInstance ();
	}

	//this checks if it's created already and if not, try again.
	void CreateInstance(){
		if (instance == null) {
			instance = this;
				}
	}

	//this bad boy takes 3 values when you call it.
	//You need to give it the value you want converted so just the total value, then the currency check if it's a per click or a per second value. 
	//so one value then a true or fales value. simple.

	public string GetCurrencyIntoString(float valueToConvert, bool currencyPerSec, bool currencyPerClick){
		string converted;
		if (valueToConvert >= 1000000000000) {

			converted = (valueToConvert / 1000000000000f).ToString ("f2") + " Tril";
		} else if (valueToConvert >= 1000000000) {
			
			converted = (valueToConvert / 1000000000f).ToString ("f2") + " Bil";
			
		} else if (valueToConvert >= 1000000) {
			
			converted = (valueToConvert / 1000000f).ToString ("f2") + " Mil";
			
		} else if (valueToConvert >= 1000) {

			converted = (valueToConvert / 1000f).ToString ("f2") + " K";

		} else {

			converted = "" + valueToConvert.ToString("f1");
		}

		if (currencyPerSec == true) {
			converted = converted + " per sec";
		}

		if (currencyPerClick == true) {
			converted = converted + " per click";	
		}

		return converted;

	}

}
