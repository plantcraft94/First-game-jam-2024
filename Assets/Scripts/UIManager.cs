using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Image Meter;
    // Start is called before the first frame update
    void Start()
    {
        Meter = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Meter.fillAmount = LightCamController.Lightmeter / 3f;
        if (LightCamController.LightCooldown == true)
        {

            Meter.color = new Color(0f, 0f, 0f, 0.5f + Mathf.PingPong(3f * Time.time, 0.5f));
        }
        else
        {
            Meter.color = Color.black;
        }
    }
}
