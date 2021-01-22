using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesMenager : MonoBehaviour
{
    private List<GameObject> emptyList;

    private Vector3 startPoint;
    [SerializeField]
    private int currRound = 1;
    private int maxRound;
    [SerializeField]
    private BloonDetector bloonDetector;

    private void Start()
    {
        startPoint = GameBox.instance.waypoints[0].position;
        emptyList = new List<GameObject>();
        maxRound = GameBox.instance.dataBase.rounds.Count - 1;
    }

    public void DoNextRound()
    {
        StartCoroutine(DoRound(currRound));
    }

    private IEnumerator DoRound(int roundIndex)
    {
        GameBox.instance.uIMenager.StartNextRoundOnUI();
        int pieceCount = GameBox.instance.dataBase.rounds[roundIndex].pieces.Count;
        for (int i = 0; i < pieceCount; i++)
        {
            var currPiece = GameBox.instance.dataBase.rounds[roundIndex].pieces[i];

            for (int j = 0; j < currPiece.count; j++)
            {
                GameBox.instance.poolingMenager.SummonBloon(currPiece.bloonName, (int)currPiece.bloonName % 100, startPoint, 0, 0, currPiece.isCammo, 0, emptyList);
                yield return new WaitForSeconds(currPiece.bloonSpaceing / 2 / GiveBloonSpeed(currPiece.bloonName));
            }
            yield return new WaitForSeconds(currPiece.pieceSpaceing / 2 / GiveBloonSpeed(currPiece.bloonName));
        }
        StartCoroutine(TryEndRound());
    }

    IEnumerator TryEndRound()
    {
        yield return new WaitUntil(() => bloonDetector.bloonInRange == false);
        yield return null;
        yield return new WaitUntil(() => bloonDetector.bloonInRange == false);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => bloonDetector.bloonInRange == false);

        if (currRound < maxRound)
        {
            currRound++;
            GameBox.instance.uIMenager.FinishRoundOnUI(currRound, GameBox.instance.dataBase.rounds[currRound].rbeInfo);
            Debug.Log("Ended round " + (currRound - 1) + " Round to start " + currRound);
        }
        else
        {
            GameBox.instance.uIMenager.DoWin();
        }
    }

    //przenieść dane do DataBase-u, dać odnośniki
    private float GiveBloonSpeed(BloonTypes bloonName)
    {
        switch (bloonName)
        {
            case BloonTypes.Red: return 1;
            case BloonTypes.Blue: return 1.4f;
            case BloonTypes.Green: return 1.8f;
            case BloonTypes.Yellow: return 3.2f;
            case BloonTypes.Pink: return 3.5f;
            case BloonTypes.Black: return 1.8f;
            case BloonTypes.White: return 2;
            case BloonTypes.Lead: return 1;
            case BloonTypes.Zebra: return 1.8f;
            case BloonTypes.Rainbow: return 2.2f;
            case BloonTypes.Ceramic: return 2.5f;
            case BloonTypes.MOAB: return 1f;
            default: return 0;
        }
    }

}
