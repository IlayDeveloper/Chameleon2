using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructor : MonoBehaviour {

	public GameObject colorSwitcher;
	public GamePlay gp;
	public GameObject coin;
	//public GameObject[] coins;
	public GameObject[] details;
	public GroundModel[] alltPath;
	public GameObject player;
	public GroundModel centralGround;
	public static Vector3 beginPosition = Vector3.zero;
	private GroundModel startLine;
	void Awake () 
	{
		CreateNewPath();
	}

	void Update ()
	{
		UpdatePath();
	}
	
	void CreateNewPath (int count = 20)
	{
		List<GroundModel> newPath = new List<GroundModel>();
		GameObject go;
		GroundModel gm;
		int counter = 0;

		for (int i = 0; i < count; i++)
		{
			if(i == 0)
			{
				go = Instantiate(details[0], Vector3.zero, Quaternion.identity);
				gm = go.GetComponent<GroundModel>();
				gm.myRenderer.material.color = gp.curentColors[(int)Colors.First];
				gm.myColor = Colors.First;
				startLine = gm;
				//newPath.Add(gm);
				
			}
			else
			{
				go = Instantiate(details[Random.Range(1, details.Length)], beginPosition, Quaternion.identity);
				gm = go.GetComponent<GroundModel>();

				if (i == 1)
				{
					gm.Setup(startLine);	
				}
				else
				{
					gm.Setup(newPath[i-2]);
				}
			
				gm.ChangeColor();
				newPath.Add(gm);

				//Instantiate color switcher
				counter = Random.Range(0, 2);
				for (int k = 0; k < counter; k++)
				{
					int x = Random.Range(0, 800);
					int y = Random.Range(400, 600);

					Instantiate(colorSwitcher, gm.transform.position + new Vector3(x ,y, 0), Quaternion.identity, gm.transform);
				}

				//Instantiate coins
				counter = Random.Range(0, 2);
				for (int t = 0; t < counter; t++)
				{
					int x2 = Random.Range(0, 1000);
					int y2 = Random.Range(400, 600);

					Instantiate(coin, gm.transform.position + new Vector3(x2 ,y2, 0), Quaternion.identity, gm.transform);
				}
			}
		}

		alltPath = newPath.ToArray();
		centralGround = alltPath[alltPath.Length/2];
	}

	void UpdatePath ()
	{
		if(player.transform.position.x > centralGround.transform.position.x)
		{
			alltPath[0].Setup(alltPath[alltPath.Length - 1]);
			ShiftArrayLeft();
		}
	}

	private void ShiftArrayLeft ()
	{
		GroundModel firstGameModel =  alltPath[0];

		for (int i = 0; i < alltPath.Length - 1; i++)
		{
			alltPath[i] = alltPath[i + 1];
		}
		alltPath[alltPath.Length - 1] = firstGameModel;
		centralGround = alltPath[alltPath.Length/2];
	}

	public void RegeneratePath ()
	{
		foreach (GroundModel gm in alltPath)
		{
			Destroy(gm.gameObject); 
		}
		Destroy(startLine.gameObject);
		CreateNewPath();
	}
}
