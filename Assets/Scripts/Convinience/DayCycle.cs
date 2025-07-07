using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{
    private float timeTracker = 0;
    [SerializeField] private Light2D sun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timeTracker);
        timeTracker += Time.deltaTime;
        if(timeTracker > 5 && timeTracker <= 10)
        {
            Cycle(0.7f);
        }
        if (timeTracker > 10 && timeTracker <= 15)
        {
            Cycle(0.5f);
        }
        if (timeTracker > 20 && timeTracker <= 25)
        {
            Cycle(0.3f);
            timeTracker = 0;
        }


    }

    public void Cycle(float dayTime)
    {
        sun.intensity = dayTime;
    }    


}
