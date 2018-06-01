using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats: MonoBehaviour {
    public int rango; //Rango que el jugador tiene en la aldea.
    public int vida; //Vida que tiene el jugador
    public int resistencia; //Puntos de resistencia del jugador.
    public Slider sliderVida;
    public Slider sliderResistencia;
    private bool atacado = false;
    private bool restandoResist;
    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter playerController;
    private bool esquivando = false;
    private void Start()
    {
        sliderVida.maxValue = vida;
        sliderVida.value = vida;
        sliderResistencia.maxValue = resistencia;
        sliderResistencia.value = resistencia;
        playerController = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
    }

    private void restarVida(int daño) {
        Debug.Log(daño);
        vida = vida - daño;
        sliderVida.value = vida;
        if (vida <= 0) {
            gameOver();
        }
    }
    private void Update()
    {
        if (playerController.esquivar && !esquivando) {
            esquivando = true;
            restandoResist = true;
            restarResistencia(25);
        }
    }
    public void restarResistencia(int valor) {
        resistencia = resistencia - valor;
        sliderResistencia.value = resistencia;
        
        StartCoroutine("recuperarResistenciaContiempo");
    }

    IEnumerator recuperarResistenciaContiempo() {
        restandoResist = false;
        yield return new WaitForSeconds(3);
        if (restandoResist)
        {
            yield return null;
        }
        else {
            InvokeRepeating("rellenarResistencia",0,0.01f);
        }
    }

    void rellenarResistencia() {
        sliderResistencia.value += 0.1f;
        if (sliderResistencia.value == sliderResistencia.maxValue) {
            CancelInvoke();
            esquivando = false;
        }
    }

    void gameOver() {
        //Animación del personaje cayendo al suelo derrotado.
        //Aparece la pantalla de game over con las estadisticas del juego y las letras ¿Volver a intentar?
        //si dice que si, empieza el nivel de 0, si dice que no, sale al menu inicial.
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LoboObjetoPadre" && other.gameObject.GetComponent<AnimalsBasicIA>().atacando) {
            if (!atacado) {
                atacado = true;
                restarVida(other.gameObject.GetComponentInParent<Animal>().attack);
                GetComponent<Animator>().SetTrigger("Daño");
                StartCoroutine("espera");
            }
        }
    }

    IEnumerator espera() {
        yield return new WaitForSeconds(1);
        atacado = false;
    }
}
