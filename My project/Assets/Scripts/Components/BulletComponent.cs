using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    #region SerializeFields
    
    [SerializeField] private float m_fireSpeed;
    
    #endregion

    /// <summary>
    /// This function get fire bullet speed
    /// </summary>
    /// <returns>m_fireSpeed</returns>
    public float GetFireSpeed()
    {
        return m_fireSpeed;
    }
}
