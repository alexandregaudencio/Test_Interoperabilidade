using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gafanhoto : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDuration;
    //[SerializeField] private float jumpHeight = 3;
    [SerializeField] private Ease ease;

    private Vector3 initialPosition;

    private void Start()
    {
        transform.DOJump(transform.position, jumpPower, 1, jumpDuration).SetEase(ease).SetLoops(100);
        //transform.DOMoveY(transform.position.y + jumpHeight, jumpDuration).SetEase(Ease.OutExpo);
    }


}
