using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    private static QuizManager instance;
    public static QuizManager Instance {get => instance;}
    [SerializeField] public Quiz[] quizzes;
    public int[] answers;

    void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

    void OnEnable()
    {
        answers = new int[quizzes.Length];
    }

    public void SetAnswer(int quizIndex, int answer)
    {
        answers[quizIndex] =  answer;
    }
}
