using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {

   	public float game_speed;

 	// Use this for initialization
 	void Start () {
 
 	}
 
 	// Update is called once per frame
 	void Update () {
		Vector2 offset = new Vector2(0, Time.time * game_speed);
		GetComponent<Renderer>().material.mainTextureOffset = offset;       
	}﻿
}
