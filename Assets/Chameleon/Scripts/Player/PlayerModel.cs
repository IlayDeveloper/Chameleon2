using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour 
{
	[HideInInspector]
	public string isInitilized = "no";
	[HideInInspector]
	public string noADS = "no";
	[HideInInspector]
	private int coins = 0;

	void Awake () 
	{
		CheckPlayer();
	}

	public int Coins
	{
		get
		{
			return coins;
		}
		set
		{
			coins = value;
			PlayerPrefs.SetInt("coins", coins);
			PlayerPrefs.Save();
		}
	}

	private void CheckPlayer ()
	{
		isInitilized = PlayerPrefs.GetString("isInitilized");
		if (isInitilized == "yes")
		{
			GetPlayer();
		}
		else
		{
			CreatePlayer();
		}
	}

	private void CreatePlayer ()
	{
		PlayerPrefs.SetInt("coins", 0);
		PlayerPrefs.SetString("noADS", "no");
		PlayerPrefs.SetString("isInitilized", "yes");
		PlayerPrefs.Save();

		GetPlayer();
	}

	public void GetPlayer ()
	{
		isInitilized = PlayerPrefs.GetString("isInitilized");
		noADS = PlayerPrefs.GetString("noADS");
		coins = PlayerPrefs.GetInt("coins");
	}

	public void UpdatePlayer ()
	{
		PlayerPrefs.SetInt("coins", coins);
		PlayerPrefs.SetString("noADS", noADS);
		PlayerPrefs.SetString("isInitilized", isInitilized);
		PlayerPrefs.Save();
	}
	
}
