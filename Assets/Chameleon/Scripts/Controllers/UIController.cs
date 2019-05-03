using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject pauseWindow;
	public GameObject gameOverWindow;
	public GameObject settingsWindow;
	public GameObject uiMainMenu;
	public GameObject uiPlaying;
	public Text coinsText;
	public Text pointsText;
	public GamePlay gp;
	public Animator commentAnim;
	public Animator xPointsAnim;
	public Text commentText;
	public Text xPointText;

	void Start () 
	{
		
	}	

	public void PlayingState ()
	{
		pauseWindow.SetActive(false);
		gameOverWindow.SetActive(false);
		uiPlaying.SetActive(true);
		uiMainMenu.SetActive(false);
		gp.UnPause();
	}

	public void MainMenuState ()
	{
		pauseWindow.SetActive(false);
		gameOverWindow.SetActive(false);
		uiPlaying.SetActive(false);
		uiMainMenu.SetActive(true);
		gp.Restart();
		gp.UnPause();
		gp.ToStation(GameStation.MainMenu);
	}

	public void GameOverState ()
	{
		gameOverWindow.SetActive(true);
	}

	public void PauseState ()
	{
		pauseWindow.SetActive(true);
		gp.Pause();
	}

	public void ShowSettings ()
	{
		settingsWindow.SetActive(true);
		gp.Pause();
	}

	public void CloseSettings ()
	{
		settingsWindow.SetActive(false);
		gp.UnPause();
	}

	public void Restart ()
	{
		gp.Restart();
		PlayingState();
	}

	public void UpdateCoins (int coins)
	{
		coinsText.text = coins.ToString();
	}
	public void UpdatePoints (int points)
	{
		pointsText.text = points.ToString();
	}

	public void SuperMode (bool trigger, int accelCount = 0)
	{
		if (trigger)
		{
			commentText.text = "X" + (accelCount*2).ToString() + " SMOOTHLY";
			commentAnim.SetTrigger("ShowUp");

			xPointText.text = "X" + (accelCount*2).ToString();
			xPointsAnim.SetTrigger("SuperMode");
			pointsText.color = gp.curentColors[(int)Colors.First];
		}
		else
		{
			xPointsAnim.SetTrigger("UsualMode");
			pointsText.color = Color.white;
		}
	}
}
