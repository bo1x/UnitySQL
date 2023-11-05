using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float altura;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PersonajePrincipal");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,altura,player.transform.position.z);
    }
}
