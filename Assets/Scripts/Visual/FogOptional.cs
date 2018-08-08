using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOptional : MonoBehaviour
{
    public bool revertFogState = false;

    void OnPreRender()
    {
        revertFogState = RenderSettings.fog;
        RenderSettings.fog = enabled;
    }

    void OnPostRender()
    {
        RenderSettings.fog = revertFogState;
    }
}



/*
 This script lets you enable and disable per camera.
 By enabling or disabling the script in the title of the inspector, you can turn fog on or off per camera.
*/



