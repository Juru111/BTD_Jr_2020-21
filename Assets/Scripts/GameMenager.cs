using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenager : MonoBehaviour
{
    private Vector3 startPoint;

    [SerializeField]
    private TMP_Text moneyDisplay;
    [SerializeField]
    private int moneyCount;
    [SerializeField]
    private TMP_Text hpDisplay;
    [SerializeField]
    private int hpCount;

    void Awake()
    {
        RefreshMoneyDispaly();
        RefreshHpDispaly();
    }

    void Start()
    {
        startPoint = GameBox.instance.waypoints[0].position;

        //robocze spawnowanie balonów
        StartCoroutine(WIPSpawning());
        //GameBox.instance.PoolingMenager.SummonBloon(BloonTypes.MOAB, 10, startPoint, 0, 0, false, false, null);
    }

    void Update()
    {
        
    }

    public void LoseHp(int hpLost)
    {
        hpCount -= hpLost;
        RefreshHpDispaly();
        if (hpCount <= 0)
        {
            //lose Popup
            Debug.Log("END!");
            hpCount = 0;
        }
    }

    public void changeMoneyBalance(int moneyChange)
    {
        moneyCount += moneyChange;

        //może robić to rzadziej gdy będzie się to działo często
        RefreshMoneyDispaly();
    }

    private void RefreshMoneyDispaly()
    {
        moneyDisplay.text = moneyCount.ToString();
    }

    private void RefreshHpDispaly()
    {
        hpDisplay.text = hpCount.ToString();
    }

    IEnumerator WIPSpawning()
    {
        for (int i = 1; i < 11; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                BloonTypes bloonType = (BloonTypes)i;
                GameBox.instance.PoolingMenager.SummonBloon(bloonType, (int)bloonType % 100, startPoint, 0, 0, false, false, null);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(60);
        for (int i = 1; i < 100; i++)
        {
            BloonTypes bloonType = (BloonTypes)Random.Range(1, 10);
            GameBox.instance.PoolingMenager.SummonBloon(bloonType, (int)bloonType % 100, startPoint, 0, 0, false, false, null);
            yield return new WaitForSeconds(0.2f);
        }

    }

    public void DebugLogging()
    {
        Debug.Log("Debug Logging from GameMenager");
    }
}
