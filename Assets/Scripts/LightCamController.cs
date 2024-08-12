using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightCamController : MonoBehaviour
{
    Camera cam;
    Vector2 MousePos;
    [SerializeField]
    public static float Lightmeter = 3.0f;
    GameObject Light;
    bool LightOn = false;
    public static bool LightCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Light = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position from screen space to world space
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);

        // Since we are in 2D, set the z position to 0
        mouseWorldPosition.z = 0;

        // Update the GameObject's position to snap to the mouse position
        transform.position = mouseWorldPosition;
        if (!LightOn && Lightmeter < 3f)
        {
            Lightmeter += Time.deltaTime;
            if (Lightmeter >= 3f)
            {
                Lightmeter = 3f;
                LightCooldown = false;
            }
        }
        else if (LightOn && Lightmeter > 0f)
        {
            Lightmeter -= Time.deltaTime;
            if (Lightmeter <= 0f)
            {
                LightOn = false;
                Light.SetActive(false);
                Lightmeter = 0f;
                LightCooldown = true;
            }
        }
        
    }
    public void EnableLight(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (LightCooldown)
            {
                return;
            }
            print("Light On");
            Light.SetActive(true);
            LightOn = true;

        }
    }
    public void DisableLight(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {

            print("Light Off");
            LightOn = false;
            Light.SetActive(false);
        }
    }
}
