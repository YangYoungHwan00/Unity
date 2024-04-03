using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject wizard;
    public Slime slime;
    public Slider slider;
    
    void Awake()
    {
        slider = GetComponent<Slider>();
        wizard = GameObject.Find("Wizard");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
