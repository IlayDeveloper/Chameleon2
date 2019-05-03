using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackGroundTheme
{
	public string name;
	public GameObject background;
	public GameObject farDetails;
	public GameObject middleDetails;
	public GameObject closeDetails;
}

public class BackGroundController : MonoBehaviour {
	public static Vector3 offset = new Vector3(2900, 0, 0);

    // References
	public Transform parentFarLayer;
	public Transform parentMiddleLayer;
	public Transform parentCloseLayer;
	public Rigidbody2D ball;

	public BackGroundTheme[] themes = new BackGroundTheme[3];

	//private GameObject farLayer;
	private GameObject[] middleLayers = new GameObject[2];
	private GameObject[] closeLayers = new GameObject[2];

	private int activeMiddleLayer;
	private int transMiddleLayer;
	private int activeCloseLayer;
	private int transCloseLayer;

	private Rigidbody2D rbMiddleLayer;
	private Rigidbody2D rbMiddleLayer2;

	void Start ()
	{
		BackGroundTheme currentBG = themes[0];

		Instantiate(currentBG.farDetails, Vector3.zero, Quaternion.identity, parentFarLayer);
		Instantiate(currentBG.background, parentFarLayer.position + new Vector3(0,0, 1000), Quaternion.identity, parentFarLayer);
		middleLayers[0] = Instantiate(currentBG.middleDetails, Vector3.zero, Quaternion.identity);
		middleLayers[1] = Instantiate(currentBG.middleDetails, Vector3.zero + offset, Quaternion.identity);
		closeLayers[0] = Instantiate(currentBG.closeDetails, Vector3.zero, Quaternion.identity);
		closeLayers[1] = Instantiate(currentBG.closeDetails, Vector3.zero + offset, Quaternion.identity);

		rbMiddleLayer =middleLayers[0].AddComponent<Rigidbody2D>();
		rbMiddleLayer.mass = ball.mass;
		rbMiddleLayer.gravityScale = 0;

		rbMiddleLayer2 = middleLayers[1].AddComponent<Rigidbody2D>();
		rbMiddleLayer2.mass = ball.mass;
		rbMiddleLayer2.gravityScale = 0;

		transMiddleLayer = 0;
		activeCloseLayer = 1;
		
		transCloseLayer = 0;
		activeMiddleLayer = 1;
	}
	
	void FixedUpdate () 
	{
		rbMiddleLayer.velocity = new Vector2(ball.velocity.x/1.5f, 0);
		rbMiddleLayer2.velocity = new Vector2(ball.velocity.x/1.5f, 0);

		if (parentFarLayer.position.x >= closeLayers[activeCloseLayer].transform.position.x)
		{
			closeLayers[transCloseLayer].transform.position += offset;
			activeCloseLayer += transCloseLayer;
			transCloseLayer = activeCloseLayer - transCloseLayer;
			activeCloseLayer -= transCloseLayer;
		}
		if (parentFarLayer.position.x >= middleLayers[activeMiddleLayer].transform.position.x)
		{
			middleLayers[transMiddleLayer].transform.position += offset;
			activeMiddleLayer += transMiddleLayer;
			transMiddleLayer = activeMiddleLayer - transMiddleLayer;
			activeMiddleLayer -= transMiddleLayer;
		}
	}

	public void CreateBackGround ()
	{

	}

	public void Restart()
	{
		middleLayers[transMiddleLayer].transform.position = Vector3.zero;
		middleLayers[activeMiddleLayer].transform.position = Vector3.zero + offset;

		closeLayers[transCloseLayer].transform.position = Vector3.zero;
		closeLayers[activeCloseLayer].transform.position = Vector3.zero + offset;
	}
}
