using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : Singleton<AdManager>
{
    BannerView _bannerView;
    public BannerView BannerView => _bannerView;

    string _bannerViewId;

    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        _bannerViewId = "adUnitId";
#else
        _bannerViewId = "ca-app-pub-5639813524802030/9295725497";
#endif

        MobileAds.Initialize(initStatus => { });

        DontDestroyOnLoad(this);
        RequestBanner();
    }

    void RequestBanner()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }
        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        _bannerView = new BannerView(_bannerViewId, adSize, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();
        BannerView.LoadAd(request);
    }
}
