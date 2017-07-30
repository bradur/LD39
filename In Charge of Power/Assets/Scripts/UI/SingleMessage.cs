// Date   : 30.07.2017 14:48
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SingleMessage : MonoBehaviour
{

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    private bool staticMessage = false;
    private bool followMouse = true;

    [SerializeField]
    private Animator animator;

    private RectTransform rectTransform;

    public void Init(Vector2 position, Sprite messageSprite, string messageText, bool staticMessage, bool followMouse)
    {
        this.staticMessage = staticMessage;
        this.followMouse = followMouse;
        rectTransform = GetComponent<RectTransform>();
        
        if (this.staticMessage)
        {
            animator.enabled = false;
        }
        else
        {
            position = new Vector2(position.x - rectTransform.sizeDelta.x / 2, position.y);
        }
        rectTransform.anchoredPosition = position;
        imgComponent.sprite = messageSprite;
        txtComponent.text = messageText;
    }

    public void Init(Vector2 position, Sprite messageSprite, string messageText, bool staticMessage, bool followMouse, float width)
    {
        this.staticMessage = staticMessage;
        this.followMouse = followMouse;
        rectTransform = GetComponent<RectTransform>();
        if (this.staticMessage)
        {
            animator.enabled = false;
        }
        else
        {
            //position = new Vector2(position.x - (rectTransform.sizeDelta.x / 2) * rectTransform.localScale.x, position.y);
        }
        
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        imgComponent.sprite = messageSprite;
        txtComponent.text = messageText;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        if (staticMessage && followMouse)
        {
            rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x - rectTransform.sizeDelta.x / 2, Input.mousePosition.y);
        }
    }
}
