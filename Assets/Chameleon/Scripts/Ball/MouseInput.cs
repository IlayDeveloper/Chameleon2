using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public BallController ball;
	public void OnPointerDown(PointerEventData eventData)
    {
        ball.AddForce();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ball.RemoveForce();
    }
}
