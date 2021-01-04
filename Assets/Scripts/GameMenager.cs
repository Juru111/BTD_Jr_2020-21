using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenager : MonoBehaviour
{

    private Vector3 startPoint;

    void Start()
    {
        startPoint = GameBox.instance.waypoints[0].position;

        //robocze spawnowanie balonów
        StartCoroutine(WIPSpawning());
        //GameBox.instance.PoolingMenager.SummonBloon(BloonTypes.MOAB, 10, startPoint, 0, 0, false, false, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WIPSpawning()
    {
        for (int i = 1; i < 11; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                BloonTypes bloonType = (BloonTypes)i;
                GameBox.instance.poolingMenager.SummonBloon(bloonType, (int)bloonType % 100, startPoint, 0, 0, false, false, null);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(60);
        for (int i = 1; i < 100; i++)
        {
            BloonTypes bloonType = (BloonTypes)Random.Range(1, 10);
            GameBox.instance.poolingMenager.SummonBloon(bloonType, (int)bloonType % 100, startPoint, 0, 0, false, false, null);
            yield return new WaitForSeconds(0.2f);
        }

    }



    public void DebugLogging()
    {
        Debug.Log("Debug Logging from GameMenager");
    }
}
