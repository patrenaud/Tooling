using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuizmosExample : MonoBehaviour
{
	public MeshFilter m_MeshFilter;
	public Transform m_Target;
    public Vector3 m_CubeSize = Vector3.one;
	public float m_SphereRadius = 1f;

	public float m_FOV = 60f;
	public float m_MinRange = 1f;
	public float m_MaxRange = 100f;
	public float m_Aspect = 0.75f;

    private void OnDrawGizmosSelected()
    {
        Color gizmocolor = Color.cyan;
        gizmocolor.a = 0.25f; // Permet d'être translucide !!!
        Gizmos.color = gizmocolor;

        Gizmos.DrawCube(transform.position, m_CubeSize);

        Gizmos.DrawWireCube(transform.position + transform.forward * 5f, m_CubeSize);

        if (m_Target != null)
        {
			Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, m_Target.position);
        }

		if (m_MeshFilter != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawMesh(m_MeshFilter.sharedMesh, m_Target.position + 5f * transform.right, Quaternion.identity);
		}

		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, transform.up * 100f);


		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position + transform.forward * 2f, m_SphereRadius);

		Gizmos.color = Color.blue;
		Gizmos.matrix = transform.localToWorldMatrix; // Permet de rotate de lui-même
		Gizmos.DrawFrustum(Vector3.zero, m_FOV, m_MaxRange, m_MinRange,m_Aspect );

    }
}