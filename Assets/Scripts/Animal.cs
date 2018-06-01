using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
    [Header("Stats y nombre")]
    public float speed;
    public int vit;
    public int attack;
    public string animalName;
    [HideInInspector]public int numObjetos = 2;
    [Tooltip("Nodo del animal ")]public Nodo nodoAnimal;
    [HideInInspector] public bool animalMuerto = false;

    //Booleanos para controlar el proceso de muerte de los animales
    private bool desvanecer;//Booleano para que cuando el animal muera solo se pueda desbanecer una unica vez.
    private bool animTerminada;//Para que no pase nada más si el animal muere.
    [HideInInspector] public bool muerto;

    [Header("Animaciones")]
    [Tooltip("El animator debe de tener si o si, un booleano llamado 'Muerte'")]public Animator anim;

    
    private SkinnedMeshRenderer meshRenderer;
    [Header("Assets necesarios para desvanecer")]
    public int fadeSpeed = 3;
    private Color matCol;
    private Color newColor;
    private float alfa = 0;

    public List<Collider> colliders;

    // Use this for initialization
    void Start () {
        speed = Random.Range(speed-5, speed + 5); //Es un random entre la velocidades, para que no siempre tengan la misma velocidad.
        
        meshRenderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();//Esta en la posición 1 de los hijos, si se cambia explota.
        if (meshRenderer == null) {
            Debug.Log("Error");
        }

        numObjetos = Random.Range(1, 4);
        
        matCol = meshRenderer.material.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (desvanecer)
        {
            Destroy(gameObject);
        }
    }

    void muerte() {
        anim.SetBool("Muerte", true);
        nodoAnimal.numeroVivos = nodoAnimal.numeroVivos - 1;//Se tiene que restar en uno el numero de vivos.
        nodoAnimal.numeroCaidos = nodoAnimal.numeroCaidos + 1;//Se tiene que aumentar el numero de caidos.

        //Reproducir la animación de muerte y dejar al animal tirado en el suelo:
        animTerminada = true;
        
        //anim.SetTrigger("Muerte");//IMPORTANTE: Poner este booleano en todos los animators de los personajes.
        
        //En estas lineas cambio el rendering mode a FADE, para que se puede hacer el desvanecimiento del animal tras los 10 s de espera.
        meshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        meshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        meshRenderer.material.SetInt("_ZWrite", 0);
        meshRenderer.material.DisableKeyword("_ALPHATEST_ON");
        meshRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        meshRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        meshRenderer.material.renderQueue = 3000;
        
       // Debug.Log("muerte");
        StartCoroutine("desvanecerse");
    }

    IEnumerator desvanecerse() {
        
        yield return new WaitForSeconds(25f);

        Color color = meshRenderer.material.color;
        float startOpacity = color.a;

        float t = 0;

        while (t < fadeSpeed)
        {
            t += Time.deltaTime;
            // interpolación entre 0 y 1:
            float blend = Mathf.Clamp01(t / fadeSpeed);

            // cambia la opacidad progresivamente:
            color.a = Mathf.Lerp(startOpacity, 0, blend);

            // Aplicar el resultado al color del material:
            meshRenderer.material.color = color;

            // esperar un frame y repetir:
            yield return null;
        }

       desvanecer = true;

    }

    private void desvanecimiento() {
        alfa = meshRenderer.material.color.a - Time.deltaTime / (fadeSpeed == 0 ? 1 : fadeSpeed);
        newColor = new Color(matCol.r, matCol.g, matCol.b, alfa);
        meshRenderer.material.SetColor("_Color", newColor);
        desvanecer = alfa <= 0 ? true : false;
        Debug.Log("asdasdasdasd");
        
    }

    public void restarVida(int daño) {
        vit = vit - daño;//Le resto el daño pasado por las flechas de momento solo flechas.
        
        if (vit <= 0 && !animTerminada)
        {
            muerto = true;
            for (int x = 0; x<colliders.Count;x++) {
                colliders[x].enabled = false;
            }
            muerte();
        }
    }
}
