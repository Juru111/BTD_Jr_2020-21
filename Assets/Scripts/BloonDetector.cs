using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloonDetector : MonoBehaviour
{
    public bool bloonInRange { private set; get; } = false;

    private void FixedUpdate()
    {
        //wartość ta jest nadpisywwana w OnTriggerStay gdyby balon był w zasięgu
        bloonInRange = false;
    }

    private void OnTriggerStay2D()
    {
        //zmiana na false jest w FixedUpdate
        bloonInRange = true;
    }

    private void Update()
    {
        //Debug.Log(bloonInRange);
    }
}
