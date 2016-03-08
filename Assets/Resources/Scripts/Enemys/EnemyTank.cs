using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyTank : MonoBehaviour {


	public float speed = 1f;
	public float time = 0.0f;
	public float health = 100;


    int collisionUpdateRate = 1;
    float nextUpdateTime = 3;

	public PolygonCollider2D pathMask;
    public Image healthBar;
    public GameObject canvas;

    public Color tenPercent, twentyFivePercent, fiftyPercent, sixtyFivePercent, eightyPercent, fullHealth;

	void Start() {
		pathMask = GameObject.Find ("Plains With Lake 2 path").GetComponent<PolygonCollider2D> ();
        Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), pathMask);
    }

	void Update () {

        canvas.transform.position = this.transform.position;

        healthBar.fillAmount = health / 100;

        if(health > 99)
        {
            healthBar.color = fullHealth;
        }

        if (health < 81 && health > 65)
        {
            healthBar.color = eightyPercent;
        }
        if (health < 66 && health > 50)
        {
            healthBar.color = sixtyFivePercent;
        }
        if (health < 51 && health > 25)
        {
            healthBar.color = fiftyPercent;
        }
        if (health < 26 && health > 10)
        {
            healthBar.color = twentyFivePercent;
        }
        if (health < 11 && health > 0)
        {
            healthBar.color = tenPercent;
        }

        timeScale();
		time++;

		Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
		transform.Translate(-Vector2.right * Time.deltaTime * speed);

        if (health <= 0) {
			gameObject.SendMessageUpwards("destroyTank");
		}
    }

	public float getTime(){
		return time;
	}

	public float getHealth(){
		return health;
	}

	public void setHealth(float newHealth){
		health = newHealth;
	}

	void timeScale (){
		if(Input.GetKey(KeyCode.Alpha1)) {Time.timeScale = 0.5f; }

		if(Input.GetKey(KeyCode.Alpha2)) {Time.timeScale = 1f; }

		if(Input.GetKey(KeyCode.Alpha3)) {Time.timeScale = 2f; }

		if(Input.GetKey(KeyCode.Alpha4)) {Time.timeScale = 4f; }
	}
}
