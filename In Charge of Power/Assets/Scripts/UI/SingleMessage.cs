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

    private bool right = false;

    public void Init(Vector2 position, Sprite messageSprite, string messageText, bool staticMessage, bool followMouse, bool right)
    {
        this.right = right;
        this.staticMessage = staticMessage;
        this.followMouse = followMouse;
        rectTransform = GetComponent<RectTransform>();

        if (this.staticMessage)
        {
            animator.enabled = false;
        }
        if (!right)
        {
            position = new Vector2(position.x, position.y - rectTransform.sizeDelta.y / 2);
        }
        if (right)
        {
            position = new Vector2(position.x - rectTransform.sizeDelta.x, Input.mousePosition.y - rectTransform.sizeDelta.y / 2);
        }
        if (messageText.Length > 40)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + 30f);
        }
        rectTransform.anchoredPosition = position;
        imgComponent.sprite = messageSprite;
        txtComponent.text = messageText;
    }

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
            position = new Vector2(position.x - rectTransform.sizeDelta.x / 2, position.y - rectTransform.sizeDelta.y / 2);
        }
        if (messageText.Length > 40)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + 30f);
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
        if (messageText.Length > 40)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + 30f);
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
            if (!right)
            {
                rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y - rectTransform.sizeDelta.y / 2);
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x - rectTransform.sizeDelta.x, Input.mousePosition.y - rectTransform.sizeDelta.y / 2);
            }
        }
    }
}
