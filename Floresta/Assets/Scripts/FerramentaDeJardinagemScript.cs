using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FerramentaDeJardinagemScript : MonoBehaviour
{
    [Range(0, 10)]
    public float raio;

    public GameObject origem;
    public GameObject mao;
    public GameObject player;
    public PlayerScript playerScript;
    public float distanciaMaoObjeto;
    public bool objetoNaMao;

    public Image sprite;
    public Transform foco;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        origem = GameObject.Find($"Origem{gameObject.name}");
        mao = GameObject.FindGameObjectWithTag("Mao");
    }

    private void Start()
    {
        foco = Camera.main.transform;
    }
    void Update()
    {
        distanciaMaoObjeto = Vector3.Distance(transform.position, mao.transform.position);


        if (distanciaMaoObjeto <= raio && Input.GetKeyDown(KeyCode.Mouse0) && !playerScript.maosOcupadas)
        {
            objetoNaMao = true;
            playerScript.maosOcupadas = true;
            Debug.Log($"Pegou {gameObject.name}");
        }

        if (objetoNaMao == true)
        {
            transform.position = mao.transform.position;
        }

        if (transform.position != origem.transform.position && Input.GetKeyDown(KeyCode.Mouse1))
        {
            objetoNaMao = false;
            playerScript.maosOcupadas = false;
            Debug.Log($"Largou {gameObject.name}");

        }

        if (objetoNaMao == false)
        {
            transform.position = origem.transform.position;
        }

        ApontarSprite();
    }
    

    void ApontarSprite()
    {
        sprite.transform.LookAt(sprite.transform.position + foco.forward);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, raio);
    }
}
