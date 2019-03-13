using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAd()
    {
        Debug.Log("Ad being shown");
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
    }
}
