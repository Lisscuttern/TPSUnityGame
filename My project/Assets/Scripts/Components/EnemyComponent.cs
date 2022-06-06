using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class EnemyComponent : MonoBehaviour
{
    #region SerializeFields
    
    [SerializeField] private Vector3 movePos;
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private Transform m_player;
    
    #endregion

    #region Private Fields

    private GameObject targetBullet;
    private float distance;

    #endregion
    
    
    private void Start()
    {
        movePos = new Vector3(Random.Range(-9f, 9f), 1, Random.Range(-9, 9f));
    }

    private void FixedUpdate()
    {
        Fire();
    }

    private void Update()
    {
            Movement();
    }

    /// <summary>
    /// This function help for enemy move on the ground with random positions
    /// </summary>
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
    
    
    /// <summary>
    /// this function help for collect bullet from ground
    /// </summary>
    private void BulletCollect()
    {
        bullet[0] = targetBullet;
        targetBullet.transform.parent = transform;
        targetBullet.transform.DOLocalJump(new Vector3(0, 1.5f, 0),1f,1,0.5f);
    }
    
    /// <summary>
    /// This function help for fire the bullet 
    /// </summary>
    private void Fire()
    {
        if (bullet[0] == null)
            return;
        distance = Vector3.Distance(transform.position, m_player.position);
        if (distance > 10f)
            return;

        if (DOTween.IsTweening("BulletFire"))
            return;
        
        if (DOTween.IsTweening("BulletCollect"))
            return;
        
        bullet[0].gameObject.tag = "FireBullet";
        Sequence sequence = DOTween.Sequence();
        sequence.Join(bullet[0].transform.DOMove(m_player.position, .2f)).OnComplete(() =>
        {
        });
        
        Destroy(bullet[0].gameObject,.5f);
        
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