using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSize : MonoBehaviour 
{
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () 
	{
		sr = GetComponent<SpriteRenderer>();
		transform.localScale = new Vector2(3, Random.Range(2.5f,3.2f));	
	}
	
	void Update()
	{
		sr.sortingOrder = -(int)transform.position.y;
	}
}
