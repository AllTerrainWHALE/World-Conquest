using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] GameObject sliderGO;
    [SerializeField] Slider sliderComp;

    [SerializeField] GameObject sliderValGO;
    [SerializeField] TMP_Text sliderValComp;

    [SerializeField] public int value
    {
        get { return (int)sliderComp.value; }
        set { sliderComp.value = value; }
    }
    [SerializeField] public int minValue
    {
        get { return (int)sliderComp.minValue; }
        set { sliderComp.minValue = (int)value; }
    }
    [SerializeField] public int maxValue
    {
        get { return (int)sliderComp.maxValue; }
        set { sliderComp.maxValue = (int)value; }
    }

// Start is called before the first frame update
void Start()
    {
        //sliderComp = sliderGO.GetComponent<Slider>();
        //sliderValComp = sliderValComp.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderValComp.SetText(value.ToString());
    }

    //public void setMinValue(int value) => sliderComp.minValue = value;
    //public void setMaxValue(int value) => sliderComp.maxValue = value;
}
