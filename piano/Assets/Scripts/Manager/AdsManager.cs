using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Monetization;

public class AdsManager : Singleton<AdsManager>
{
    /// <summary>
    /// Unity Ads
    /// </summary>
    string gameId = "3327662";
    bool testMode = false;
    public string placementIdInterstitial = "video";
    /// <summary>
    /// Google Ads
    /// </summary>
    private InterstitialAd interstitial;
    private BannerView bannerView;

    private bool m_enableAds;

    public int m_poincutEnterPLayCount;
    private int m_interstitialLoadFailedCount;
    private int m_bannerLoadFailedCount;

    public bool AdsEnabled { get { return m_enableAds; } }

    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        RequestBanner();
        CheckAdsEnable();

        // Initialize the Unity Ads SDK.
        Monetization.Initialize(gameId, testMode);
        // StartCoroutine(ShowInterstitialWhenReady());

        m_interstitialLoadFailedCount = 0;
        m_bannerLoadFailedCount = 0;
    }

    private void Update()
    {
    }

    IEnumerator ShowInterstitialWhenReady()
    {
        while (!Monetization.IsReady(placementIdInterstitial))
        {
            //MonoBehaviour.print("wwwwwwwwwwadfgdfgdfhdfhdaaaaaaaafffff");
            yield return new WaitForSeconds(0.5f);
        }
        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementIdInterstitial) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
        else
        {
            MonoBehaviour.print("Interstitial unity ads is not ready yet");
        }
    }

    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished || result == ShowResult.Skipped)
        {
            // Reward the player
            m_poincutEnterPLayCount = 0;
        }

    }

    //private AdRequest CreateAdRequest()
    //{
    //    return new AdRequest.Builder()
    //        .AddTestDevice(AdRequest.TestDeviceSimulator)
    //        .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
    //        .AddKeyword("game")
    //        .SetGender(Gender.Male)
    //        .SetBirthday(new DateTime(1985, 1, 1))
    //        .TagForChildDirectedTreatment(false)
    //        .AddExtra("color_bg", "9B30FF")
    //        .Build();
    //}


    private void RequestBanner()
    {


#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-8285969735576565/7277422273";
#endif

        if(bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        bannerView.Hide();
    }

    public void ShowBanner()
    {
        if (!m_enableAds) return;

        this.bannerView.Show();
    }

    public void HideBanner()
    {
        this.bannerView.Hide();
    }

    #region banner callback handlers

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        m_bannerLoadFailedCount = 0;
        MonoBehaviour.print("HandleBannerLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (m_bannerLoadFailedCount < 3)
        {
            m_bannerLoadFailedCount++;
            RequestBanner();
        }
        else
        {
            m_bannerLoadFailedCount = 0;
        }
        MonoBehaviour.print(
            "HandleBannerFailedToLoad event received with message: " + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleBannerOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestBanner();
        MonoBehaviour.print("HandleBannerClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleBannerLeftApplication event received");
    }

    #endregion

    private void RequestInterstitial()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-8285969735576565/6734017864";
#endif

        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }


    public void ShowInterstitial()
    {
        if (!m_enableAds) return;

        m_poincutEnterPLayCount++;

        if (m_poincutEnterPLayCount < 2) return;

        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            MonoBehaviour.print("Interstitial google ads is not ready yet");

            StartCoroutine(ShowInterstitialWhenReady());
        }

    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        m_interstitialLoadFailedCount = 0;
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (m_interstitialLoadFailedCount < 3)
        {
            m_interstitialLoadFailedCount++;
            RequestInterstitial();
        }
        else
        {
            m_interstitialLoadFailedCount = 0;
        }
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
        m_poincutEnterPLayCount = 0;
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
        MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    public void DisableAds()
    {
        Debug.Log("Ads disabled");
        m_enableAds = false;
        string abc = SystemInfo.deviceUniqueIdentifier.GetHashCode().ToString();
        PlayerPrefs.SetInt(abc, m_enableAds ? 1 : 0);
    }
    private void CheckAdsEnable()
    {
        string abc = SystemInfo.deviceUniqueIdentifier.GetHashCode().ToString();
        int a = PlayerPrefs.GetInt(abc, 1);
        m_enableAds = a == 1;
    }
}
