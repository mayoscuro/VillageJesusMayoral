using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn2 : MonoBehaviour
{
    [Header("Prefabs")]

    public GameObjectListOfLists listOfListsGameObject = new GameObjectListOfLists();



    private List<GameObject> WolfInScene = new List<GameObject>();//Lista de todos los lobos que hay actualmente en escena.
    private List<GameObject> DeerInScene = new List<GameObject>();//Lista de todos los ciervos que hay actualmente en escena.
    private List<GameObject> FoxInScene = new List<GameObject>();//Lista de todos los zorros que hay actualmente en escena.
    //public GameObject 

    public void animalSpawn(string name, int cantidad, Nodo nodo)
    {
        int posDelLugar = 0;//posición del lugar de spawn en el script.
        int aux = 0;//Auxiliar para spawnear 5 animales en x lugar y luego cambiar de sitio.
        GameObject alfagbo = null;
        //Debug.Log(name + ", " + cantidad);
        for (int y = 0; y < listOfListsGameObject.gameObjects.Count; y++)
        {
            if (listOfListsGameObject.gameObjects[y].prefab.name == name)
            {
                for (int x = 0; x < cantidad; x++)
                {
                    GameObject nuevo = Instantiate(listOfListsGameObject.gameObjects[y].prefab);
                    //Debug.Log(name);
                    //Debug.Log(nodo.gameObject);
                    nuevo.GetComponent<Animal>().nodoAnimal = nodo;
                    
                    nuevo.transform.position = listOfListsGameObject.gameObjects[y].listSpawns[posDelLugar].transform.position;
                    nuevo.GetComponent<NavMeshAgent>().Warp( new Vector3(listOfListsGameObject.gameObjects[y].listSpawns[posDelLugar].transform.position.x + Random.Range(6,12), listOfListsGameObject.gameObjects[y].listSpawns[posDelLugar].transform.position.y, listOfListsGameObject.gameObjects[y].listSpawns[posDelLugar].transform.position.z + Random.Range(6, 12)));
                    listOfListsGameObject.gameObjects[y].inScene.Add(nuevo);

                    if (listOfListsGameObject.gameObjects[y].prefab.name == "Lobo" && aux == 0)
                    {
                        nuevo.GetComponent<AnimalsBasicIA>().alfa = true;
                        nuevo.transform.localScale = new Vector3(2f,2f,2f);
                        alfagbo = nuevo;
                    }
                    else if (listOfListsGameObject.gameObjects[y].prefab.name == "Lobo" && aux != 0)
                    {
                        nuevo.GetComponent<AnimalsBasicIA>().alfa = false;
                        nuevo.GetComponent<AnimalsBasicIA>().alfagameObject = alfagbo;
                    }

                    aux++;
                    
                    
                    //Lo que pretende hacer este if es que se spawneen 5 animales en cada zona y si llega al final, vuelvan a spawnear en la primera zona.
                    if (aux == 3 && posDelLugar < listOfListsGameObject.gameObjects[y].listSpawns.Count)
                    {
                        posDelLugar++;
                        aux = 0;
                    }
                    if (posDelLugar == listOfListsGameObject.gameObjects[y].listSpawns.Count)
                    {
                        posDelLugar = 0;
                    }
                }
            }

        }
        //Debug.Log(WolfInScene[0].name);*/
    }

    public void destroyAnimals(string name, int cantidad)
    {
        for (int y = 0; y < listOfListsGameObject.gameObjects.Count; y++)
        {
            if (listOfListsGameObject.gameObjects[y].prefab.name == name)
            {
                for (int x = 0; x < cantidad; x++)
                {
                    if (listOfListsGameObject.gameObjects[y].inScene.Count > 0)
                    {
                        GameObject mort = listOfListsGameObject.gameObjects[y].inScene[0];
                        listOfListsGameObject.gameObjects[y].inScene.Remove(mort);
                        Destroy(mort);
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class GameObjectList
{
    public GameObject prefab;
    public List<GameObject> listSpawns;
    [HideInInspector]public List<GameObject> inScene;
}

[System.Serializable]
public class GameObjectListOfLists
{
    public List<GameObjectList> gameObjects;
}
