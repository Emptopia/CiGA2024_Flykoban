using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpos : MonoBehaviour
{
    public ParticleSystem pushToEndParticle;
    private WinnerManager WinnerManagers;
    private bool isOn = false;
    private AudioSource inAudio;
    private void Start()
    {
        WinnerManagers = WinnerManager.Instance;
        inAudio = GetComponent<AudioSource>();

    }

    private void Update()
    {
       
    }
    
    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Box_Gra")||other.CompareTag("Box_No_Gra"))&&isOn==false)
        {
            inAudio.Play();
            pushToEndParticle.Play();
            Debug.Log("stay");
            WinnerManagers.NowCount++;
            isOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box_Gra")||other.CompareTag("Box_No_Gra")&&isOn==true)
        {
            Debug.Log("In");
            WinnerManagers.NowCount--;
            isOn = false;
        }
    }
}
