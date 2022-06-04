using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private DynamicJoystick m_dynamicJoyStick;
    [SerializeField] private float m_speed;

    #endregion

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (transform.position.x < -9.5f || transform.position.x > 9.5f)
            return;
        
        m_rb.velocity = new Vector3(
            m_dynamicJoyStick.Horizontal * m_speed,
            m_rb.velocity.y,
            m_dynamicJoyStick.Vertical * m_speed
        );
    }
}