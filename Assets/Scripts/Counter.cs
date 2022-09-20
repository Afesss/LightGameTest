using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text counter;

    public void SetCounter(byte count)
    {
        counter.text = count.ToString();
    }
}
