using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekController : MonoBehaviour {
    public static int numberOfDays;
    public bool firstDay = false; //Solo controla si es el primer dia despues de darle a newGame, el resto de dias son normales.
    public Ecosystem ecosystem;
    public JefeAldeaMisiones misiones;



    private RestablecerPlatasYRecursos restablecerRecursos;

	// Use this for initialization
	void Start () {
        restablecerRecursos = GetComponent<RestablecerPlatasYRecursos>();
	}
	
	// Update is called once per frame
	void Update () {
        newWeek();
	}

    void newWeek() {
        if (numberOfDays == 0 && !firstDay)//Solo debería ejecutarse el primer dia de la primera semana.
        {
            firstDay = true;
            ecosystem.primerDia = true;
            ecosystem.nuevaSemana();
            misiones.recalcularMisiones();
        }
        
        if (numberOfDays == 8)
        {
            numberOfDays = 0;
            ecosystem.finDeSemana = true;//Para que entre en el if de recorrer el grafo 
            ecosystem.nuevaSemana();
            misiones.recalcularMisiones();
            restablecerRecursos.restablecerRecursos = true; //Resetea los recursos que se obtienen en el mapa (No animales).
        }
    }
}
