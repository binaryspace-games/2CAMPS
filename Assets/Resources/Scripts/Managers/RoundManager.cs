using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class RoundManager : MonoBehaviour {

    public enum Tower { lightTroop, mediumTroop, heavyTroop, lightTank, mediumTank, heavyTank, lightHumvee, mediumHumvee, heavyHumvee, lightJeep, armoredJeep, lightTruck, heavyTruck, lightCargoPlane, heavyCargoPlane }        //Different Types Of Towers
    public GameObject lightTroop, mediumTroop, heavyTroop, lightTank, mediumTank, heavyTank, lightHumvee, mediumHumvee, heavyHumvee, lightJeep, armoredJeep, lightTruck, heavyTruck, lightCargoPlane, heavyCargoPlane;          //All Their Prefab

    string jsonString;
    JsonData roundData;

    private bool endOfRounds = false;

    private int currentRound = 1;
    private int roundsLeft;

    private int currentWave = 1;
    private int wavesLeft;

    private int troopsLeft = 1;
    private bool waveStart = true;
    private bool waveWait = false;
    private float waveDelayTime = 0;
    private float nextTime = 0;
    private float nextWaveDelay = 0;

    void Start() {
        jsonString = File.ReadAllText(Application.dataPath + "/StreamingAssets/RoundData/RoundData.json");
        roundData = JsonMapper.ToObject(jsonString);

        roundsLeft = roundData.Count;
        wavesLeft = roundData["Round1"].Count;
    }

    void Update()
    {
        //troopsLeft = (int)roundData["Round" + currentRound]["Wave" + currentWave]["numberOfTroops"];

        if (roundData.Count > 0)                                                                                                          //IF WE HAVE MORE ROUNDS
        {
            if (roundData["Round" + currentRound].Count + 1 /* Rounds.Waves */ > currentWave)                                             //IF WE HAVE MORE WAVES
            {
                if (troopsLeft /* Rounds.Waves.AmmountOfTroops*/ > 0)                                                                     //IF WE HAVE MORE TROOPS
                {
                    //Debug.Log("There are troops, wave number " + currentWave + " round " + currentRound );
                    if (waveStart)                                                                                                        //If we just started this wave
                    {
                        troopsLeft = (int)roundData["Round" + currentRound]["Wave" + currentWave]["numberOfTroops"];

                        double waveDelay = (double)roundData["Round" + currentRound]["Wave" + currentWave]["waveDelay"];
                        waveDelayTime = Time.time + (float)waveDelay;
                        waveWait = true;
                        waveStart = false;

                        //Debug.Log("Wave Start");
                    }
                    else if (waveWait == true && Time.time < waveDelayTime)                                                               //If we are delaying from start
                    {
                        //Debug.Log("Waiting Delay");
                        return;
                    }
                    else if (nextTime < Time.time)                                                                                        //IF THE DELAY IS WAITED
                    {
                        GameObject waveTroop = getTroop((string)roundData["Round" + currentRound]["Wave" + currentWave]["waveTroop"]);
                        int numberOfTroops = (int)roundData["Round" + currentRound]["Wave" + currentWave]["numberOfTroops"];
                        double troopSpawnDelay = (double)roundData["Round" + currentRound]["Wave" + currentWave]["troopSpawnDelay"];

                        Instantiate(waveTroop, GameObject.Find("mapSpawn").transform.position, GameObject.Find("mapSpawn").transform.rotation);
                        troopsLeft--;
                        //Debug.Log("Spawning troop number: " + (numberOfTroops - troopsLeft - 1) + ", With Time = " + Time.time);
                        if (troopsLeft != 0)
                        {
                            nextTime = Time.time + (float)troopSpawnDelay;
                        }
                    }
                    else
                        return;
                }
                else                //IF THERE NO MORE TROOPS TO SPAWN
                {
                    //Debug.Log("NEW WAVE");
                    if (roundData["Round" + currentRound].Count == currentWave)
                    {
                        //Debug.Log("If waves = currentWave --> currentRound++");
                        currentRound++;
                        currentWave = 1;
                        troopsLeft = 1;
                        waveStart = true;
                        return;
                    }
                    else {
                        //Debug.Log("If waves > currentWave || waves < currentWaves --> currentWave++");
                        currentWave++;
                        troopsLeft = 1;
                        waveStart = true;
                        return;
                    }
                }
            }
            else                    //IF THERE ARE NO MORE WAVES 
            {
                //Debug.Log("NEW ROUND");
                currentRound++;
                roundsLeft--;
                currentWave = 1;
            }
        }
        else
        {
            Debug.Log("WIN CONDITION");
        }
    }

    GameObject getTroop(string troop)
    {
        if (troop == "lightTroop") { return lightTroop; }
        else if (troop == "mediumTroop") { return mediumTroop; }
        else if (troop == "heavyTroop") { return heavyTroop; }
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

 