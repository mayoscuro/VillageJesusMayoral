using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DayNightController : MonoBehaviour {
    [Header("Sun properties")]
    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0,1)]
    public float currentTimeOfDay = 0;
    float sunInitialIntensity;
    public bool timerStop = false;

    [Header("Eventos especiales diarios")]
    public EventosDeAnimalesEspeciales eventosEsp;




    
    public void stopTimmer() {//Cuando el personaje habla con algun NPC.
        timerStop = !timerStop;
    }

    [Header("Info variables")]
    public static bool day;//estatica para que se pueda comprobar si es de dia o de noche en cualquier script.
    
    void Start() {
        sunInitialIntensity = sun.intensity;
    }
    
    void Update() {
        UpdateSun();
        if (!timerStop) {
            currentTimeOfDay += (Time.deltaTime / secondsInFullDay);//* timeMultiplier;
        }
        if (currentTimeOfDay >= 1) { //Cuando pasa esto es porque ha pasado un dia.
            currentTimeOfDay = 0;
            WeekController.numberOfDays++;
            Debug.Log(WeekController.numberOfDays);
        }
       // Debug.Log(day);
    }
    
    void UpdateSun() {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
 
        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
            day = false;
            if (!eventosEsp.kodama1.activeInHierarchy && !eventosEsp.lordOfTheForest.activeInHierarchy) //Si es de noche y no hay un kodama, quiere decir que hay posibilidades de que salga alguno.
            {
                eventosEsp.eventRandomChoose();
            }
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            if (eventosEsp.kodama1.activeInHierarchy)
            {
                eventosEsp.kodama1.GetComponent<NavMeshAgent>().isStopped = true;
                eventosEsp.aparicion = false;
                eventosEsp.eventoKodamaTipo1 = false;
                eventosEsp.eventoKodamaTipo2 = false;
                eventosEsp.eventoKodamaTipo3 = false;
                eventosEsp.kodama1.SetActive(false);
            }

            if (eventosEsp.lordOfTheForest.activeInHierarchy) {
                eventosEsp.aparicion = false;
                eventosEsp.eventoLordOfTheForest = false;
                eventosEsp.lordOfTheForest.SetActive(false);
            }
            day = true;
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));

        }
        else if (currentTimeOfDay >= 0.73f)
        {
            day = false;
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
            if (!eventosEsp.kodama1.activeInHierarchy && !eventosEsp.lordOfTheForest.activeInHierarchy) //Si es de noche y no hay un kodama, quiere decir que hay posibilidades de que salga alguno.
            {
                eventosEsp.eventRandomChoose();
            }
        }
 
        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
