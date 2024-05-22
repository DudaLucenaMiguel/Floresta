using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ArvoreScript : MonoBehaviour
{
    public float vida = 5;
    public GerenciaScript gerencia;

    private void Awake()
    {
        gerencia = GameObject.Find("Gerencia").GetComponent<GerenciaScript>();
    }
    void Update()
    {
        vida -= Time.deltaTime;
        if(vida<=0)
        {
            gerencia.pontos++;
            Destroy(gameObject);
        }
    }
}
