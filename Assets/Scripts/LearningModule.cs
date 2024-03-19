/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 2/28/24
 * 
 * Desc: A script that displays info then a question then info over and over until no questions remain
 */
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LearningModule : QuestionModule
{
    public InfoQuestionPair[] infoQuestions;

    // Info Refs
    public GameObject infoContainer;

    // Question Refs
    public GameObject questionAnswersContainer;

    // Button Refs
    public Button continueButton;
    public Button backButton;


    // Private vars
    private int currentQuestionIndex = 0;
    private bool showingInfo;



    private void Start()
    {
        continueButton.onClick.AddListener(() => StartCoroutine(Continue()));
        backButton.onClick.AddListener(() => StartCoroutine(Back()));


        ShowCurrentInfo();
    }



    public void ShowCurrentInfo()
    {   // Increment Current Question
        continueButton.interactable = true;

        resultText.gameObject.SetActive(false);

        // Set Info container Text to current Question Info
        infoContainer.GetComponentInChildren<Text>().text = infoQuestions[currentQuestionIndex].info;
        showingInfo = true;

        infoContainer.SetActive(true);
        questionAnswersContainer.SetActive(false);
    }
    public void ShowCurrentQuestion()
    {
        // Destroy old questions answers
        foreach (Transform t in questionAnswersContainer.transform) Destroy(t.gameObject);


        // Create Quiz Question UI
        UnityAction correctCallback = () =>
        {
            ShowResultText(true);
            continueButton.interactable = true;
            DisableButtons(questionAnswersContainer);
        };
        UnityAction incorrectCallback = () =>
        {
            ShowResultText(false);
            DisableButtons(questionAnswersContainer);
        };
            CreateQuizQuestionUI(CurrentQuestionInfoPair.quizQuestion, correctCallback, incorrectCallback);



        showingInfo = false;

        continueButton.interactable = false;

        questionAnswersContainer.SetActive(true);
        infoContainer.SetActive(false);
    }



    public IEnumerator Continue()
    {   

        // Transition
        DisableButtons();
        animator.CrossFade("TransitionIn", 0);
        animator.Update(0); // Updates the current animator state so that we can read anim duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        EnableButtons();

        // Continue
        if (showingInfo) ShowCurrentQuestion();
        else 
        { 
            currentQuestionIndex++;

            // Continue at end
            if (currentQuestionIndex >= infoQuestions.Length)
            {
                OnModuleCompleted();
                yield break;
            }

            ShowCurrentInfo(); 
        }

        // Transition
        animator.CrossFade("TransitionOut", 0);
        animator.Update(0); // Updates the current animator state so that we can read anim duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }
    public IEnumerator Back()
    {   // Back at start
        if (showingInfo && currentQuestionIndex == 0) 
        {
            UnfocusModule();
            yield break; 
        }

        // Transition
        DisableButtons();
        animator.CrossFade("TransitionIn", 0);
        animator.Update(0); // Updates the current animator state so that we can read anim duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        EnableButtons();

        // Back
        if (showingInfo) 
        {
            currentQuestionIndex--;
            ShowCurrentQuestion();
        }
        else 
        {
                ShowCurrentInfo(); 
        }

        // Transition
        animator.CrossFade("TransitionOut", 0);
        animator.Update(0); // Updates the current animator state so that we can read anim duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }


    void EnableButtons()
    {
        continueButton.interactable = true;
        backButton.interactable = true;
    }
    void DisableButtons()
    {
        continueButton.interactable = false;
        backButton.interactable = false;
    }

    public InfoQuestionPair CurrentQuestionInfoPair => infoQuestions[currentQuestionIndex];
}
