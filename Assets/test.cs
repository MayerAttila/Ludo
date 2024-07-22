using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine("asd");
    }
    public IEnumerable asd() 
    {
        Debug.Log(Time.frameCount);
        yield return null;
        Debug.Log(Time.frameCount);
    }

}
