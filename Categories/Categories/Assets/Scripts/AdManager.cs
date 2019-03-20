using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;


public class AdManager : MonoBehaviour
{
    private BannerView bannerView;
    [SerializeField] private string appID = "ca-app-pub-2714254015915544~1740214392";
    [SerializeField] private string bannerID = "ca-app-pub-3940256099942544/6300978111";

    void start()
    {
        MobileAds.Initialize(appID);
        RequestBanner();
    }

    private void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

    }

    public void OnClickShowBanner()
    {
        this.RequestBanner();
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
