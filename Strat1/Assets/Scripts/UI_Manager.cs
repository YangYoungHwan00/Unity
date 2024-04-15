using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Slider hpBar;
    public int h = 10;

    public Wizard wizard;
    
    void Awake()
    {
        wizard = GameObject.Find("Wizard").GetComponent<Wizard>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = wizard.curHp/wizard.maxHp;
    }
}
