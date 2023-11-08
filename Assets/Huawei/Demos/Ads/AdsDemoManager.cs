using System.Collections.Generic;
using UnityEngine;
using HmsPlugin;
using HuaweiMobileServices.Ads;
using System;
using UnityEngine.Events;
public class AdsDemoManager : MonoBehaviour
{
    //private Toggle testAdStatusToggle;
    private readonly string TAG = "[HMS] AdsDemoManager: ";

    #region Singleton

    public static AdsDemoManager Instance { get; private set; }
    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        HMSAdsKitManager.Instance.ConsentOnFail = OnConsentFail;
        HMSAdsKitManager.Instance.ConsentOnSuccess = OnConsentSuccess;
        HMSAdsKitManager.Instance.RequestConsentUpdate();

        //testAdStatusToggle = GameObject.FindGameObjectWithTag("Toggle").GetComponent<Toggle>();
        //testAdStatusToggle.isOn = HMSAdsKitSettings.Instance.Settings.GetBool(HMSAdsKitSettings.UseTestAds);

        //#region SetNonPersonalizedAd , SetRequestLocation

        var builder = HwAds.RequestOptions.ToBuilder();

        builder
            .SetConsent("tcfString")
            .SetNonPersonalizedAd((int)NonPersonalizedAd.ALLOW_ALL)
            .Build();

        bool requestLocation = true;
        var requestOptions = builder.SetConsent("testConsent").SetRequestLocation(requestLocation).Build();

        Debug.Log($"{TAG}RequestOptions NonPersonalizedAds:  {requestOptions.NonPersonalizedAd}");
        Debug.Log($"{TAG}Consent: {requestOptions.Consent}");

        //#endregion

    }
    private void Awakee()
    {
        // Initialize the HMS Ads SDK
        HwAds.Init("sfbla");
        // Optionally, you can set personalized ad consent if needed
        // HwAds.SetPersonalizedAdConsent(true); // Set to true for personalized ads

        // Check if the initialization was successful
    }
    private void OnConsentSuccess(ConsentStatus consentStatus, bool isNeedConsent, IList<AdProvider> adProviders)
    {
        Debug.Log($"{TAG}OnConsentSuccess consentStatus:{consentStatus} isNeedConsent:{isNeedConsent}");
        foreach (var AdProvider in adProviders)
        {
            Debug.Log($"{TAG}OnConsentSuccess adproviders: Id:{AdProvider.Id} Name:{AdProvider.Name} PrivacyPolicyUrl:{AdProvider.PrivacyPolicyUrl} ServiceArea:{AdProvider.ServiceArea}");
        }
    }

    private void OnConsentFail(string desc)
    {
        Debug.LogError($"{TAG}OnConsentFail:{desc}");
    }

    public void ShowBannerAd()
    {
        Debug.Log($"{TAG}ShowBannerAd");

        HMSAdsKitManager.Instance.ShowBannerAd();
    }

    public void HideBannerAd()
    {
        Debug.Log($"{TAG}HideBannerAd");

        HMSAdsKitManager.Instance.HideBannerAd();
    }
    public void ShowRewardedAd(Action<int> onRewarded,Action fail=null)
    {
        Debug.Log($"{TAG}ShowRewardedAd");
        HMSAdsKitManager.Instance.OnRewarded = (Reward) => onRewarded.Invoke(Reward.Amount);
        HMSAdsKitManager.Instance.ShowRewardedAd(fail);
    }
    //public void ShowRewardedAd()
    //{
    //    Debug.Log($"{TAG}ShowRewardedAd");
    //    HMSAdsKitManager.Instance.OnRewarded = (Reward) => onRewarded.Invoke(Reward.Amount);
    //    HMSAdsKitManager.Instance.OnRewardedAdFailedToLoad = onRewardFail;
    //    HMSAdsKitManager.Instance.ShowRewardedAd();
    //}
    public void ShowInterstitialAd(Action success)
    {
        Debug.Log($"{TAG}ShowInterstitialAd");
        HMSAdsKitManager.Instance.ShowInterstitialAd(success);
    }

    public void ShowSplashImage()
    {
        Debug.Log($"{TAG}ShowSplashImage!");

        HMSAdsKitManager.Instance.LoadSplashAd("testq6zq98hecj", SplashAd.SplashAdOrientation.PORTRAIT);
    }

    public void ShowSplashVideo()
    {
        Debug.Log($"{TAG}ShowSplashVideo!");

        HMSAdsKitManager.Instance.LoadSplashAd("testd7c5cewoj6", SplashAd.SplashAdOrientation.PORTRAIT);
    }
    public void SetTestAdStatus()
    {
       // HMSAdsKitManager.Instance.SetTestAdStatus(testAdStatusToggle.isOn);
        HMSAdsKitManager.Instance.SetTestAdStatus(HMSAdsKitSettings.Instance.Settings.GetBool(HMSAdsKitSettings.UseTestAds));
        HMSAdsKitManager.Instance.DestroyBannerAd();
        HMSAdsKitManager.Instance.LoadAllAds();
    }
}
