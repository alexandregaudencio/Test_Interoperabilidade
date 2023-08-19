


using UnityEngine;

[System.Serializable]
public class GatinhoData 
{
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private bool fliped = false;

    public Vector3 Position { get => position; set => position = value; }
    public bool Fliped { get => fliped; set => fliped = value; }
}
