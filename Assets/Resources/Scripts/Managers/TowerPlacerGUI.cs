using UnityEngine;
using System.Collections;

public class TowerPlacerGUI : MonoBehaviour {


	public enum Tower { rifleman, sniper, shotgun, builder, missile, rpg, ptboat, sub, specialist, heavy, battleship, oilrig }        //Different Types Of Towers
	public Tower selectedTower = Tower.rifleman;
	public Color clickColor = new Color(1,1,1,1);
	public SpriteRenderer sr;
	
	public void OnMouseDown(){
		sr.color = clickColor; 
		//GameObject.Find("Placement").GetComponent<Placement>().placeTower(selectedTower);
	}

	public void OnMouseUp(){
		sr.color = new Color(1,1,1,1);
	}
}
