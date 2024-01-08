using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class QuizAgent : MonoBehaviour
{
    [SerializeField] private int quizIndex;
    
    [Header("References")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Button[] answerButton;

    public UnityEvent onQuizDone;

    void OnEnable()
    {
        SetupQuiz();
    }



    private void SetupQuiz()
    {
        Quiz quiz = QuizManager.Instance.quizzes[quizIndex];

        questionText.text = quiz.question;

        for(int i=0; i < quiz.answers.Length; i++)
        {
            answerButton[i].GetComponentInChildren<TMP_Text>().text = quiz.answers[i];
            //answerButton[i].onClick.AddListener(() => AnswerQuiz(i));
        }
    }


    public void AnswerQuiz(int answer)
    {
        QuizManager.Instance.answers[quizIndex] = answer;

        onQuizDone?.Invoke();
    }
}
