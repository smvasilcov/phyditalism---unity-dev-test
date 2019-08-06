using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball_controller : MonoBehaviour
{
    public bool isActive = false;
    public float speed = 1.0f;
    float savedSpeed = 0;
    public int speedMultiplicator = 20;
    public float delta = 0.01f;  // for smooth interpotation

    public int counter = 0;
    public int counterLimit = 0;

    public bool reversedTrajectory = false; // false = ball goes forward; true = ball goes back by inversed trajectory
    bool moving = false;
    float timer = 0;
    
    float clickDelay = 0.5f;    // for double click
    float clickTimer = 10;
    int numOfClicks = 0;

    json_parser Parser;

    void Start()
    {
        Parser = GetComponent<json_parser>();
        counterLimit = Parser.coordinates.Length;
        transform.position = Parser.coordinates[0];
        if (isActive)
            SetActive(true);
        else
            SetActive(false);
    }

    void FixedUpdate()
    {
        CheckClick();
        MoveBall();
        SpeedSliderCheck();
    }

    void CheckClick()  // Check if the ball was clicked
    {
        clickTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Ball") && hit.transform.gameObject==gameObject && isActive)
                {
                    if (clickTimer < clickDelay)
                    {
                        Debug.Log("DoubleClick");
                        SetToStartPosition();
                    }
                    else
                    {
                        Debug.Log("Clicked");
                        moving = true;
                    }
                        
                    clickTimer = 0;
                }
            }
        }
    }

    public void SpeedSliderCheck()
    {
        speed = 0;
        if(isActive)
            speed = speedMultiplicator * GameObject.FindGameObjectWithTag("SpeedSlider").GetComponent<Slider>().value;
    }

    void MoveBall()
    {
        if (moving)
        {
            timer = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, Parser.coordinates[counter], timer);
            
            if ((transform.position - Parser.coordinates[counter]).magnitude < delta)
            {
                if (!reversedTrajectory)    // when ball goes forward
                {
                    counter++;      // cout steps on trajectory
                    if (counter >= counterLimit)
                    {
                        SetToEndPosition();
                    }
                }
                else                         // when ball goes back
                {
                    counter--;
                    if (counter <= 0)
                    {
                        SetToStartPosition();
                    }
                }
            }
        }
    }

    void SetToStartPosition()
    {
        moving = false;
        reversedTrajectory = false;
        counter = 0;
        transform.position = Parser.coordinates[counter];
        //transform.position = new Vector3(Parser.x[counter], Parser.y[counter], Parser.z[counter]);
    }

    void SetToEndPosition()
    {
        moving = false;
        reversedTrajectory = true;
        counter = Parser.coordinates.Length-1;
        transform.position = Parser.coordinates[counter];
        //transform.position = new Vector3(Parser.x[counter], Parser.y[counter], Parser.z[counter]);
    }

    public void SetActive(bool state)
    {
        switch(state)
        {
            case true:
                isActive = true;
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case false:
                isActive = false;
                GetComponent<Renderer>().material.color = Color.gray;
                break;
        }
    }

}
