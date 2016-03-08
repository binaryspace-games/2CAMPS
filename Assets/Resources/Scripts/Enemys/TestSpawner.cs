using UnityEngine;
using System.Collections;

public class TestSpawner : MonoBehaviour {

	public GameObject tank;
	public float fireRate = 2;
	public float lastFire = 0;
	public float time;

	/*
	void Start () {
		nextFire = fireRate;
	}*/

	void Update () {

		time = Time.time;

		if(Time.time > fireRate + lastFire){
			Instantiate(tank, this.transform.position, tank.transform.rotation);
			lastFire = Time.time;	
		}

	}
}
