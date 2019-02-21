using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animationController : MonoBehaviour {

    public Sprite frame1, frame2, frameBot, frameMid, frameTop;
    public GameObject camera;
    byte state;
    
	void Start () {
        state = 0;        
	}
	
	
	void Update () {
        
        switch (state)
        {
            case 0: // idle
                if (Mathf.Floor(Time.time) % 2 == 0)
                {                    
                    GetComponent<Image>().sprite = frame1;
                }
                else
                {                   
                    GetComponent<Image>().sprite = frame2;
                }
                break;
            case 1: // bot
                GetComponent<Image>().sprite = frameBot;
                break;
            case 2: // mid
                GetComponent<Image>().sprite = frameMid;
                break;
            case 3: // top
                GetComponent<Image>().sprite = frameTop;
                break;

        }
    }

    public void changeState(string strState)
    {
        StopAllCoroutines();       
        if (strState == "b")
            state = 1;
        else if (strState == "m")
            state = 2;
        else if (strState == "t")
            state = 3;
        else
            state = 0;
        StartCoroutine(delayStateZero(0.5f));
    }
    IEnumerator delayStateZero(float timeDelay)
    {        
        yield return new WaitForSeconds(timeDelay);
        camera.GetComponent<Player>().resetBallState();
        state = 0;
    }
}
