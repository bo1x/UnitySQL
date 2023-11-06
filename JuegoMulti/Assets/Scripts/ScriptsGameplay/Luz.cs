using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luz : MonoBehaviour
{
    Vector3 rot = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("tiempoTranscurrido");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator tiempoTranscurrido()
    {
        while (true)
        {
            RotarLuz();
            yield return new WaitForSeconds(1);
        }
    }

    void RotarLuz()
    {
        rot.x = 0.5f;
        transform.Rotate(rot, Space.World);
    }
}
