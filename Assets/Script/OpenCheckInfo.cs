using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCheckInfo : MonoBehaviour
{
    public void OpenCheckInfoForm()
    {
        this.transform.Find("CheckInfo").gameObject.SetActive(true);
        this.GetComponent<Image>().raycastTarget = true;
        this.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void CloseCheckInfoForm()
    {
        this.transform.Find("CheckInfo").gameObject.SetActive(false);
        this.GetComponent<Image>().raycastTarget = false;
        this.GetComponent<RectTransform>().SetAsFirstSibling();
    }
}
