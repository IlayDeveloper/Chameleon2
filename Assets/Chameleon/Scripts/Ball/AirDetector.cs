using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDetector : MonoBehaviour {

	public bool inAir = false;
	public GameObject ball;

	void FixedUpdate ()
	{		
		Collider2D col = Physics2D.OverlapCircle(ball.transform.position, 120, 1 << 8);
		if (! col)
		{
			inAir = true;
		}
			
	}
	
}
