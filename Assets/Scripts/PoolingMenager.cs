using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingMenager : MonoBehaviour
{
    [System.Serializable]
    public class BloonTypeInfo
    {
        public BloonTypes name;
        public GameObject prefab;
        public int startSize;
    }
    [SerializeField]
    private Transform bloonsAnchor;
    [SerializeField]
    private List<BloonTypeInfo> bloonTypeInfos;
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> bloonTypesDictionary;
    

    // Start is called before the first frame update
    void Awake()
    {
        bloonTypesDictionary = new Dictionary<string, Queue<GameObject>>();

        //Wypełnianie wszystkich pool-i początkową liczbą obiektów
        foreach (BloonTypeInfo type in bloonTypeInfos)
        {
            Queue<GameObject> bloonPool = new Queue<GameObject>();

            for(int i = 0; i < type.startSize; i++)
            {
                GameObject bloon = Instantiate(type.prefab, bloonsAnchor);
                bloon.SetActive(false);
                bloonPool.Enqueue(bloon);
            }
            //Debug.Log("zapisyje taką kolejkę: " + type.name.ToString());
            bloonTypesDictionary.Add(type.name.ToString(), bloonPool);
        }
    }

    public void SummonBloon(BloonTypes bloonName, int layersLeft, Vector3 position,
                            int myNextWaypoint, float distanceToWaypoint, bool isCammo, bool isRegrow)
    {
        
        if (bloonTypesDictionary[bloonName.ToString()].Count > 0)
        {
            GameObject bloonToSummon = bloonTypesDictionary[bloonName.ToString()].Dequeue();
            bloonToSummon.SetActive(true);

            if(bloonToSummon.TryGetComponent<Bloon>(out Bloon bloonComponent))
            {
                bloonComponent.SetMe(bloonName, layersLeft, position, myNextWaypoint, distanceToWaypoint, isCammo, isRegrow);
            }
            else
            { Debug.LogError(bloonToSummon + " nie posiada komponentu Bloon!"); }
                
        }
        else //jeśli jest za mało obiektów w pamięci
        {
            //tworzy jeden na aktualne potzrebę
            GameObject bloonToSummon = Instantiate(bloonTypeInfos[(int)bloonName - 1].prefab, bloonsAnchor);

            if (bloonToSummon.TryGetComponent<Bloon>(out Bloon bloonComponent))
            {
                bloonComponent.SetMe(bloonName, layersLeft, position, myNextWaypoint, distanceToWaypoint, isCammo, isRegrow);
            }
            else
            { Debug.LogError(bloonToSummon + " nie posiada komponentu Bloon!"); }

            //oraz dospawnowuje w Kolejkę zapas
            for (int i = 0; i < 30; i++)
            {
                GameObject bloon = Instantiate(bloonTypeInfos[(int)bloonName - 1].prefab, bloonsAnchor);
                bloon.SetActive(false);
                bloonTypesDictionary[bloonName.ToString()].Enqueue(bloon);
            }
        }

    }

    public void ReturnBloon(GameObject bloon, BloonTypes bloonName)
    {
        bloonTypesDictionary[bloonName.ToString()].Enqueue(bloon);
        bloon.SetActive(false);
    }
}
