using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopMenu : MenuBase<ShopMenu>
{

    [SerializeField] Button m_buyBtn;
    [SerializeField] GameObject m_successPopup;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void OnBack()
    {
        /// no doing
    }

    public void OnCloseButtonClick()
    {
        GUIManager.Instance.GotoHome();
    }

    public void OnBuyButtonClick()
    {
    }

    public void OnRestoreButtonClick()
    {

    }

    protected override void OnMenuOpening()
    {
        bool a = AdsManager.Instance.AdsEnabled;
        if (!a)
        {
            DisableBuyButton();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Piano purchase failed " + i.definition.id);
    }

    public void OnPurchaseCompleted(Product p)
    {
        Debug.Log("SHOP: Piano purchase completed");
        DisableBuyButton();
        if (m_successPopup)
        {
            m_successPopup.SetActive(true);
        }
    }

    private void DisableBuyButton()
    {
        if (m_buyBtn)
        {
            m_buyBtn.interactable = false;
            m_buyBtn.GetComponent<Image>().color = Color.gray;
        }
    }
}
