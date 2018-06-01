using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    [Header("Prefabs")]
    [Tooltip("prefab del lobo a spawnear")]public GameObject lobo;
    [Tooltip("prefab del ciervo a spawnear")] public GameObject ciervo;
    [Tooltip("prefab del zorro a spawnear")] public GameObject zorro;

    public GameObject[] wolfSpawn;//Lugares des los que pueden spawnear los lobos.
    public GameObject[] deerSpawn;//Lugares desde los que pueden spawnear los ciervos.
    public GameObject[] foxSpawn;//Lugares desde los que pueden spawnear los zorros.


    private List<GameObject> WolfInScene = new List<GameObject>();//Lista de todos los lobos que hay actualmente en escena.
    private List<GameObject> DeerInScene = new List<GameObject>();//Lista de todos los lobos que hay actualmente en escena.
    private List<GameObject> FoxInScene = new List<GameObject>();//Lista de todos los lobos que hay actualmente en escena.
    //public GameObject 

    public void animalSpawn(string name, int cantidad) {
        int posDelLugar = 0;//posición del lugar de spawn en el script.
        int aux = 0;//Auxiliar para spawnear 5 animales en x lugar y luego cambiar de sitio.
        switch (name) {
            case "Lobo":
                for (int x = 0;x <cantidad; x++) {
                    GameObject loboObj = Instantiate(lobo);
                    loboObj.transform.position = wolfSpawn[posDelLugar].transform.position;
                    WolfInScene.Add(loboObj);
                    aux++;
                    //Lo que pretende hacer este if es que se spawneen 5 animales en cada zona y si llega al final, vuelvan a spawnear en la primera zona.
                    if (aux == 3 && posDelLugar < wolfSpawn.Length) {
                        posDelLugar++;
                        aux = 0;
                    }
                    if (posDelLugar == wolfSpawn.Length) {
                        posDelLugar = 0;
                    }
                }
                break;
            case "Ciervo":
                for (int x = 0; x < cantidad; x++)
                {
                    GameObject ciervoObj = Instantiate(ciervo);
                    ciervoObj.transform.position = deerSpawn[posDelLugar].transform.position;
                    DeerInScene.Add(ciervoObj);
                    aux++;
                    //Lo que pretende hacer este if es que se spawneen 5 animales en cada zona y si llega al final, vuelvan a spawnear en la primera zona.
                    if (aux == 5 && posDelLugar < deerSpawn.Length)
                    {
                        posDelLugar++;
                        aux = 0;
                    }
                     if (posDelLugar == deerSpawn.Length)
                    {
                        posDelLugar = 0;
                    }
                }
                break;
            case "Zorro":
                for (int x = 0; x < cantidad; x++)
                {
                    GameObject zorroObj = Instantiate(zorro);
                    zorroObj.transform.position = foxSpawn[posDelLugar].transform.position;
                    FoxInScene.Add(zorroObj);
                    aux++;
                    //Lo que pretende hacer este if es que se spawneen 5 animales en cada zona y si llega al final, vuelvan a spawnear en la primera zona.
                    if (aux == 2 && posDelLugar < foxSpawn.Length)
                    {
                        posDelLugar++;
                        aux = 0;
                    }
                    if (posDelLugar == foxSpawn.Length)
                    {
                        posDelLugar = 0;
                    }
                }
                break;
        }

        //Debug.Log(WolfInScene[0].name);
    }

    public void destroyAnimals(string name, int cantidad) {
        switch (name)
        {
            case "Lobo":

                for (int x = 0; x < cantidad; x++)
                {
                    if (WolfInScene.Count>0) {
                        GameObject loboMort = WolfInScene[0];
                        WolfInScene.Remove(loboMort);
                        Destroy(loboMort);
                    }
                }
                break;
            case "Ciervo":
                for (int x = 0; x < cantidad; x++)
                {
                    if (DeerInScene.Count > 0)
                    {
                        GameObject ciervoMort = DeerInScene[0];
                        DeerInScene.Remove(ciervoMort);
                        Debug.Log("Mortfffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" + ciervoMort);
                        Destroy(ciervoMort);

                        
                        
                    }
                }
                break;
            case "Zorro":
                for (int x = 0; x < cantidad; x++)
                {
                    if (FoxInScene.Count > 0)
                    {
                        GameObject zorroMort = FoxInScene[0];
                        FoxInScene.Remove(zorroMort);
                        Destroy(zorroMort);
                    }
                }
                break;
        }
    }

}
