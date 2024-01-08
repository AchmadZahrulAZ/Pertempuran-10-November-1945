using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string sceneName;
    public int sceneIndex;
    public int[] answeredQuestions;

    public GameData(string name, int index, int[] answeredQuestions)
    {
        sceneName = name;
        sceneIndex = index;
        this.answeredQuestions = answeredQuestions;
    }
}