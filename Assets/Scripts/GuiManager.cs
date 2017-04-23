using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

    public Text scoreText;
    public Text messageBox;

    private int score = 0;

    public void setScore()
    {
        score += 1;
        messageBox.text = "GOALS: " + score;
    }

    public void SetMessage(Color color, string text)
    {
        messageBox.text = text;
        messageBox.color = color;
    }

}
