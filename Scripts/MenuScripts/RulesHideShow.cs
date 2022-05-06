using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesHideShow : MonoBehaviour
{
    // Create input values
    public GameObject PanelToShow;
    public GameObject PanelToHide1;
    public GameObject PanelToHide2;

    // Shows and hides chosen panels
    public void OpenPanel()
    {
        if (PanelToShow.activeInHierarchy == false)
        {
            PanelToShow.SetActive(true);
            PanelToHide1.SetActive(false);
            PanelToHide2.SetActive(false);
        }
    }
}
