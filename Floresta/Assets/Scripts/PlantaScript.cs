using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlantaScript : MonoBehaviour
{
    [Range(0, 10)]
    public float raio;

    public Image sprite;
    public Transform foco;

    

    public PlayerScript playerScript;

    public GameObject arvorePreFab;
    public float timer = 1;

    public BarraDeVida barraDeVida;
    public float vidaMaxima;
    public float vida;

    public GameObject mao;
    public float distanciaMaoObjeto;

    public GameObject[] drops;
    public DropScript[] dropsScripts;
    public float[] distanciasDrops;

    public GameObject[] spawns;
    public SpawnScript[] spawnsScripts;
    public float[] distanciasSpawns;

    public GameObject regador;
    public float distanciaDoRegador;

    public bool objetoNaMao;
    public bool objetoNoDrop;
    public bool objetoNoSpawn;
    public bool plantaRegada;
    public bool arvoreReflorestada;

    public int indexDrop;
    public int indexSpawn;

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        mao = GameObject.FindGameObjectWithTag("Mao");

        regador = GameObject.FindGameObjectWithTag("Regador");

        drops = GameObject.FindGameObjectsWithTag("Drop");
        distanciasDrops = new float[drops.Length];
        dropsScripts = new DropScript[drops.Length];
        for (int i = 0; i < dropsScripts.Length; i++)
        {
            dropsScripts[i] = drops[i].GetComponent<DropScript>();
        }

        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        distanciasSpawns = new float[spawns.Length];
        spawnsScripts = new SpawnScript[spawns.Length];
        for (int i = 0; i < dropsScripts.Length; i++)
        {
            spawnsScripts[i] = spawns[i].GetComponent<SpawnScript>();
        }

    }
    private void Start()
    {
        objetoNoSpawn = true;
        vida = vidaMaxima;
        foco = Camera.main.transform;
    }

    void Update()
    {
        EstarNoSpawn();
        PegarObjeto();
        LargarObjetoNoDrop();
        SerRegada();
        Pontuar();
        TempoDeVida();
        ApontarSprite();
    }
    void EstarNoSpawn()
    {
        for (int i = 0; i < distanciasSpawns.Length; i++)
        {
            distanciasSpawns[i] = Vector3.Distance(transform.position, spawns[i].transform.position);

            if (distanciasSpawns[i] == 0)
            {
                indexSpawn = i;
                spawnsScripts[indexSpawn].ocupado = true;
            }
        }

    }
    void PegarObjeto()
    {
        distanciaMaoObjeto = Vector3.Distance(transform.position, mao.transform.position);

        if (distanciaMaoObjeto <= raio && Input.GetKeyDown(KeyCode.Mouse0) && playerScript.maosOcupadas == false && objetoNoDrop == false)
        {
            objetoNaMao = true;
            objetoNoSpawn = false;
            spawnsScripts[indexSpawn].ocupado = false;
            playerScript.maosOcupadas = true;
            Debug.Log($"Pegou {gameObject.name}");
        }
        if (objetoNaMao == true)
        {
            transform.position = mao.transform.position;
        }
    }
    void LargarObjetoNoDrop()
    {
        for (int i = 0; i < distanciasDrops.Length; i++)
        {
            distanciasDrops[i] = Vector3.Distance(transform.position, drops[i].transform.position);

            if (distanciasDrops[i] <= dropsScripts[i].raio && Input.GetKeyDown(KeyCode.Mouse0) && objetoNaMao == true && dropsScripts[i].ocupado == false)
            {
                objetoNaMao = false;
                playerScript.maosOcupadas = false;

                Debug.Log($"Largou {gameObject.name}");

                objetoNoDrop = true;
                transform.position = drops[i].transform.position;
            }

            if (distanciasDrops[i] == 0)
            {
                indexDrop = i;
                dropsScripts[indexDrop].ocupado = true;
            }
        }
    }

    void SerRegada()
    {
        distanciaDoRegador = Vector3.Distance(transform.position, regador.transform.position);

        if (distanciaDoRegador <= raio && Input.GetKeyDown(KeyCode.Mouse0) && objetoNoDrop == true)
        {
            plantaRegada = true;
        }

        if (plantaRegada == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameObject Arvore = Instantiate(arvorePreFab, transform.position, transform.rotation);
                plantaRegada = false;
                arvoreReflorestada = true;
                objetoNoDrop = false;
                dropsScripts[indexDrop].ocupado = false;
            }
        }
    }
    void Pontuar()
    {
        if (arvoreReflorestada == true)
        {
            Destroy(gameObject);
        }
    }
    void TempoDeVida()
    {
        vida -= Time.deltaTime;
        if(vida <= 0)
        {
            if(objetoNoSpawn == true)
            {
                spawnsScripts[indexSpawn].ocupado = false;
            }
            else if(objetoNoDrop == true)
            {
                dropsScripts[indexDrop].ocupado = false;
            }
            else if(objetoNaMao == true)
            {
                playerScript.maosOcupadas = false;
            }

            Destroy(gameObject);
        }

        barraDeVida.AlterarBarraDeVida(vida, vidaMaxima);
    }
    void ApontarSprite()
    {
        sprite.transform.LookAt(sprite.transform.position + foco.forward);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raio);
    }
}
