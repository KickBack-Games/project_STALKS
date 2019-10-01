using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour 
{
	public GameObject spawner, bg1, bg2, monster, monsterPopUp, GUIManager, heart;
	public float speed = 1.5f, 
				 bgOffset;
	public bool moving, inMenu, inSettings, playing, lost, onceGameplayLock, spawned = true;
	public Vector3 target, finalTarget;

	private int score, powerUpCounter = 0;
	private spawner spawnerScript;
	private Vector2 myPos, dir, beginningPoint;
	private SpriteRenderer sr;
	private Animator anim;
	public Animation animation;

	private GUIMANAGER gui;

 	void Awake()
 	{
 		inMenu = true;
 		inSettings = false;
 		playing = false;
 		onceGameplayLock = false;
 		Time.timeScale = 1;
        Application.targetFrameRate = 60;

 	}
	void Start () 
	{
		gui = GUIManager.GetComponent<GUIMANAGER>();
		anim = GetComponent<Animator>();
		animation = GetComponent<Animation>();
		sr = GetComponent<SpriteRenderer>();
		spawnerScript = spawner.GetComponent<spawner>();
		moving = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (inMenu || inSettings)
		{
			if (playing) 
			{
				inMenu = false;	
				monster.SetActive(true);
				spawner.SetActive(true);
				// player will stop here
				beginningPoint = spawner.transform.position;
				anim.SetBool("isMoving", true);
			}
			anim.SetBool("isMoving", false);
			//transform.position = new Vector2(-18f, transform.position.y + 1);
		}
		else
		{
			// Sort of scene-ish... player keeps moving up until the player hits the forest.. then game BEGINS
			if (!onceGameplayLock)
			{
				heart.SetActive(true);
				transform.position = new Vector2(-18f, transform.position.y - 1);
				if (transform.position.y <= beginningPoint.y + 70f)
					onceGameplayLock = true;
			}
			else
			{
				sr.sortingOrder = -(int)transform.position.y;
				if (Input.GetMouseButtonDown(0)) 
				{
					if (!moving)
					{
		            	//target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		            	//target.z = transform.position.z;
		            	
		            	spawned = false;

					}
				}
				if (transform.position == target)
				{
					if (!spawned)
					{
						//spawnerScript.spawn();
						spawned = true;
						anim.SetBool("isMoving", false);
					}
					moving = false;
				}

				if (moving)
				{
					anim.SetBool("isMoving", true);
					transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

				}
			}
		}
		if (transform.position.y <= bg1.transform.position.y + bgOffset)
		{
			bg1.transform.position = new Vector2(0, bg1.transform.position.y - 768);
		}
		if (transform.position.y <= bg2.transform.position.y + bgOffset)
		{
			bg2.transform.position = new Vector2(0, bg2.transform.position.y - 768);
		}
	}

	// MAKE THE PLAYER LOSE WHEN THEY TOUCH
    void OnTriggerEnter2D(Collider2D col)
    {
    	if (col.gameObject.tag == "monster")
    	{
    		onLost();
    		print("IT got you!");

    	}
		if (col.gameObject.tag == "powerup")
    	{
    		print("BAM!");
    		monster.transform.position = new Vector2(0f, transform.position.y + 300);
    		Destroy(col.gameObject);

    	}
    }

	void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void setScore(int score)
    {
    	this.score = this.getScore() + score;
    	gui.setScoreText(this.score);
    	FindObjectOfType<SM>().Play("base");
    }

    public int getScore()
    {
    	return this.score;
    }

    public void onLost()
    {
    	lost = true;
		CameraShake.Shake(.2f, 10f);
		if (PlayerPrefs.GetInt("prankMode") == 0)
		{
			FindObjectOfType<SM>().Play("shriek");
			monsterPopUp.SetActive(true);
		}
		monster.SetActive(false);
		gameObject.SetActive(false);
		heart.SetActive(false);
		FindObjectOfType<SM>().Stop("tension");
		
    }

    public void powerUpCounterOneUp() 
    { 
    	this.powerUpCounter++;
    	print(powerUpCounter);
    }
    public int getPowerUpCounterOneUp() 
    { 
    	return this.powerUpCounter;
    }
}
