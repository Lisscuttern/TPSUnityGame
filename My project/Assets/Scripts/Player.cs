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
    [SerializeField] private FloatingJoystick m_floatingJoystick;
    [SerializeField] private float m_speed;
    [SerializeField] private GameObject[] bullet;

    #endregion

    private GameObject targetBullet;

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// This function help for player move on the ground
    /// </summary>
    private void Movement()
    {
        m_rb.velocity = new Vector3(
            m_floatingJoystick.Horizontal * m_speed,
            m_rb.velocity.y,
            m_floatingJoystick.Vertical * m_speed
        );
    }

    /// <summary>
    /// this function help for collect bullet from ground
    /// </summary>
    private void BulletCollect()
    {
        bullet[0] = targetBullet;
        targetBullet.transform.parent = transform;
        targetBullet.transform.DOLocalJump(new Vector3(0, 1, 0),1f,1,0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (bullet[0] != null)
                return;
            targetBullet = other.gameObject.GetComponent<BulletComponent>().gameObject;
            BulletCollect();

            // bullet[0] = other.gameObject;
            //
            // other.transform.parent = transform;
            // other.gameObject.transform.DOLocalJump(new Vector3(0, 1, 0),1f,1,0.5f);
        }
    }

    /// <summary>
    /// This function help for fire the bullet 
    /// </summary>
    private void Fire()
    {
        
    }
}