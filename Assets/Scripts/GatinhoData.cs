


using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GatinhoData
{
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private bool fliped = false;
    [SerializeField] private float score;
    [SerializeField] private Vector3 bearPosition = Vector3.zero;


    public event Action<float> ScoreChange;
    public Vector3 Position { get => position; set => position = value; }
    public bool Fliped { get => fliped; set => fliped = value; }
    public float Score => score;

    public Vector3 BearPosition { get => bearPosition; set => bearPosition = value; }

    public List<Vector2> GafanhotoPositions; 


    public void SetScore(int value)
    {
        score = value;
        ScoreChange?.Invoke(score);

    }
    public void IncreaseScore()
    {
        score++;
        ScoreChange?.Invoke(score);

    }

    public void AddGafanhotoPosition(Vector2 position)
    {
        GafanhotoPositions.Add(position);
    }

    public void storageData(Vector3 position, bool fliped)
    {
        this.position = position;
        this.fliped = fliped;
    }


}
