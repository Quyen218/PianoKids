using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private IAPListener m_iapListener;

    private void Start()
    {
        m_iapListener = GetComponent<IAPListener>();
    }

    public void OnPurchaseSuccess(Product p)
    {
        Debug.Log("Piano purchase success " + p.definition.id);
        if (p.definition.payout != null && p.definition.payout.type == PayoutType.Item)
        {
            if (p.definition.payout.subtype == "ads")
            {
                AdsManager.Instance.DisableAds();
            }
        }
    }
}