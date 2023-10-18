using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    bool isFirstUpdated = true;

    private void Update()
    {
        if (isFirstUpdated)
        {
            isFirstUpdated = false;


            Loader.LoaderCallBack();
        }
    }
}
