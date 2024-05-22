using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    CharacterController CC;
    public float velocidadeDoPlayer = 20;
    Vector3 direcao;
    public float velocidadeDeGiro = 200;
    float smoothTime = 0.05f;
    float currentVelocity;
    float valorDaGravidade = -9.81f;
    Vector3 gravidade;

    public bool maosOcupadas;

    private void Awake()
    {
        CC = GetComponent<CharacterController>();
    }
    
    void Start()
    {
        maosOcupadas = false;
    }

    void Update()
    {
        Movimentar();
    }

    public void Movimentar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direcao = new Vector3(horizontal, 0, vertical);
        CC.Move(direcao * velocidadeDoPlayer * Time.deltaTime);

        gravidade = new Vector3(0, valorDaGravidade, 0);
        CC.Move(gravidade * Time.deltaTime);

        Rotacionar();
    }
    public void Rotacionar()
    {
        if (direcao.magnitude >= smoothTime)
        {
            var targetAngle = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
