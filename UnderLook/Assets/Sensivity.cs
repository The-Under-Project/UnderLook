using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Sensivity : MonoBehaviour
{
    public InputField UserPref;
    public float sensibility;
    public CameraController cam;


    private void Start()
    {
        cam = GetComponentInChildren<CameraController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (UserPref.text != null)
        {
            float resul = 0;
            if (float.TryParse(UserPref.text, out resul) && resul != sensibility)
            {
                sensibility = resul;
                cam.sensi = sensibility;
            }
        }
    }
}
