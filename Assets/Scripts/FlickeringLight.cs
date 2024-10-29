using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light lightSource;
    public float minIntensity = 0.0f;                // Minimum light intensity (can go to 0 for complete off)
    public float maxIntensity = 1.5f;                // Maximum light intensity
    public float flickerDurationMin = 0.2f;          // Minimum flicker duration
    public float flickerDurationMax = 0.8f;          // Maximum flicker duration
    public float offDurationMin = 0.5f;              // Minimum duration the light is off
    public float offDurationMax = 1.5f;              // Maximum duration the light is off
    private float nextFlickerTime = 0f;              // Time when the next flicker should occur

    void Start()
    {
        // Get the Light component attached to this GameObject
        lightSource = GetComponent<Light>();
        // Start the first flicker
        StartFlickering();
    }

    void Update()
    {
        if (lightSource != null)
        {
            // Check if it's time to change the light's state
            if (Time.time >= nextFlickerTime)
            {
                // Randomly decide whether to turn the light on or off
                if (lightSource.intensity <= minIntensity)
                {
                    // Turn the light on with random intensity
                    lightSource.intensity = Random.Range(minIntensity, maxIntensity);
                    // Schedule the next flicker off
                    nextFlickerTime = Time.time + Random.Range(flickerDurationMin, flickerDurationMax);
                }
                else
                {
                    // Turn the light off
                    lightSource.intensity = minIntensity;
                    // Schedule the next flicker on
                    nextFlickerTime = Time.time + Random.Range(offDurationMin, offDurationMax);
                }
            }
        }
    }

    private void StartFlickering()
    {
        // Initialize the light intensity to be off at the start
        lightSource.intensity = minIntensity;
        // Set the next flicker time based on an initial random off duration
        nextFlickerTime = Time.time + Random.Range(offDurationMin, offDurationMax);
    }
}

