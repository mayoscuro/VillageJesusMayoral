using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecosystem : MonoBehaviour
{
    private int numeroTiempollovidoDuranteLaSemana;
    private int numeroDeFrutasYAnimalesCaidos;

    //Información sobre cuando termina la semana.
    [HideInInspector] public bool finDeSemana;
    //Información sobre si es la primera semana que se juega.
    [HideInInspector] public bool primerDia;


    [Header("Todos los nodos que forman el grafo")]
    [Tooltip("Nodos que forman el grafo, pueden estar el cualquier orden,pero recomiendo que el primero sea el maximo depredador")]
    public List<Nodo> nodosGrafo;

    [Header("Spawn")]
    [Tooltip("Script que lleva a cabo las operaciones de spawn y eliminado de los diferentes animales")]
    public Spawn2 spawn;
    [Tooltip("Numero de animales que se spawnearan cada semana. Sirve para quepueda ser ajustado desde el inspector")] public int numeroDeAnimales;


    //Función que organiza los datos iniciales de la nueva semana.
    public void nuevaSemana()
    {
        recorrerGrafo();
    }

    private void recorrerGrafo()
    {//Función que recorre el grafo y lo actualiza llamando a calcularNuevoGrafo.
        Queue<Nodo> cola = new Queue<Nodo>();//Cola Fifo inicialmente vacia.
        int cantidadNodos = transform.childCount;//Numero de Nodos del grafo.
        Nodo nodoInicial = nodosGrafo[0];//El primer Nodo hace referencia a los lobos, que son el maximo depredador.
        foreach (Nodo nodo in nodosGrafo)
        {
            nodo.visitado = false;
        }
        nodoInicial.visitado = true;
        cola.Enqueue(nodoInicial);

        while (cola.Count > 0)
        {//Mientras la cola no este vacia
            Nodo actual = cola.Dequeue();
            //Debug.Log(actual.gameObject.name + ",   ");//Debug de los nombres de los diferentes nodos, para comprobar que funcionan bien.

            if (finDeSemana || primerDia)
            {
                terminaLaSemana(actual);
                /*Debug.Log(actual.gameObject.name + ", caidos esta semana: " + obtenerDatosDelGrafo(actual.gameObject.name).numeroCaidos);
                Debug.Log(actual.gameObject.name + ", caidos semana pasada: " + obtenerDatosDelGrafo(actual.gameObject.name).numCaidosSemanaPasada);
                Debug.Log(actual.gameObject.name + ", vivos esta semana: " + obtenerDatosDelGrafo(actual.gameObject.name).numeroVivos);
                Debug.Log(actual.gameObject.name + ", vivos semana pasada: " + obtenerDatosDelGrafo(actual.gameObject.name).numVivosSemanaPasada);*/
            }

            foreach (Nodo presa in actual.comida)
            {
                if (presa.visitado == false)
                {
                    presa.visitado = true;
                    cola.Enqueue(presa);
                }
            }
        }
        finDeSemana = false;//Pongo a false el booleano para que esta linea de terminarLaSemana() solo se ejecuta cuando toque.
        primerDia = false;

    }

    private void terminaLaSemana(Nodo actual)
    {
        if (!primerDia)
        {
            actual.numVivosSemanaPasada = actual.numeroVivosInicioSemana /*- actual.numCaidosSemanaPasada*/;//Guardar cuantos animales de este tipo estaban vivos la semana pasada (para poder contrastar con la actual).
        }
        actual.numeroVivosInicioSemana = calcularNuevoNumeroRecursos(actual);
        actual.inicializarVivos();//Esta linea esta por probar.
        actual.ActualizarDepredadores();//Esta linea esta por probar.
        //Debug.Log(actual.numeroVivosInicioSemana);

        if (actual.numeroVivos <= 0) {
            borrarNodoDelEcosistema(actual);
        }

    }

    private void borrarNodoDelEcosistema(Nodo actual) {
        foreach(Nodo nodo in nodosGrafo) { 
            
            if (nodo.depredadores.IndexOf(actual) >= 0 && nodo.numeroDepredadores.Count > 0 && nodo.depredadores.Contains(actual) && nodo !=null)
            {
                //Debug.Log("Este peta " + nodo.gameObject.name);
                nodo.numeroDepredadores.RemoveAt(nodo.depredadores.IndexOf(actual));//Me cargo el numero de depredadores que esta en la misma posición que el objeto a eliminar.
                nodo.porcentajeProbabilidades.RemoveAt(nodo.depredadores.IndexOf(actual));//Me cargo el porcentaje de los depredadores que esta en la misma posición que el objeto a eliminar.
            }
            
            nodo.comida.Remove(actual);//Borro el nodo actual de las listas de comida de todos los nodos en los que esté.
            nodo.depredadores.Remove(actual);//borro al nodo actual de todas las listas de depredadores en las que aparezca.

            
        }
        //Ponemos a punto las probabilidades de todos los animales, cuando un elemento deja de pertenecer al ecosistema:
        foreach (Nodo comida in actual.comida)
        {
            if (comida.porcentajeProbabilidades.Count > 0)
            {
                for (int y = 0; y < comida.porcentajeProbabilidades.Count; y++) { 
                    int numeroDepredadores = comida.porcentajeProbabilidades.Count;//Como el animal fallecido ya no existe, las probabilidades de que se los coman otros animales tambien varia.
                    int maximaProbabilidad = 100;
                    int probabilidad = maximaProbabilidad / numeroDepredadores;

                    comida.porcentajeProbabilidades[y] = probabilidad;
                }
            }
        }
        int x = 0;
        foreach (Nodo depredador in actual.depredadores) {
            if (depredador.comida.Count > 0)
            {                                           // //Los que antes eran depredadores de varios animales, ahora lo serán de un numero más reducido de ellos, por lo que se modifican sus preferencias alimenticias tambien.
                foreach (Nodo comida in depredador.comida) //Preguntar si esto le parece bien a la gente.
                {
                    int numeroDepredadores = comida.porcentajeProbabilidades.Count;
                    int maximaProbabilidad = 100;
                    int probabilidad = maximaProbabilidad / numeroDepredadores;
                
                    comida.porcentajeProbabilidades[x] = probabilidad;
                    x++;
                }
                
            }
        }

        Debug.Log(actual.gameObject.name);
        nodosGrafo.RemoveAt(nodosGrafo.IndexOf(actual));
        Destroy(actual.gameObject);
    }

    private int calcularMuertesDelEcosistema(Nodo actual)
    {
        int muertesEnEcosistema = 0;
        int x = 0;
        foreach (int porcentaje in actual.porcentajeProbabilidades)
        {
            if (((actual.numeroDepredadores[x] * porcentaje) / 100) < 6)
            {
                muertesEnEcosistema += Random.Range(0, 4);
                //Debug.Log(actual.gameObject.name);
            }
            else if (((actual.numeroDepredadores[x] * porcentaje) / 100) > 5)
            {
                muertesEnEcosistema += Random.Range(6, 10);
            }
        }
        //Debug.Log("Muertes por ecosistema, " + actual.gameObject.name + "  ,   " + muertesEnEcosistema);
        return muertesEnEcosistema;
    }
    private int calcularNuevoNumeroRecursos(Nodo actual)
    {
        if (!primerDia)
        {
            int muertesExtra = calcularMuertesDelEcosistema(actual);
            int inicialesSiguienteSemana = 0;
            if (actual.numeroCaidos + muertesExtra < ((actual.numeroVivosInicioSemana * 30) / 100))
            {//Si el numero de caidos es menor al 30% de los que había inicialmente.
                inicialesSiguienteSemana = actual.numeroVivosInicioSemana + numeroDeAnimales; //Se suman 10 nuevas unidades semanales a dicha especie.
                spawn.animalSpawn(actual.gameObject.name, numeroDeAnimales, actual);//Spawnear a los animales cada vez que pasa una semana.
            }
            else if (actual.numeroCaidos + muertesExtra > ((actual.numeroVivosInicioSemana * 29) / 100) && actual.numeroCaidos + muertesExtra < ((actual.numeroVivosInicioSemana * 50) / 100))
            {//Si el numero de caidos es menor al 50% y mayor al 30% de los que habian inicialmente.
                inicialesSiguienteSemana = actual.numeroVivosInicioSemana;//Se mantiene el numero de unidades de la especie a como estaban al principio.
            }
            else if (actual.numeroCaidos + muertesExtra > ((actual.numeroVivosInicioSemana * 50) / 100))
            { //Si el numero de caidos es mayor que el 50% de los que había inicialmente.
                inicialesSiguienteSemana = actual.numeroVivosInicioSemana - (actual.numeroCaidos + muertesExtra); //Se restan x unidades semanales a dicha especie.
                spawn.destroyAnimals(actual.gameObject.name, muertesExtra);
                actual.numCaidosSemanaPasada = actual.numeroCaidos + muertesExtra;
            }
            return inicialesSiguienteSemana;
        }
        else
        {
            spawn.animalSpawn(actual.gameObject.name, actual.numeroVivosInicioSemana, actual);
            return actual.numeroVivosInicioSemana;//Se devuelven los mismos que los que hay en el inspector, porque son los de inicio del juego.
        }
    }

    public Nodo obtenerDatosDelGrafo(string name)
    {
        foreach(Nodo actual in nodosGrafo)
        {
            if (actual.gameObject.name == name)
            {
                return actual;
            }
        }
        return null;
    }
}