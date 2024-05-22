using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{

    public Image vidaRestante;
    public Transform foco;
    private void Awake()
    {
        foco = Camera.main.transform;
    }
    void Update()
    {
        vidaRestante.transform.LookAt(vidaRestante.transform.position + foco.forward);
    }
    public void AlterarBarraDeVida(float vidaAtual, float vidaMaxima)
    {
        vidaRestante.fillAmount = vidaAtual / vidaMaxima;
    }
}
