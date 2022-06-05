using System;
using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    #region SerializeFields
    
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private float m_fireSpeed;

    #endregion


    public float GetFireSpeed()
    {
        return m_fireSpeed;
    }
}
