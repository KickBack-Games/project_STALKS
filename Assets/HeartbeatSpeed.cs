using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class HeartbeatSpeed : MonoBehaviour
	{
		private Animator anim;


		public float animSpeed;
		public GameObject player, it;
		// Start is called before the first frame update
		void Start()
		{
			anim = GetComponent<Animator>();
			anim.speed = animSpeed;   
		}

		// Update is called once per frame
		void Update()
		{

			if (it.transform.position.y - player.transform.position.y <= 55) {
				anim.speed = 2.4f; 
			}
			else if (it.transform.position.y - player.transform.position.y <= 100) {
				anim.speed = 1.8f;
			}
			else if (it.transform.position.y - player.transform.position.y <= 160) {
				anim.speed = 1.2f;
			}
			else {
				anim.speed = 1;
			}
		}
	}
