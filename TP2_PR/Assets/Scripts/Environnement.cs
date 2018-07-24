using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environnement : MonoBehaviour
{
    [SerializeField]
    private int m_Health;
    [SerializeField]
    private bool m_Bounds;
    [SerializeField]
    private Color m_Color = Color.white;

    private void OnDrawGizmosSelected()
    {
        if (m_Bounds)
        {
            if (m_Color != null)
            {
                Gizmos.color = m_Color;
            }
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}
