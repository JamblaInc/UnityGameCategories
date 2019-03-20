using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour
{
    public static BannerAds instance;

    private string appID = "ca-app-pub-2714254015915544~1740214392";

    private BannerView bannerView;
    private string bannerID = "ca-app-pub-3940256099942544/6300978111";

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appID);

        this.RequestBanner();
    }

    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        bannerView.Show();
    }        
}
