using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class GUIMANAGER : MonoBehaviour 
{
	public Text ttpTxt, retryTxt, scoreTxt, highscoreTxt;
	public GameObject player, menu, mainMenu, settingsMenu, lostMenu, inGameMenu;

	private bool onceLock, onceLockLost;
	private playerMovement PM;
	private float ttpCount;
	// Use this for initialization
	void Start () 
	{
		PM = player.GetComponent<playerMovement>();

		onceLock = false;
		onceLockLost = false;
		scoreTxt.text = "0";
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (PM.inMenu)
		{
			if (ttpCount < 1f)			
			{
				ttpCount += .03f;
			}
			else
			{
				if (ttpTxt.text == "")
					ttpTxt.text = "- TAP  TO  PLAY -";
				else
					ttpTxt.text = "";

				ttpCount = 0f;
			}
			menu.transform.position = Vector2.MoveTowards(menu.transform.position, new Vector2(0, 0), 2000f * Time.deltaTime);

		}
		else if (PM.inSettings) 
		{
			menu.transform.position = Vector2.MoveTowards(menu.transform.position, new Vector2(-160f, 0), 2000f * Time.deltaTime);
			ttpTxt.text = "";
		}
		else
		{
			menu.transform.position = Vector2.MoveTowards(menu.transform.position, new Vector2(0, player.transform.position.y), 2000f * Time.deltaTime);
			if (!inGameMenu.active)
			{
				inGameMenu.SetActive(true);
			}
			if (!onceLock && PM.lost)
			{
				onceLock = true;
				ttpTxt.text = "";
				menu.SetActive(true);
				settingsMenu.SetActive(false);
				mainMenu.SetActive(false);

				// Set the Highscore!
				if (PM.getScore() > PlayerPrefs.GetInt("highscore", 0))
				{
					PlayerPrefs.SetInt("highscore", PM.getScore());
				}
				highscoreTxt.text += "  " + PlayerPrefs.GetInt("highscore", 0).ToString();
				scoreTxt.text = "";
				inGameMenu.SetActive(false);
				// Make it appear immediately, since no pop up, and typically pop up is the one to pop lost menu
				if (PlayerPrefs.GetInt("prankMode") == 1)
				{
					lostMenu.SetActive(true);
					PlayerPrefs.SetInt("prankMode", 0);
				}

			}
		}
	}

	public void UI_retry()
	{
		if (PM.lost)
		{
 			Application.LoadLevel(Application.loadedLevel);
		}
	}

	public void toSettings() 
	{
		PM.inSettings = true;
		PM.inMenu = false;
		print("To the settings!");
	}

	public void toMainMenu() 
	{
		PM.inSettings = false;
		PM.inMenu = true;
	}

	public void startGame() 
	{
		if (!PM.inSettings) {
			PM.playing = true;
			mainMenu.SetActive(false);
			FindObjectOfType<SM>().Play("tension");
		}
	}

	public void togglePrankMode()
	{
		// OFF
		if (PlayerPrefs.GetInt("prankMode", 0) == 0)
		{
			PlayerPrefs.SetInt("prankMode", 1);
		}
		else
		{
			PlayerPrefs.SetInt("prankMode", 0);
		}
	}

	public void setScoreText(int score)
	{
		scoreTxt.text = score.ToString();
	} 
}
