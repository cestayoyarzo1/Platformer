using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedActivator : MonoBehaviour
{



    [SerializeField]
    GameObject flashlight, halo;
    Light itemLight;
    [SerializeField]
    float onIntensity;
    [SerializeField]
    UInt16 state;
    [SerializeField]
    float yScaleON, yScaleOFF;
    [SerializeField]
    bool collisionActive;


    void Start()
    {
        itemLight = flashlight.GetComponentInChildren<Light>();
        itemLight.intensity = 0; 
        state = 0;
        halo.SetActive(false);
        collisionActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("ToggleState");
        itemLight.intensity = state * onIntensity;
        if (state == 1)
        {
            halo.SetActive(true);
            transform.localScale = new Vector3(0.04f, yScaleON, 0.04f);
        }
        else
        {
            halo.SetActive(false);
            transform.localScale = new Vector3(0.04f, yScaleOFF, 0.04f);
        }
    }

    IEnumerator ToggleState()
    {
        yield return new WaitForSeconds(2f);
        state ^= 1;
    }

}
