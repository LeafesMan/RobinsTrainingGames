/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 2/28/24
 * 
 * Desc: Struct for Questions  answers
 */


[System.Serializable]
public struct QuizQuestion
{
    public string question;
    public string correctAnswer;
    public string[] incorrectAnswers;
}
