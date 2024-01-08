using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuizUIStatistic : MonoBehaviour
{
    [SerializeField] private QuizUIQuestion answerPrefab;
    [SerializeField] private Transform answerParent;
    [Header("UI")]
    [SerializeField] private TMP_Text quizScoreText;


    void OnEnable()
    {
        ShowQuizAnalytic();
    }

    private void ShowQuizAnalytic()
    {
        int quizScore = 0;
        int totalScore = 0;
        for(int i=0; i<QuizManager.Instance.quizzes.Length; i++)
        {
            Quiz quiz = QuizManager.Instance.quizzes[i];
            int playerAnswer = QuizManager.Instance.answers[i];
            QuizUIQuestion quizUIQuestion = Instantiate(answerPrefab, answerParent);
            quizUIQuestion.SetQuestion(quiz, playerAnswer);

            if(playerAnswer == quiz.correctAnswer)
            {
                quizScore += quiz.score;
            }
            totalScore += quiz.score;
        }
        quizScoreText.text = $"{quizScore}/{totalScore}";
    }
}
