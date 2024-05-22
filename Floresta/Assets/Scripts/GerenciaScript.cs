using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciaScript : MonoBehaviour
{
    public int pontos;
    public int meta;
    public float tempoDaRodada;
    public float minutosTempo;
    public float segundosTempo;
    public float timer;
    [Range(0,600)]
    public float minutosTimer;
    [Range(0, 60)]
    public float segundosTimer;
    public bool tempoRodando;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pontosText;

    public TextMeshProUGUI tempoText;
    public TextMeshProUGUI metaText;

    public bool Perdeu;
    public bool Ganhou;

    public GameObject painelDeVitoria;
    public GameObject painelDeDerrota;
    public GameObject painelInicial;

    void Start()
    {
        tempoRodando = false;
        Time.timeScale = 0;
        painelDeDerrota.SetActive(false);
        painelDeVitoria.SetActive(false);
        painelInicial.SetActive(true);
    }

    void Update()
    {
        GerenciarJogo();
        GerenciarPaineis();
        Teclado();
        ContarTimer();
        ContarTempo();
    }
    void GerenciarJogo()
    {
        if (tempoRodando == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0 && pontos < meta)
            {
                tempoRodando = false;
                Perdeu = true;
                Debug.Log("Perdeu");

            }
            else if (timer >= 0 && pontos == meta)
            {
                tempoRodando = false;
                Ganhou = true;
                Debug.Log("Venceu");
            }
        }
        else
        {
            timer = tempoDaRodada;
        }


        
        
        
        
        if (meta >= 0)
        {
            metaText.text = $"{meta}";
            pontosText.text = $"Arvores Reflorestadas: {pontos}/{meta}";
        }
        else
        {
            metaText.text = $"0";
            pontosText.text = $"Arvores Reflorestadas: 0/0";
        }
        

    }
    void GerenciarPaineis()
    {
        if(Perdeu == true)
        {
            Time.timeScale = 0;
            painelDeDerrota.SetActive(true);
        }
        else if(Ganhou == true)
        {
            Time.timeScale = 0;
            painelDeVitoria.SetActive(true);
        }
    }
    void ContarTimer()
    {
        minutosTimer = Mathf.FloorToInt(Mathf.FloorToInt(timer) / 60);
        segundosTimer = Mathf.FloorToInt(timer) - minutosTimer * 60;

        if (tempoDaRodada >= 0)
        {
            if (segundosTimer >= 10)
            {
                timerText.text = $"{minutosTimer}:{segundosTimer}";
            }
            else
            {
                timerText.text = $"{minutosTimer}:0{segundosTimer}";
            }
        }
        else
        {
            timerText.text = $"00:00";
        }
        
    }

    void ContarTempo()
    {
        minutosTempo = Mathf.FloorToInt(Mathf.FloorToInt(tempoDaRodada) / 60);
        segundosTempo = Mathf.FloorToInt(tempoDaRodada) - minutosTimer * 60;

        if(tempoDaRodada >= 0)
        {
            if (segundosTimer >= 10)
            {
                tempoText.text = $"{minutosTempo}:{segundosTempo}";
            }
            else
            {
                tempoText.text = $"{minutosTempo}:0{segundosTempo}";
            }
        }
        else
        {
            tempoText.text = $"00:00";
        }
        
    }
    void Teclado()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SomarMeta()
    {
        meta += 1;
    }
    public void SubtrairMeta()
    {
        meta -= 1;
    }
    public void SomarTempo()
    {
        tempoDaRodada += 15;
    }
    public void SubtrairTempo()
    {
        tempoDaRodada -= 15;
    }
    public void IniciarJogo()
    {
        if(meta>0 && tempoDaRodada > 0)
        {
            tempoRodando = true;
            painelInicial.SetActive(false);
            timer = tempoDaRodada;
            Time.timeScale = 1;
        }
       
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
