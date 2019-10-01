using UnityEngine;
using System;

public class overNoTree : MonoBehaviour 
{
	private GameObject player;
	private playerMovement pm;

	public GameObject spawner;
	private spawner spawnerScript;

	void Start()
	{
		player = GameObject.Find("player");
		spawner = GameObject.Find("spawner");

		try 
		{
			pm = player.GetComponent<playerMovement>();
			spawnerScript = spawner.GetComponent<spawner>();
		}
		catch(NullReferenceException ex) 
		{
        	Debug.Log("He is gone..." + ex);
        }
	}

    void OnMouseOver()
    {
    	if (Input.GetMouseButtonDown(0) && player.transform.position.y <= -140f) 
		{
			try
			{
				if (!pm.moving)
				{
					if (player.transform.position.y > transform.position.y + 10)
					{
						pm.moving = true;
						spawnerScript.spawn(10);
						pm.target = new Vector2(transform.position.x, transform.position.y + 10);
						pm.setScore(1);
						pm.powerUpCounterOneUp();
					}
				}
			}
			catch(NullReferenceException ex) {
            	Debug.Log("He is gone..." + ex);
            }
		}
    }
}

