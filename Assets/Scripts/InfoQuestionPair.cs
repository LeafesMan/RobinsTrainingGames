/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 2/28/24
 * 
 * Desc: Data structure for a pair of generic objects. This allows unity to render a pair in the inspector
 */

[System.Serializable]
public struct InfoQuestionPair
{
    public string info;
    public QuizQuestion quizQuestion;

    public InfoQuestionPair(string info, QuizQuestion question)
    {
        this.info = info; this.quizQuestion= question;
    }
}
