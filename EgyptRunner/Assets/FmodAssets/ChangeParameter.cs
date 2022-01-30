using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParameter : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;
    [SerializeField] [Range (0f,1f)]
    private float estado;

    private bool on = false;
  

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    void Update()
    {
       if (Input.GetButtonDown("Change") && !on)
        {
            instance.setParameterByName("Estado",1f);
            on = true;
        }
        else if (Input.GetButtonDown("Change") && on)
        {
           instance.setParameterByName("Estado",0f);
            on = false; 
        }
    }
}
