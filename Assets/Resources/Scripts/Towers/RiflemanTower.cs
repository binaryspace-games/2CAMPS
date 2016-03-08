using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RiflemanTower : MonoBehaviour {


	class Sprites{

	}

	public Color color = new Color(0,0,1,1);

	public enum Targeting { first, last, strong }
	public Targeting currentState = Targeting.first; 

	public Animator animator;

	public float fireRate = 2;
	public float lastFire = 0;
	public float range = 2f;
	public float damage = 15f;
	float nextfire = 0.0f; 

	public GameObject currentTargetedEnemy;

	//############################################################ Stats Of Tower  (Base, Up1, Up2, Up3a, Up3b)

	public float rofMultiplier = 1f;                                // ROF: 1, Auto <1 REST>                       /
	public float rangeMultiplier = 1f;                              // Range: 1, 1, 1.5, 2.5, 1.5                   
	public float damageMultiplier = 1f;                             // Damage: 1, 1, 1, 1, 1.4                     /                
	public string fireMode = "3round";
	public int threeRoundCount = 0;
	public int level = 0;

	void Start(){
		animator = this.GetComponentInChildren<Animator> ();
		fireMode = "3round";
	}

	void Update () {

		if (currentState == Targeting.first) {
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll (this.transform.position, range, 1 << LayerMask.NameToLayer ("Enemy"));

            int numberOfEnemys = 0;
			for (int i = 0; i < hitColliders.Length; i++) {
				if (hitColliders [i].tag == "Enemy") {
					numberOfEnemys = numberOfEnemys + 1;
				}
			}
			if (numberOfEnemys == 0) {                                                     //IF WE HIT NOTHING
				stopFire ();
			}
			if (numberOfEnemys > 0) {                                                      //IF WE HIT Something (with a tag of enemy)
				if (Time.time > fireRate + lastFire) {
					lastFire = Time.time;
					if(fireMode == "3round"){
						if (threeRoundCount < 4) {
							Fire (hitColliders);
							threeRoundCount = threeRoundCount + 1;
						} else if (threeRoundCount > 3) {
							threeRoundCount = threeRoundCount + 1;
						} else if (threeRoundCount == 5){
							threeRoundCount = 1;
						}
					}
					else{
						if (Time.time > fireRate + lastFire) {
							Fire (hitColliders);
							lastFire = Time.time;
						}
					}
				}

                Quaternion rotation = Quaternion.LookRotation(currentTargetedEnemy.transform.position - this.transform.position, transform.TransformDirection(Vector3.up));
                transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

            }
        }
	}

	void Upgrade(int lvl){
		if(lvl == 1){
			fireMode = "auto";
		}
		if(lvl == 2){ 
			rangeMultiplier = 1.5f;
		}
		if(lvl == 3){ 
			rofMultiplier = 1.45f;
			rangeMultiplier = 1.2f;

			Collider2D[] hitColliders = getAllInRange (this.transform.position, range);
			for (int i = 0; i < hitColliders.Length; i++) {
				if(hitColliders[i].gameObject.tag == "UnmannedTower"){ hitColliders [i].gameObject.SendMessage("increaseROF", 20);}
			}
		}
		if(lvl == 4){ 
			rofMultiplier = 1.45f;
			rangeMultiplier = 1.2f;

			Collider2D[] hitColliders = getAllInRange (this.transform.position, range);
			for (int i = 0; i < hitColliders.Length; i++) {
				if(hitColliders[i].gameObject.tag == "GroundTower"){ hitColliders [i].gameObject.SendMessage("increaseDMG", 5); }
			}
		}
	}

	void Fire(Collider2D[] hitColliders){
		playFire ();

		float[] times = new float[5];
		int maxTimeIndex = 0; 

		for (int i = 0; i < hitColliders.Length; i++) {
			EnemyTank et = hitColliders [i].GetComponent<EnemyTank> ();
			times[i] = et.getTime();
		}
		for (int n = 0; n < times.Length; n++){
			if (times[n] == Mathf.Max(times)){
				maxTimeIndex = n;
			}
		}
        if(getIfInRange(hitColliders[maxTimeIndex]) == true)
		currentTargetedEnemy = hitColliders[maxTimeIndex].gameObject;
		float currentHealth = currentTargetedEnemy.GetComponent<EnemyTank>().getHealth();
		currentTargetedEnemy.GetComponent<EnemyTank>().setHealth (currentHealth - damage);
	}

	public float findMax(int[] array){
		float maxTime = Mathf.Max(array);
		return maxTime;
	}

	public void playFire(){
		animator.SetBool ("Firing", true);
	}

	public void stopFire(){
		//Stop Fire Anim.
		animator.SetBool("Firing", false);
	}

    public bool getIfInRange(Collider2D hitColl)
    {
        Collider2D[] hitColls = getAllInRange(this.transform.position, range);
        for (int i = 0; i < hitColls.Length; i++)
        {
            if(hitColls[i] == hitColl)
            {
                return true;
            } 
        }
        return false;
        
    }

	void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawWireSphere(this.transform.position, range);

		Gizmos.color = new Color(1,0,0,1);
		Gizmos.DrawWireSphere(this.transform.position, 0.3f);
	}

	public Collider2D[] getAllInRange(Vector3 position, float distance, string mask){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (position, range, 1<< LayerMask.NameToLayer(mask));

		return hitColliders;
	}

	public Collider2D[] getAllInRange(Vector3 position, float distance){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (position, range);

		return hitColliders;
	}

	public void increaseROF(int percentage){
		rofMultiplier = (rofMultiplier / 100) * (100 + percentage);
	}

	public void increaseDMG(int percentage){
		damageMultiplier = (damageMultiplier / 100) * (100 + percentage);
	}
}





