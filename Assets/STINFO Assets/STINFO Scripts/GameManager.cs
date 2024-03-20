using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator transition;
    public GameObject startButton;
    public List<STINFOQuestionsAndAnswers> QnA;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        startButton.SetActive(false);
        transition.SetTrigger("Start");
    }
}
