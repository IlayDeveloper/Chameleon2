using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundModel : MonoBehaviour {

	public Colors myColor;
	private GamePlay gp;
	public Renderer myRenderer;
	//public Vector3 connectorLeft;
	public Transform connectorRight;
	void Awake ()
	{
		gp = GameObject.FindGameObjectWithTag("GamePlay").GetComponent<GamePlay>();
	}

	public void Setup (GroundModel gm)
	{
		transform.position = gm.connectorRight.position;
	}

	public void ChangeColor ()
	{
		if (Random.Range(0, 10) < 5)
		{
			myColor = Colors.First;
			
		}
		else
		{
			myColor = Colors.Second;
		}
		myRenderer.material.color = gp.curentColors[(int)this.myColor];
	}
	
}
