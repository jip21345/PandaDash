  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool isTouching;

    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {

            isTouching = true;

        }
        else 
        {
            isTouching = false;
        }
    }


    public bool IsTouch()
    {

        return isTouching;
    }
}
