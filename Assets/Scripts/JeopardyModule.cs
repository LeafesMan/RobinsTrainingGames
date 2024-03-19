/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 3/5/24
 * 
 * Desc: Displays a panel of questions allowing the player to attempt the questions in any order,
 *      upon selecting a question it will be displayed and the player forced to answer the question, 
 *      right or wrong they will be directed back to the Question Panel where there accumulated points will be displayed, 
 *      - This continues until all questions are answered
 *      - Completed questions should be clearly unclickable
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class JeopardyModule : QuestionModule
{
    public GameObject jeopardyButtonTemplate;
    public GameObject jeopardyTitleTemplate;

    [Header("Categories")]
    [SerializeField] Category[] categories;
    [Header("Scoring")]
    public GameObject scoreText;
    public int basePoints = 100;      // Base points for a question
    public int pointIncrement = 25;  // Extra points for harder questions
    public int requiredPoints = 1000;     // Required points to pass
    private int totalPoints;
    int TotalPoints {  // The Total accumulated points
        get { return totalPoints; }
        set { totalPoints = value; SetText(scoreText, "Score: " + TotalPoints + "/" + requiredPoints); } 
    } 

    [Header("Object Refs")]
    public GameObject jeopardyPanel;
    public GameObject questionPanel;
    public GameObject passedPanel;
    public GameObject failedPanel;

    public Button continueButton;

    

    [System.Serializable]
    struct Category
    {
        public string title;
        public QuizQuestion[] questions;
    }


    private void Start()
    {
        int numQuestions = categories[0].questions.Length;
        for (int i = 1; i < categories.Length; i++)
            if (numQuestions != categories[i].questions.Length)
                throw new System.Exception("Jeopardy FAILED: Uneven category lengths!");

        TotalPoints = 0;

        CreateJeopardyButtons();
        continueButton.onClick.AddListener(() => StartCoroutine(Continue()));
    }

    /// <summary>
    /// Handles continue pressed in two cases:
    /// - Completed Jeopardy
    /// - Question Done
    /// </summary>
    private IEnumerator Continue()
    {
        continueButton.interactable = false;
        animator.CrossFade("TransitionIn", 0);
        animator.Update(0); // Updates the current animator state so that we can read anim duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Question Done --> Hide Question Panel, show Jeopardy Panel
        if (!jeopardyPanel.activeSelf)
        {
            resultText.gameObject.SetActive(false);
            continueButton.interactable = DidAnswerAllQuestions();
            DestroyQuestion();
            jeopardyPanel.SetActive(true);
        }
        // Completed Jeopardy --> Do something based on score
        else
        {
            jeopardyPanel.SetActive(false);

            if(totalPoints >= requiredPoints) passedPanel.SetActive(true);
            else failedPanel.SetActive(true);

            Destroy(continueButton.gameObject);
        }

        // Transition Out
        animator.CrossFade("TransitionOut", 0);
        animator.Update(0); // Updates the current animator state so that we can read anim duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void CreateJeopardyButtons()
    {
        Vector2 panelSize = jeopardyPanel.GetComponent<RectTransform>().sizeDelta;
        Vector2 initPos = jeopardyPanel.GetComponent<RectTransform>().anchoredPosition - panelSize / 2;


        float xOffset = 0.5f * panelSize.x / categories.Length; 
        
        foreach(Category category in categories)
        {   // Reset points and yOffset for this category
            int points = basePoints;
            float yOffset = 0.5f * panelSize.y / (categories[0].questions.Length + 1);

            GameObject categoryText = Instantiate(jeopardyTitleTemplate, jeopardyPanel.transform);
            categoryText.GetComponent<RectTransform>().anchoredPosition = initPos + new Vector2(xOffset, panelSize.y - 0.5f * panelSize.y / (categories[0].questions.Length + 1));
            SetText(categoryText, category.title);

            foreach (QuizQuestion question in category.questions)
            {   // Create Button, set its position, callback, and text
                GameObject jeopardyButton = Instantiate(jeopardyButtonTemplate, jeopardyPanel.transform);
                SetText(jeopardyButton, points.ToString());
                jeopardyButton.GetComponent<RectTransform>().anchoredPosition = initPos + new Vector2(xOffset, yOffset);
                jeopardyButton.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowQuestion(question, jeopardyButton)));

                // Adjust points and yOffset for next question
                yOffset += panelSize.y / (category.questions.Length + 1);
                points += pointIncrement;
            }

            // Adjust xOffset for next category
            xOffset += panelSize.x / categories.Length;
        }
    }


    public bool DidAnswerAllQuestions()
    {   // If find any active Jeoprdy Buttons return false
        foreach(Transform t in jeopardyPanel.transform)
        {
            Button b = t.GetComponent<Button>();
            if (b != null && b.interactable) return false;
        }

        // Otherwise all questions are answered --> return true
        return true;
    }
    private void DestroyQuestion()
    {
        foreach (Transform t in questionPanel.transform)
            Destroy(t.gameObject);
    }


    private IEnumerator ShowQuestion(QuizQuestion question, GameObject button)
    {
        // Transition Out
        animator.CrossFade("TransitionIn", 0);
        animator.Update(0); // Updates the current animator state so that we can read anim duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);


        // Disable Jeopardy Panel
        jeopardyPanel.SetActive(false);

        // Both callbacks disable answers buttons, enable continue button, and show a result
        // Correct Callback gives points 
        UnityAction correctCallback = () => 
        { 
            DisableButtons(questionPanel); 
            ShowResultText(true); 
            TotalPoints += int.Parse(button.transform.GetChild(0).GetComponent<Text>().text);
            
            continueButton.interactable = true;

            Button b = button.GetComponent<Button>();
            b.interactable = false;
            ReplaceButtonDisableColor(b, Color.green);
        };
        UnityAction incorrectCallback = () => 
        {
            DisableButtons(questionPanel);
            ShowResultText(false); 

            continueButton.interactable = true;

            Button b = button.GetComponent<Button>();
            b.interactable = false;
            ReplaceButtonDisableColor(b, Color.red);
        };


        void ReplaceButtonDisableColor(Button button, Color color)
        {
            var newButtonColors = button.colors;
            newButtonColors.disabledColor = color;

            button.colors = newButtonColors;
        }

        // Create UI for this question, pass in the callbacks
        CreateQuizQuestionUI(question, correctCallback, incorrectCallback);


        // Transition Out
        animator.CrossFade("TransitionOut", 0);
        animator.Update(0); // Updates the current animator state so that we can read anim duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }

}
