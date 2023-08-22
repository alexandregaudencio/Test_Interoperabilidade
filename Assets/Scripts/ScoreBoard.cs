using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private gatinho gatinho;
    [SerializeField] private TMP_Text text_score;
    private GatinhoData data => gatinho.GatinhoData;



    private void Update()
    {
        text_score.SetText(data.Score.ToString());
    }
}
