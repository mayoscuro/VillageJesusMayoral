using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats: MonoBehaviour {
    public int rango; //Rango que el jugador tiene en la aldea.
    public int vida; //Vida que tiene el jugador
    public int resistencia; //Puntos de resistencia del jugador.
    public Slider sliderVida;
    public Slider sliderResistencia;
    public Ecosystem ecosistem;
    public GameObject panelGameOver;
    public Text textoCausa;
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
            gameOver("Muerte");
        }
    }
    private void Update()
    {
        if (playerController.esquivar && !esquivando) {
            esquivando = true;
            restandoResist = true;
            restarResistencia(25);
        }
        if (ecosistem.obtenerDatosDelGrafo("Lobo") == null) {
            gameOver("Lobo");
        }
        if (ecosistem.obtenerDatosDelGrafo("Ciervo") == null)
        {
            gameOver("Ciervo");
        }
        if (ecosistem.obtenerDatosDelGrafo("Zorro") == null)
        {
            gameOver("Zorro");
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

    void gameOver(string causa) {
        panelGameOver.SetActive(true);
        playerController.enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        if (causa == "Zorro") {
            textoCausa.text = "Todos los ejemplares de Zorros han muerto";
        } else if (causa == "Lobo") {
            textoCausa.text = "Todos los ejemplares de Lobos han muerto";
        } else if (causa == "Ciervo") {
            textoCausa.text = "Todos los ejemplares de Ciervos han muerto";
        } else if (causa == "Muerte") {
            textoCausa.text = "Has muerto";
        }
    }

    public void exitToMainMenuButton() {
        SceneManager.LoadScene("EscenaMenuPrincipal", LoadSceneMode.Single);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lobo" && other.gameObject.GetComponentInParent<AnimalsBasicIA>().atacando) {
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
