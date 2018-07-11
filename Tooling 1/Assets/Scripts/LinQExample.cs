using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LinQExample : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Objects;

    void Update()
    {
        // Pour trouver le plus près
        GameObject NearestGO = m_Objects
            .OrderBy(Object => Vector3.Distance(transform.position, Object.transform.position))
               .FirstOrDefault();
        //Debug.Log(NearestGO.name);


        // Pour trouver le plus éloigné
        GameObject FurthestGO = m_Objects
            .OrderBy(Object => Vector3.Distance(transform.position, Object.transform.position))
            .LastOrDefault();
        //Debug.Log(FurthestGO.name);


        // Pour trouver le plus éloigné 2
        GameObject FurthesttGO = m_Objects
            .OrderByDescending(Object => Vector3.Distance(transform.position, Object.transform.position))
            .FirstOrDefault();
        //Debug.Log(FurthesttGO.name);


        // Pour trouver un élénent à une position X
        GameObject SecondGO = m_Objects
            .OrderByDescending(Object => Vector3.Distance(transform.position, Object.transform.position))
            .Skip(1)
            .FirstOrDefault();
        //Debug.Log(SecondGO.name);

        // Pour trouver les X premiers éléments de la liste
        GameObject[] FirstTwoGO = m_Objects
            .OrderByDescending(Object => Vector3.Distance(transform.position, Object.transform.position))
            .Take(2)
            .ToArray();
		//Debug.Log(FirstTwoGO);

        // Comment filtrer les éléments selon une condition
        GameObject[] FilteredGO = m_Objects
            .Where(Object => Object.name.Contains("Cube"))
            .ToArray();
        /*foreach (GameObject GO in FilteredGO)
        {
            Debug.Log(GO.name);
        }*/

		// Est-ce que je contien au moins un élément selon la condition
		bool HasCube = m_Objects
			.Any(go => go.name == "Cube3");
		//Debug.Log(HasCube);

		// Comment sortir les occurences uniques d'une liste (1, 2, 3, 4, etc.)
		List<int> NonSenseInts = new List<int>() {1, 2, 3, 1, 2, 3, 1, 2, 3};
		int[] DistinctInts = NonSenseInts
			.Distinct()
			.ToArray();
		foreach (int i in DistinctInts)
        {
            Debug.Log(i);
        }



        

        //Debug.Log(GetNearestGameObject().name);
    }





    /*private GameObject GetNearestGameObject()
	// Ceci est la méthode longue pour sort !!!!!!!!!!!!!!!!
	{
		float minDistance = Mathf.Infinity;
		GameObject ClosestGO = null;

		foreach(GameObject GO in m_Objects)
		{
			float distance = Vector3.Distance(transform.position, GO.transform.position);
			if(distance < minDistance)
			{
				 minDistance = distance;
				 ClosestGO = GO;
			}
		}
		return ClosestGO;
	}*/

}
