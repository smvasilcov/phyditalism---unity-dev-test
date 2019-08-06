using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_active_ball : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;

    public int index = 0;
    public int i = 0;

    void Start()
    {
        leftButton.onClick.AddListener(Prevoius);
        rightButton.onClick.AddListener(Next);
    }

    void Next()  // a bit scary / gonna make it look better
    {
        i = 0;
        index = 0;
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))  // figure out which ball is active now
        {
            if (ball.GetComponent<ball_controller>().isActive)
                index = i;
            i++;
        }

        // set next ball active
        GameObject.FindGameObjectsWithTag("Ball")[index].SendMessage("SetActive", false);
        if (index >= GameObject.FindGameObjectsWithTag("Ball").Length - 1)
            GameObject.FindGameObjectsWithTag("Ball")[0].SendMessage("SetActive", true);
        else
            GameObject.FindGameObjectsWithTag("Ball")[index + 1].SendMessage("SetActive", true);
    }

    void Prevoius()  // a bit scary / gonna make it look better [2]
    {
        i = 0;
        index = 0;
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))  // figure out which ball is active now
        {
            if (ball.GetComponent<ball_controller>().isActive)
                index = i;
            i++;
        }

        // set previous ball active
        GameObject.FindGameObjectsWithTag("Ball")[index].SendMessage("SetActive", false);
        if (index <= 0)
            GameObject.FindGameObjectsWithTag("Ball")[i - 1].SendMessage("SetActive", true);
        else
            GameObject.FindGameObjectsWithTag("Ball")[index - 1].SendMessage("SetActive", true);



    }
}
