using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonthsEnum { Undefined, January, February, March, April, May, June, July, August, September, October, November, December}

public class Practice : MonoBehaviour
{
    [SerializeField] private MonthsEnum chosenMonth;

    private void Start()
    {
        Debug.Log((MonthsEnum)Random.Range(1,13));

    }
}
