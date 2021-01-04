using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopIcon : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 0;
    private void OnEnable()
    {
        StartCoroutine(ShowPoP());
    }

    protected virtual IEnumerator ShowPoP()
    {
        transform.Rotate(0f, 0f, Random.Range(0f, 360.0f));
        yield return new WaitForSeconds(lifeTime);
        GameBox.instance.poolingMenager.ReturnPopIcon(gameObject);
    }
}
