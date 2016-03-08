using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIValues : MonoBehaviour {

	public float cashAmmount = 0;
	public int livesAmmount = 0;
	public int roundAmmount = 0;
	public int timeValueMinutes = 0;
	public int timeValueSeconds = 0;

	public enum Difficulty { easy, medium, hard, veteran, }
	public Difficulty difficultyMode = Difficulty.easy; 

	public Text cashVal;
	public Text roundVal;
	public Text livesVal;
	public Text timeVal;
	
	void Awake () {
		difficultyMode = stringToDifficulty(PlayerPrefs.GetString("Difficulty")); //Get The Difficulty For The Match

		cashVal = GameObject.Find("CashValue").GetComponent<Text>();
		roundVal = GameObject.Find("RoundValue").GetComponent<Text>();
		livesVal = GameObject.Find("LivesValue").GetComponent<Text>();
		timeVal = GameObject.Find("TimeValue").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		cashVal.text = "$" + Mathf.Floor(cashAmmount);
		roundVal.text = "" + roundAmmount;
		livesVal.text = "" + livesAmmount;
		timeVal.text = "" + timeValueMinutes + ":" + timeValueSeconds;
	}

	public Difficulty stringToDifficulty(string str){
		if(str == "easy"){ return Difficulty.easy; }
		if(str == "medium"){ return Difficulty.medium; }
		if(str == "hard"){ return Difficulty.hard; }
		if (str == "veteran") {
			return Difficulty.veteran;} 
		else {
			return Difficulty.easy;
		}
	}

	public void removeLives(int amount){
		livesAmmount = livesAmmount - amount;
	}
		
	public void advanceRound(){
		roundAmmount++;
	}
}
