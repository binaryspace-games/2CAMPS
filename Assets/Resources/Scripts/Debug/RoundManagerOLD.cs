using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class RoundManagerOLD : MonoBehaviour {

    public enum Tower { lightTroop, mediumTroop, heavyTroop, lightTank, mediumTank, heavyTank, lightHumvee, mediumHumvee, heavyHumvee, lightJeep, armoredJeep, lightTruck, heavyTruck, lightCargoPlane, heavyCargoPlane}        //Different Types Of Towers
    public GameObject lightTroop, mediumTroop, heavyTroop, lightTank, mediumTank, heavyTank, lightHumvee, mediumHumvee, heavyHumvee, lightJeep, armoredJeep, lightTruck, heavyTruck, lightCargoPlane, heavyCargoPlane;          //All Their Prefab

    string jsonString;
	JsonData itemData;

    private bool endOfRounds = false;

    private int currentRound = 1;
    private int roundsLeft;

    private int currentWave = 1;
    private int wavesLeft;
    
    private int troopsLeft = 5;

    private bool waveStart = true;
    private float nextTime = 0;
    private float nextWaveDelay = 0;

    void Start () {
		jsonString = File.ReadAllText(Application.dataPath + "/Resources/RoundData.json");
        itemData = JsonMapper.ToObject(jsonString);
        roundsLeft = itemData.Count;
    }

    void Update() {
        if (endOfRounds == false) {
            if (roundsLeft == 0) {                        //IF WE DONT HAVE ANY MORE ROUNDS LEFT IN FILE 
                endOfRounds = true;                       //STOP THE LOOP
                return;
            }
            else if (itemData["Round" + currentRound].Count < currentWave) {   // round is done ***HANDLER OF END OF ROUND
                roundsLeft--;
                currentRound++;
                wavesLeft = itemData["Round" + currentRound + 1].Count;    // advance round and put in next wave data
                       
                return;                                                    // round is begining after this statment is done ***HANDLER OF BEGINING OF ROUND
            }
            else {                                                         // round is continuing ***HANDELER OF CONTINUING ROUND
                if (itemData["Round" + currentRound].Count >= currentWave) //IF THERE ARE MORE WAVES - RELOAD DATA 
                {
                    int waveDelay = (int)itemData["Round" + currentRound]["Wave" + currentWave]["waveDelay"];
                    GameObject waveTroop = getTroop((string)itemData["Round" + currentRound]["Wave" + currentWave]["waveTroop"]);
                    int numberOfTroops = (int)itemData["Round" + currentRound]["Wave" + currentWave]["numberOfTroops"];
                    int troopSpawnDelay = (int)itemData["Round" + currentRound]["Wave" + currentWave]["troopSpawnDelay"];

                    if (waveStart == true) { nextWaveDelay = Time.time + waveDelay; waveStart = false; }

                    if (Time.time <= nextWaveDelay) {
                        while (troopsLeft > 0) {
                            //SPAWN TROOPS
                            if (Time.time >= nextTime) {
                                Instantiate(waveTroop, GameObject.Find("mapSpawn").transform.position, GameObject.Find("mapSpawn").transform.rotation);

                                Debug.Log("Spawning troop number: " + (numberOfTroops - troopsLeft + 1) + ", With Time = " + Time.time);

                                nextTime = nextTime + troopSpawnDelay;

                                troopsLeft--;
                            }
                            else
                            return;
                        }                                        //AFTER SPAWNED

                        if (troopsLeft < 1) {                    //IF WE HAVE NO MORE TROOPS TO SPAWN
                            wavesLeft--;                         //GO TO NEXT WAVE
                        }
                    }
                    else
                    return;
                }

                else {                                        //SOMETHING WENT WRONG xD
                    roundsLeft--;
                    currentRound++;                    
                }
            }
        }
    }

    GameObject getTroop(string troop)
    {
        if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "lightTroop") { return lightTroop; }
        else
        {
            Debug.Log("INVALID TROOP -- no troop matching: '" + troop + "', using default (lightTroop)");

            return lightTroop;
        }
    }
}

 