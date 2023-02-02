using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    [SerializeField] string adUnitId;

    [SerializeField] Button b_initialize;

    private BannerView bannerView;

    public void Start()
    {
        b_initialize.onClick.AddListener(RequestInterstitial);
        MobileAds.Initialize(InitStatus => { });
        RequestBanner();

        List<string> deviceIds = new List<string>();

        deviceIds.Add("106d83047e564f5e9fdf8403311d70aa");

        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        sadUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        InterstitialAd.Load(adUnitId , request , OnLoad);
    }

    private void OnLoad(InterstitialAd arg1, LoadAdError arg2)
    {
        Debug.Log("test " + arg1 + "cause: " + arg2);
        arg1.Show();
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
                adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
                adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the bottom of the screen.
        bannerView = new BannerView(adUnitId, AdSize.IABBanner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void Cleanup()
    {
        bannerView.Destroy();
    }

    private void OnApplicationQuit()
    {
        Cleanup();
    }
}