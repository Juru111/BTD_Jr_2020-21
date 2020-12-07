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
            Debug.Log("zapisyje taką kolejkę: " + type.name.ToString());
            bloonTypesDictionary.Add(type.name.ToString(), bloonPool);
        }
    }

    public void SummonBloon(BloonTypes name, Vector3 position)
    {
        Debug.Log("próbuję zespawnować balon typu: " + name.ToString());
        Debug.Log("Kolejka powyżsego balonu z Dictionary ma: " + bloonTypesDictionary[name.ToString()].Count);
            if (bloonTypesDictionary[name.ToString()].Count > 0)
            {
                GameObject bloonToSummon = bloonTypesDictionary[name.ToString()].Dequeue();
                bloonToSummon.SetActive(true);
                bloonToSummon.transform.position = position;
            }
            else //jeśli jest za mało obiektów w pamięci
            {
                GameObject bloonToSummon = Instantiate(bloonTypeInfos[(int)name - 1].prefab, bloonsAnchor);
                bloonToSummon.transform.position = position;
                for (int i = 0; i < 30; i++)
                {
                    GameObject bloon = Instantiate(bloonTypeInfos[(int)name - 1].prefab, bloonsAnchor);
                    bloon.SetActive(false);
                    bloonTypesDictionary[name.ToString()].Enqueue(bloon);
                }
        }

    }

    public void ReturnBloon(GameObject bloon)
    {
        //[metoda dla samo odkładania]
    }
}
