using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class EnemyComponent : MonoBehaviour
{
    
    [SerializeField] private float moveTime;
    [SerializeField] private Vector3 movePos;

    private void Start()
    {
        movePos = new Vector3(Random.Range(-9f, 9f), 1, Random.Range(-9, 9f));
    }

    private void Update()
    {
            Movement();
    }

    private void Movement()
    {
        if (DOTween.IsTweening("EnemyMove"))
            return;
        Sequence sequence = DOTween.Sequence();
        
        sequence.Join(transform.DOMove(movePos, 2.5f)).OnComplete(() =>
        {
            movePos = new Vector3(Random.Range(-9f, 9f), 1, Random.Range(-9, 9f));
        });
        
        sequence.SetId("EnemyMove");
        sequence.Play();
    }
}