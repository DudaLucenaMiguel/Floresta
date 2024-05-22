using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropScript : MonoBehaviour
{
    [Range(0, 10)]
    public float raio;
    public Image sombra;

    public bool ocupado;

    
    void Start()
    {
        ocupado = false;
    }

    void Update()
    {
        if(ocupado == true)
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raio);
    }
}
