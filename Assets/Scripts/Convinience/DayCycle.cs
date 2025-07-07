using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{
    private float dayEnd = 1200f;
    private float timeTracker = 0;
    private bool isTwilight = false;
    private bool isDay = true;
    private Light2D sun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sun = GetComponent<Light2D>();
        isDay = sun.intensity >= 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timeTracker);
        timeTracker += Time.deltaTime;

        if(isTwilight == false)
        {
            if (timeTracker >= dayEnd)
            {
                StartCoroutine(Twilight());
            }
        }
        



    }

    public void SetDayTime(float dayTime)
    {
        sun.intensity = dayTime;
    }    

    public void ResetDay()
    {
        timeTracker = 0f;
        isDay = true;
        isTwilight = false;
        SetDayTime(1f);
    }


    private IEnumerator Twilight()
    {
        // If it is day changed light amount to night and vise versa
        float setLightIntensity = isDay ? 0.3f : 1f;
        float duration = 5f;
        float elapsesd = 0f;
        float startIntensity = sun.intensity;
        isTwilight = true;
        while(elapsesd < duration)
        {
            elapsesd += Time.deltaTime;
            //changes sunlight based on setLightIntensity, as elapsed gets bigger, the intensity gets closer to 100% of setLightIntensity which is either 0.3 or 1
            sun.intensity = Mathf.Lerp(startIntensity, setLightIntensity, elapsesd / duration);
            yield return null;
        }
        Debug.Log(sun.intensity);
        isDay = !isDay;
        timeTracker = 0f;
        isTwilight = false;
    }



}
