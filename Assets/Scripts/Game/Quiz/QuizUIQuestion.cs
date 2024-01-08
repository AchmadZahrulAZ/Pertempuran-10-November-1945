using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUIQuestion : MonoBehaviour
{
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text rightText;


    public void SetQuestion(Quiz quiz, int playerAnswer)
    {
        Debug.Log($"PlayerAnswer: {playerAnswer}, quizAnswersLength:{quiz.answers.Length}");
        leftText.text = quiz.question;
        if(playerAnswer == quiz.correctAnswer)
        {
            leftText.text += "\n<color=#20841F>";
            rightText.text = $"<color=#20841F> +{quiz.score}";
        }
        else
        {
            leftText.text += "\n<color=#841F2C>";
            rightText.text = $"<color=#841F2C> 0";
        }
        leftText.text += quiz.answers[playerAnswer];
    }
}