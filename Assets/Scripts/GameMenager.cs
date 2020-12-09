using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenager : MonoBehaviour
{
    private PoolingMenager poolingMenager;
    private Vector3 startPoint;
    

    void Start()
    {
        poolingMenager = FindObjectOfType<PoolingMenager>();
        startPoint = GameBox.instance.waypoints[0].position;
        
        //robocze spawnowanie balonów
        StartCoroutine(WIPSpawning());
        
    }
    void Update()
    {
        
    }
    IEnumerator WIPSpawning()
    {
        for (int i = 1; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                BloonTypes bloonType = (BloonTypes)i;
                poolingMenager.SummonBloon(bloonType, (int)bloonType % 100, startPoint, 0, 0, false, false, null);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(3);
        }

    }
}
