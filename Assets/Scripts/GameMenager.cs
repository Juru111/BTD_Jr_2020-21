using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenager : MonoBehaviour
{
    private PoolingMenager poolingMenager;
    [SerializeField]
    private Vector3 startPoint;
    

    void Start()
    {
        poolingMenager = FindObjectOfType<PoolingMenager>();
        StartCoroutine(ConstantSpawn());
    }
    void Update()
    {
        
    }
    IEnumerator ConstantSpawn()
    {
        for (int i = 0; i < 20; i++)
        {
            //poolingMenager.SummonBloon((BloonTypes)1, startPoint);
            poolingMenager.SummonBloon(BloonTypes.Red, startPoint);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
