using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class Placement : MonoBehaviour {

    string jsonString;
    JsonData towerData;

    public GameObject rifleman, sniper, shotgun, heavy, builder, specialist, missile, rpg, ptboat, sub, battleship, farm, mine, airstrike, barrier, turret;

    public bool currentyPlacing = false;             //Are We Placing A Tower
	GameObject currentTower;                         //The Object We Are Placing
    public string currentTowerName;
	public bool invalidPos = false;
	public bool clickedOn = false;

    void Start() {
        jsonString = File.ReadAllText(Application.dataPath + "/StreamingAssets/TowerData/TowerData.json");
        towerData = JsonMapper.ToObject(jsonString);
    }

    public void Update () {                          //Update, Called Once A Video Frame
        if (currentTower != null) {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentTower.transform.position, getTowerRadius(currentTowerName), 1 << LayerMask.NameToLayer("TowerColliders"));
            foreach(Collider2D coll in hitColliders) {
                Debug.Log("hitLength = " + hitColliders.Length + " and we hit in 1: " + hitColliders[0].name + " and we hit in 2: ");
            }

            if (hitColliders.Length == 1) {
                if (hitColliders[0].tag == getTowerGround(currentTowerName)) {
                    invalidPos = false;
                }
                else
                    invalidPos = true;
            }
            else
                invalidPos = true;

            if (currentyPlacing == true) {                 //Are We Placing Something
                currentTower.transform.position = getMousePos();

                if (Input.GetMouseButtonUp(0)) {          //If We Stop Dragging The Tower
                    if (invalidPos == true) {             //If It Can't Be Placed, Let The Player Drag It Again
                        Debug.Log("CANT PLACE");
                    }
                    else {
                        currentTower = null;                        
                        currentyPlacing = false;    //We Place The Tower And Finish Here
                    }
                }
            }
        }
	}

	public void placeTower(string towerType){                     //Make A Tower Of The Kind The Player Bought
		Debug.Log("placing tower");
		GameObject tower = Instantiate(returnTower(towerType), getMousePos(), Quaternion.identity) as GameObject; 
		currentTower = tower;
        currentTowerName = towerType;
		currentyPlacing = true;
        Debug.Log("towerData.Count == " + towerData.Count);
	}

	public GameObject returnTower(string name) { 
        if ( name == "rifleman") { return rifleman; }
        else if (name == "sniper") { return sniper; }
        else if (name == "shotgun") { return shotgun; }
        else if (name == "heavy") { return heavy; }
        else if (name == "builder") { return builder; }
        else if (name == "specialist") { return specialist; }
        else if (name == "missile") { return missile; }
        else if (name == "rpg") { return rpg; }
        else if (name == "ptboat") { return ptboat; }
        else if (name == "sub") { return sub; }
        else if (name == "battleship") { return battleship; }
        else if (name == "farm") { return farm; }
        else if (name == "mine") { return mine; }
        else if (name == "airstrike") { return sub; }
        else if (name == "barrier") { return battleship; }
        else if (name == "turret") { return farm; }

        else
        return null;
	}

    public Vector3 getMousePos(){
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		return pos;
	}

	public float getTowerRadius(string name) {
        for (int i = 0; i < towerData.Count; i++) {
            Debug.Log("getting tower radius for Tower" + i);
            if ((string)towerData["Tower" + (i+1)]["name"] == name) {
                Debug.Log("Returning Tower" + (i + 1));
                return (int)towerData["Tower" + (i + 1) ]["range"];
            }
        }
        return 0;
    }

    public string getTowerGround(string name)
    {
        for (int i = 0; i > towerData.Count; i++)
        {
            if ((string)towerData["Tower" + i]["name"] == name)
            {
                return (string)towerData["Tower" + i]["placementArea"];
            }
        }
        return "ground";
    }
}


















