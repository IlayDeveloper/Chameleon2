using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStation
{
	Play,
	Pause,
	GameOver,
	MainMenu
}

public enum Colors
{
    First,
	Second,
	Empty 
}

public class GamePlay : MonoBehaviour {

	public const int changeColorGameCount = 3;
	public const float startRangeX = 500;

	//references
	public GameStation station = GameStation.MainMenu;
	public BallController ball;
	public UIController uiController;
	public Constructor constructor;
	public BackGroundController bgController;
	public PlayerModel playerModel;

	//variables
	public Color32[] curentColors;
	public Color32[] stackColors;
	public int coins = 0;
	public float points = 0;
	public int gameCount = 0;
	private float previouslyPositionX;

	void Awake ()
	{
		DefineColors();
		previouslyPositionX = ball.transform.position.x;
	}

	void Start ()
	{
		coins = playerModel.Coins;
		uiController.UpdateCoins(coins);
	}
	
	void Update () 
	{
		if(station == GameStation.MainMenu && ball.transform.position.x >= startRangeX)
		{
			ToStation(GameStation.Play);
			uiController.PlayingState();
		}

		if (station == GameStation.Play)
		CalculatePoints();	
	}
	private void CalculatePoints ()
	{
		points += (ball.transform.position.x - previouslyPositionX)/(100/(ball.accelCount+1));
		
		uiController.UpdatePoints((int)points);
		previouslyPositionX = ball.transform.position.x;
	}

	private void DefineColors ()
	{
		curentColors[0] = stackColors[Random.Range(0, 7)];
		curentColors[1] = stackColors[Random.Range(8, 15)];
	}

	public void ToStation (GameStation state)
	{
		station = state;
	}

	public void GameOver ()
	{
		ToStation(GameStation.GameOver);
		uiController.GameOverState();
		playerModel.Coins = coins;
	}

	public void Pause ()
	{
		Time.timeScale = 0;
	}

	public void UnPause ()
	{
		Time.timeScale = 1;
	}

	public void Play ()
	{
		uiController.PlayingState();
	}

	public void Restart ()
	{
		gameCount++;
		if(changeColorGameCount <= gameCount)
		{
			DefineColors();
			gameCount = 0;
		}
		ball.Restart();
		bgController.Restart();
		constructor.RegeneratePath();
		points = 0;
		previouslyPositionX = ball.beginPosition.x;
		uiController.UpdatePoints((int)points);
		if(ball.isAccelerated)
		{
			ball.StopRightNow();
		}
		
	}

	public void AddCoin ()
	{
		coins++;
		uiController.UpdateCoins(coins);
	}
}
