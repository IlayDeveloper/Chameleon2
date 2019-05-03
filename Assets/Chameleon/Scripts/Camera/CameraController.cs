using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    public Camera myCam;
    public GameObject middleLevel;
    public GameObject largeLevel;
    public GameObject veryLargeLevele;
    private float offsetX;
    public const float SMALL_SIZE = 350;
    public const float MIDDLE_SIZE = 500;
    public const float LARGE_SIZE = 700;
    public const float VERY_LARGE_SIZE = 1000;
    public const float SPEED = 0.2f;

    private Level activeCorouine;

    public enum Level
    {
        Small,
        Middle,
        Large,
        VeryLarge
    }

    public Level currentLevel = Level.Small;
    void Start()
    {
        this.offsetX = this.ball.transform.position.x - transform.position.x;
    }

    void Update()
    {
        Vector3 oldPosition = transform.position;
        transform.position = new Vector3(this.ball.transform.position.x - this.offsetX, oldPosition.y, oldPosition.z);

        CheckCurrentLevel();

        if(currentLevel == Level.Small && myCam.orthographicSize > SMALL_SIZE)
        {
            StartCoroutine("ToSmall");
            activeCorouine = Level.Small;
        }
        else if(currentLevel == Level.Middle && myCam.orthographicSize > MIDDLE_SIZE)
        {
            StartCoroutine("ToMiddle", "Down");
            activeCorouine = Level.Middle;
        }
        else if(currentLevel == Level.Middle && myCam.orthographicSize < MIDDLE_SIZE)
        {
             StartCoroutine("ToMiddle", "Up");
             activeCorouine = Level.Middle;
        }
        else if(currentLevel == Level.Large && myCam.orthographicSize < LARGE_SIZE)
        {
             StartCoroutine("ToLarge", "Up");
             activeCorouine = Level.Large;
        }
        else if (currentLevel == Level.Large && myCam.orthographicSize > LARGE_SIZE)
        {
            StartCoroutine("ToLarge", "Down");
            activeCorouine = Level.Large;
        }
        else if (currentLevel == Level.VeryLarge && myCam.orthographicSize > VERY_LARGE_SIZE)
        {
            StartCoroutine("ToVeryLarge");
            activeCorouine = Level.VeryLarge;
        }
    }

    private void CheckCurrentLevel()
    {
        if(ball.transform.position.y < middleLevel.transform.position.y)
        {
            currentLevel = Level.Small;
        }
        else if (ball.transform.position.y > middleLevel.transform.position.y && ball.transform.position.y < largeLevel.transform.position.y)
        {
            currentLevel = Level.Middle;
        }
        else if (ball.transform.position.y > largeLevel.transform.position.y && ball.transform.position.y < veryLargeLevele.transform.position.y)
        {
            currentLevel = Level.Large;
        }
        else if (ball.transform.position.y > veryLargeLevele.transform.position.y)
        {
            currentLevel = Level.VeryLarge;
        }
    }

    IEnumerator ToSmall()
    {
       while(myCam.orthographicSize > SMALL_SIZE)
       {
            myCam.orthographicSize -= SPEED; 
            yield return null; 
       }
    }

    IEnumerator ToMiddle(string direct)
    {
        if(direct == "Up")
        {
            while(myCam.orthographicSize < MIDDLE_SIZE)
            {
                myCam.orthographicSize += SPEED; 
                yield return null; 
            }
        }
        else
        {
            while(myCam.orthographicSize > MIDDLE_SIZE)
            {
                myCam.orthographicSize -= SPEED; 
                yield return null; 
            }
        }
    }

    IEnumerator ToLarge(string direct)
    {
        if (direct == "Up")
        {
            while(myCam.orthographicSize < LARGE_SIZE)
            {
                myCam.orthographicSize += SPEED; 
                yield return null; 
            }
        }
        else
        {
            while(myCam.orthographicSize > LARGE_SIZE)
            {
                myCam.orthographicSize -= SPEED; 
                yield return null; 
            }
        }
    }

    IEnumerator ToVeryLarge()
    {
       while(myCam.orthographicSize < VERY_LARGE_SIZE)
       {
            myCam.orthographicSize += SPEED; 
            yield return null; 
       }
    }



}
