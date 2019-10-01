using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour 
{
	public GameObject lostMenu;
	public Button retryButton;
	private SpriteRenderer sr;
	private float opacity = 1;
	// Use this for initialization
	void Start () 
	{
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(opacity > 0)

			opacity -= .01f;
		else
		{
			lostMenu.SetActive(true);
			Destroy(this.gameObject);

		}
		sr.color = new Color(1,1,1,opacity);

	}
}
