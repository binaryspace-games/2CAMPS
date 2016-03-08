using UnityEngine;
using System.Collections;

public class TowersPlacement : MonoBehaviour {

	public bool mouseDown = false;

	public void OnMouseDown(){
		mouseDown = true;
	}
	
	public bool getClicked(){
		return mouseDown;
	}
}
