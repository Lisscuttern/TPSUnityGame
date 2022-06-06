using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private FloatingJoystick m_floatingJoystick;
    [SerializeField] private float m_speed;
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private Transform m_enemy;
    [SerializeField] private GameObject targetBullet;

    #endregion

    #region Private Fields

    private float distance;
    
    #endregion

    private void FixedUpdate()
    {
        Movement();
        Fire();
    }

    private void Update()
    {
        if (targetBullet != null && distance > 10)
        {
            BulletPosUpdate();
        }
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
        m_rb.velocity.Normalize();
        transform.forward = m_rb.velocity;
    }

    /// <summary>
    /// this function help for collect bullet from ground
    /// </summary>
    private void BulletCollect()
    {
        if (DOTween.IsTweening("BulletCollect"))
            return;
        bullet[0] = targetBullet;
        targetBullet.transform.parent = transform;
        Sequence sequence = DOTween.Sequence();
        sequence.Join(targetBullet.transform.DOLocalJump(new Vector3(0, 1, 0),1f,1,0.5f)).OnComplete(() =>
        {
        });

        sequence.SetId("BulletCollect");
        sequence.Play();
    }

    /// <summary>
    /// This function help for update bullet position
    /// </summary>
    private void BulletPosUpdate()
    {
        targetBullet.transform.localPosition = new Vector3(0, 1, 0);
    }
    
    /// <summary>
    /// This function help for fire the bullet 
    /// </summary>
    private void Fire()
    {
        if (bullet[0] == null)
            return;
        distance = Vector3.Distance(transform.position, m_enemy.position);
        if (distance > 10f)
            return;

        if (DOTween.IsTweening("BulletFire"))
            return;
        
        if (DOTween.IsTweening("BulletCollect"))
            return;

        bullet[0].gameObject.tag = "FireBullet";
        Sequence sequence = DOTween.Sequence();
        sequence.Join(bullet[0].transform.DOMove(m_enemy.position, .2f)).OnComplete(() =>
        {
        });

        Destroy(bullet[0].gameObject, .5f);

        sequence.SetId("BulletFire");
        sequence.Play();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (bullet[0] != null)
                return;
            targetBullet = other.gameObject.GetComponent<BulletComponent>().gameObject;
            BulletCollect();
        }
    }
}