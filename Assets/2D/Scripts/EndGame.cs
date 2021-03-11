using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    
    public Text final_score_text;
    //StartGame startgame;
    void Start()
    {
        
        //startgame = GetComponent<StartGame>();
        //Debug.Log(startgame.score);
        final_score_text = GameObject.Find("Canvas/Final Score").GetComponent<Text>();
        final_score_text.text = "Score: " + Data.score;
        //Debug.Log(startgame.score);
        //startgame = gameObject.GetComponent<StartGame>();
    }
}
