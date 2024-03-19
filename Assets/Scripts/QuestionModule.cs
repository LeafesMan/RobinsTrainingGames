/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 3/5/24
 * 
 * Desc: Base class for modules that require quiz questions,
 *      Handles the creation of quiz questions
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestionModule : MonoBehaviour
{
    [Header("Question Vars")]
    public GameObject questionTemplate;
    public GameObject answerTemplate;
    public Transform questionParent;

    public Text resultText;

    [Header("Show/Hide Button")]
    public Button showHideButton;

    [Header("Animation")]
    public Animator animator;
    public float zoom;


    // Events
    public static event Action ModuleFocused;
    public static event Action ModuleUnfocused;
    public event Action ModuleCompleted;

    private void OnEnable()
    {   // Setup show/hide Button
        showHideButton.onClick.AddListener(FocusModule);

        // When this module is completed destroy showHide button
        ModuleCompleted += () => Destroy(showHideButton.gameObject);
    }

    public void FocusModule()
    {
        // Zoom cam
        Camera.main.GetComponent<CameraHandler>().SetTargetPos(transform.position);
        Camera.main.GetComponent<CameraHandler>().SetTargetZoom(zoom);

        showHideButton.gameObject.SetActive(false);
        ModuleFocused?.Invoke();
    }
    public void UnfocusModule()
    {
        // Revert cam
        Camera.main.GetComponent<CameraHandler>().ResetTargetPos();
        Camera.main.GetComponent<CameraHandler>().ResetTargetZoom();

        showHideButton.gameObject.SetActive(true);
        ModuleUnfocused?.Invoke();
    }



    public void ShowResultText(bool correct)
    {
        resultText.gameObject.SetActive(true);
        resultText.text = correct ? "Correct!" : "Incorrect!";
        resultText.color = correct ? Color.green : Color.red;
    }
    public void HideResultText()
    {
        resultText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the text attached to the given object to the passed in text
    /// </summary>
    public void SetText(GameObject obj, string text)
    {
        Text textBox = obj.GetComponent<Text>();
        if (textBox == null) textBox = obj.GetComponentInChildren<Text>();

        textBox.text = text;
    }

    public void DisableButtons(GameObject parent)
    {
        foreach (Transform buttonTransform in parent.transform)
        {
            Button b = buttonTransform.GetComponent<Button>();
            if (b != null) b.interactable = false;
        }
    }

    /// <summary>
    /// Creates a series of buttons for a quiz question, executing the passed in callbacks upon selecting the in/correct answer.
    /// </summary>
    public void CreateQuizQuestionUI(QuizQuestion quizQuestion, UnityAction correctCallback, UnityAction incorrectCallback)
    {
        // Create new Question and Answers
        GameObject questionObject = Instantiate(questionTemplate, questionParent);
        SetText(questionObject, quizQuestion.question);

        // Init array of answers
        List<(string text, bool correct)> answers = new();

        foreach (string answer in quizQuestion.incorrectAnswers)
            answers.Add((answer, false));         // Adds Incorrect answers

        answers.Add((quizQuestion.correctAnswer, true)); // Adds Correct answer


        // Randomize answers
        List<(string text, bool correct)> randomizedAnswers = new();
        for (int i = answers.Count; i > 0; i--)
        {   // Choose a random index and nab that element from the answers list,
            // appending it to the randomized answers list
            int randIndex = UnityEngine.Random.Range(0, answers.Count);
            randomizedAnswers.Add(answers[randIndex]);
            answers.RemoveAt(randIndex);
        }



        // Create Answers UI
        for (int i = 0; i < randomizedAnswers.Count; i++)
        {
            GameObject answerObject = Instantiate(answerTemplate, questionParent);
            SetText(answerObject, randomizedAnswers[i].text);

            // Is the correct answer --> Bind correct answer event
            if (randomizedAnswers[i].correct)
                answerObject.GetComponentInChildren<Button>().onClick.AddListener(correctCallback);
            // Is an incorrect answer --> Bind incorrect answer event
            else answerObject.GetComponentInChildren<Button>().onClick.AddListener(incorrectCallback);


            // Set Position
            answerObject.GetComponent<RectTransform>().anchoredPosition += Vector2.down * (80 + 60 * i);
        }

    }


    protected void OnModuleCompleted()
    {
        UnfocusModule();
        showHideButton.interactable = false;
        ModuleCompleted?.Invoke();
    }
}
