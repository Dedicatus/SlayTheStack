using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeController : MonoBehaviour
{

    private float lastWidth;
    private float lastHeight;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1080, 1920, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastWidth != Screen.width)
        {
            Screen.SetResolution(Screen.width, Screen.width * (16 / 9), true);
        }
        else if (lastHeight != Screen.height)
        {
            Screen.SetResolution(Screen.height * (9 / 16), Screen.height, true);
        }

        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }
}
