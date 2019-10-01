using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterBehavior : MonoBehaviour 
{
	public GameObject human, warning;
	public bool chasing, snatching, berzerk;
	public float speed, snatchDist, snatchCooldown;
	private float startSpeed,
				  berzerkSpeed = 170;

	private int[] posSnatch = {-53, -18, 18, 53};
	public int berzerkTime, intensity;
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () 
	{
		intensity = 0;
		startSpeed = speed;
		sr = GetComponent<SpriteRenderer>();
		chasing = true;
		berzerkTime = 600;
	}
	
	// Update is called once per frame
	void Update () 
	{
		sr.sortingOrder = -(int)transform.position.y;
		if (snatching)
		{
			warning.SetActive(true);
			int i = Random.Range(0,3);
			transform.position = new Vector2(posSnatch[i], human.transform.position.y - 325f);
			//snatchCooldown = 160;
			// do this once
			snatching = false;
			berzerkTime = Random.Range(400, 1000);
			berzerk = false;
			FindObjectOfType<SM>().Play("base");
		}
		if (chasing) 
		{
			if (berzerk)
			{
				// faster!
				speed = berzerkSpeed;
				sr.color = Color.red;
				if (berzerkTime > 0)
				{
					berzerkTime--;	
				}
				else
				{
					berzerk = false;
					// Difficulty elements
					if (intensity < 300) {
						intensity += 50;
					}
					if (berzerkSpeed < 200) {
						berzerkSpeed += 5;
					}
					if (startSpeed < 155) {
						startSpeed += 2;
					}

					chasing = true;
					berzerkTime = Random.Range(400 - intensity, 1000 - intensity);
					CameraShake.Shake(.05f,1f);
					FindObjectOfType<SM>().Play("base");
				}
			}
			else
			{
				// normal behavior
				sr.color = Color.white;
				// if you go too far,  it will get real close to try to snatch
				if (Vector2.Distance(transform.position, human.transform.position) > snatchDist)
				{
					chasing = false;
					snatching = true;
				}
				if (speed > startSpeed)
				{
					speed--;
				}
				if (berzerkTime > 0)
				{
					berzerkTime--;	
				}
				else
				{
					CameraShake.Shake(.05f,1f);
					berzerk = true;
					berzerkTime = 300;
				}
				


			}

			// keep following
			transform.position = Vector3.MoveTowards(transform.position, human.transform.position, speed * Time.deltaTime);
		}
		else
		{
			//	NOT CHASING YET, still in the snatch phase until cooldown is done
			if (transform.position.y - 75f < human.transform.position.y)
			{
				//snatchCooldown--;
				// move upward!!!
				transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * 100);
			}
			else
			{
				// cooldown is done!!! go back to chasing
				chasing = true;
				speed = 160;
				warning.SetActive(false);
				CameraShake.Shake(.1f,2f);
			}
		}

	}
	
}
