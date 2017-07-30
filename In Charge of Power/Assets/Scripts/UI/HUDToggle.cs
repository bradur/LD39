// Date   : 30.07.2017 13:26
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDToggle : MonoBehaviour
{

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgToggle;

    [SerializeField]
    private string toggleText;

    private string originalText;

    private bool toggled = false;

    [SerializeField]
    private string tooltipText;

    private void Start()
    {
        if (txtComponent != null)
        {
            originalText = txtComponent.text;
        }
    }

    public void Toggle()
    {
        toggled = !toggled;
        if (toggled)
        {
            imgToggle.enabled = true;
            if (txtComponent != null)
            {
                txtComponent.text = toggleText;
            }
        }
        else
        {
            imgToggle.enabled = false;
            if (txtComponent != null)
            {
                txtComponent.text = originalText;
            }
        }
    }

    public void ShowStaticMessage ()
    {
        UIManager.main.ShowMouseMessage(string.Format(tooltipText), false);
    }
}
