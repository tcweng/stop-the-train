using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string appStoreID = "3567260";
    private string playStoreID = "3567261";
    private bool targetAppStore = true;
    private bool testAds = true;
    public GenerateGoal goalFunction;
    public GameObject closeLoseWindow;
    public GameObject openScreenUI;

    void Start()
    {
        Advertisement.AddListener(this);
        InitializeAds();
    }

    void InitializeAds()
    {
        if(targetAppStore == true)
        {
            Advertisement.Initialize(appStoreID, testAds);
        } else
        {
            Advertisement.Initialize(playStoreID, testAds);
        }
    }

    public void ShowAds ()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
    }

    public void ShowRewardedAds()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            goalFunction.AdsContinue();
            closeLoseWindow.SetActive(false);
            openScreenUI.SetActive(true);
        }
        else if (showResult == ShowResult.Skipped)
        {
        }
        else if (showResult == ShowResult.Failed)
        {
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }
}
