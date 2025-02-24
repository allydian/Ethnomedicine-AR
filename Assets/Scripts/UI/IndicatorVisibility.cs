using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorVisibility : MonoBehaviour
{
    public GameObject indicatorgyro;

    // Start is called before the first frame update
    void Start()
    {
        indicatorgyro.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
