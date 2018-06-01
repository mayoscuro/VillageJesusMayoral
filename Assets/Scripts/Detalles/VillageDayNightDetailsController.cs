using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VillageDayNightDetailsController : MonoBehaviour {
    public List<Light> luces;
    public List<GameObject> waterDay;
    public List<GameObject> waterNight;
    public GameObject clouds;
    public float lightStep = 10;
    public DayNightController diaNoche;

    ParticleSystem.MainModule settings;
    private void Start()
    {
        settings = clouds.GetComponent<ParticleSystem>().main;
        
    }
    // Update is called once per frame
    void Update () {
        if (DayNightController.day)
        {
            //settings.startColor = new ParticleSystem.MinMaxGradient(Color.white);
            foreach (GameObject water in waterDay) {
                water.SetActive(true);
            }

            foreach (GameObject water in waterNight)
            {
                water.SetActive(false);
            }
            for (int x = 0; x < luces.Count; x++)
            {
                luces[x].gameObject.SetActive(false);
            }
        }
        else {
            //settings.startColor = new ParticleSystem.MinMaxGradient(new Vector4(84,84,84,0));
            foreach (GameObject water in waterDay)
            {
                water.SetActive(false);
            }

            foreach (GameObject water in waterNight)
            {
                water.SetActive(true);
            }
            for (int x = 0; x < luces.Count; x++)
            {
                luces[x].gameObject.SetActive(true);

            }

        }
    }

   /* IEnumerator startLights(int x) {
        if (luces[x].intensity < 4.8f)
        {
            luces[x].intensity += lightStep * Time.deltaTime;
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator stopLights(int x)
    {
        luces[x].intensity -= lightStep * Time.deltaTime;
        yield return new WaitForEndOfFrame();
    }*/
}
