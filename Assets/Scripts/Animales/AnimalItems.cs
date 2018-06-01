using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItems : MonoBehaviour {
    [Header("Objetos")]
    private ItemDatabase itemDatabase;
    private List<Item> items = new List<Item>();
    public enum TipoAnimal {
        Zorro,
        Lobo,
        Ciervo
    }
    public TipoAnimal tipoAnimal;


    // Use this for initialization
    void Start () {
        itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        StartCoroutine("espera");//Si no se espera no da tiempo a que se guarden los datos en la base de datos y se sale del rango...
    }

    IEnumerator espera() {
        yield return new WaitForSeconds(0.01f);
        if (tipoAnimal == TipoAnimal.Zorro)
        {
            //Debug.Log(itemDatabase.items[15]);
            items.Add(itemDatabase.items[15]);
            items.Add(itemDatabase.items[16]);
            items.Add(itemDatabase.items[17]);
            items.Add(itemDatabase.items[18]);

        }
        else if (tipoAnimal == TipoAnimal.Lobo)
        {
            items.Add(itemDatabase.items[8]);
            items.Add(itemDatabase.items[9]);
            items.Add(itemDatabase.items[10]);
        }
        else if (tipoAnimal == TipoAnimal.Ciervo)
        {
            items.Add(itemDatabase.items[19]);
            items.Add(itemDatabase.items[20]);
            items.Add(itemDatabase.items[21]);
        }
    }
}
