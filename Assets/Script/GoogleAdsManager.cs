using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoogleAdsManager : MonoBehaviour
{
    public UnityEvent onClosed;
    public bool isTest;

    private InterstitialAd interstitial;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
    }


    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-4215221151256725/3210840280";

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Called when an ad is shown.
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitial();
        onClosed.Invoke();
    }

    public void FullAdsShow()
    {
        if (isTest)
        {
            onClosed.Invoke();
            return;
        }
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            onClosed.Invoke();
        }
    }
}
