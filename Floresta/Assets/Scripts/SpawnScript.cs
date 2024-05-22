using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnScript : MonoBehaviour
{
    [Range(0,10)]
    public float raio;

    public Image sombra;

    public GameObject terra;
    public float distanciaTerra;
    public GameObject semente;
    public float distanciaSemente;
    public GameObject plantaPreFab;

    public bool taComTerra;
    public bool taComSemente;

    public bool ocupado;

    private void Awake()
    {
        terra = GameObject.Find("Terra");
        semente = GameObject.Find("Semente");
    }
    void Start()
    {
        
    }

    void Update()
    {
        CalcularDistancias();
        AlterarImagem();
    }

    void CalcularDistancias()
    {
        distanciaTerra = Vector3.Distance(transform.position, terra.transform.position);
        distanciaSemente = Vector3.Distance(transform.position, semente.transform.position);

        if(distanciaSemente <= raio && Input.GetKeyDown(KeyCode.Mouse0) && ocupado == false)
        {
            taComTerra = true;
        }

        if(distanciaTerra <= raio && Input.GetKeyDown(KeyCode.Mouse0) && ocupado == false)
        {
            taComSemente = true;
        }

        if(taComSemente == true && taComTerra == true)
        {
            GameObject planta = Instantiate(plantaPreFab, transform.position, transform.rotation);
            taComSemente = false;
            taComTerra = false;
        }
    }

    void AlterarImagem()
    {
        if (taComSemente == true && taComTerra == false)
        {
            sombra.color = Color.red;
        }
        else if (taComSemente == false && taComTerra == true)
        {
            sombra.color = Color.blue;
        }
        else if(ocupado == true)
        {
            sombra.color = Color.green;
        }
        else
        {
            sombra.color = Color.white;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raio);
    }
}
