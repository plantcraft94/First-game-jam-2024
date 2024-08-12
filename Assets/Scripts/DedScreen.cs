using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedScreen : MonoBehaviour
{
    GameObject DeadText;
    // Start is called before the first frame update
    void Start()
    {
        DeadText = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.isDed == true)
        {
            DeadText.SetActive(true);
        }
    }
}
