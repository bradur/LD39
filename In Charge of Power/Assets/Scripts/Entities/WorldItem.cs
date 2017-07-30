// Date   : 29.07.2017 11:53
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class WorldItem : MonoBehaviour
{

    [SerializeField]
    private SpriteOutline spriteOutline;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private SpriteRenderer spriteRendererInputIcon;

    [SerializeField]
    private TextMesh textMeshInputCount;

    [SerializeField]
    private SpriteRenderer spriteRendererOutputIcon;

    [SerializeField]
    private TextMesh textMeshOutputCount;

    [SerializeField]
    [Range(2, 10)]
    private int minSize = 4;
    public int MinSize { get { return minSize; } }

    private bool placed = false;

    private int inputCostValue;

    private int outputGenerationValue;

    private GameItem gameItem;

    [SerializeField]
    private Color inactiveColor;

    private Color originalColor;

    private bool active = true;

    public void Init(GameItem item, int inputCount, int outputCount)
    {
        originalColor = Color.white;
        gameItem = item;
        spriteRenderer.sprite = gameItem.sprite;
        spriteRendererInputIcon.sprite = ResourceManager.main.GetResource(gameItem.inputType).sprite;
        spriteRendererOutputIcon.sprite = ResourceManager.main.GetResource(gameItem.outputType).sprite;
        inputCostValue = inputCount;
        outputGenerationValue = outputCount;
        textMeshInputCount.text = "" + inputCount;
        textMeshOutputCount.text = "" + outputCount;
        boxCollider2D.enabled = false;
    }

    public void Place(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.localPosition = position;
        placed = true;
        boxCollider2D.enabled = true;
    }

    private void ItemInactive()
    {
        spriteRenderer.color = inactiveColor;
        spriteRendererInputIcon.color = inactiveColor;
        spriteRendererOutputIcon.color = inactiveColor;
        active = false;
    }

    private void ItemActive()
    {
        spriteRenderer.color = originalColor;
        spriteRendererInputIcon.color = originalColor;
        spriteRendererOutputIcon.color = originalColor;
        active = true;
    }

    void Start()
    {

    }

    [SerializeField]
    [Range(0.2f, 5f)]
    private float outputGenerationInterval = 1f;

    private float outputGenerationTimer = 0f;

    void Update()
    {
        if (placed && active)
        {
            if (outputGenerationTimer < outputGenerationInterval)
            {
                outputGenerationTimer += Time.deltaTime;
            }
            else
            {
                if (ResourceManager.main.WithdrawResource(inputCostValue, gameItem.inputType))
                {
                    outputGenerationTimer = 0f;
                    ResourceManager.main.AddResource(outputGenerationValue, gameItem.outputType);
                }
                else
                {
                    ItemInactive();
                }
            }
        }
        else if (placed && !active)
        {
            if (ResourceManager.main.WithdrawResource(inputCostValue, gameItem.inputType))
            {
                outputGenerationTimer = 0f;
                ResourceManager.main.AddResource(outputGenerationValue, gameItem.outputType);
                ItemActive();
            }
        }
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseEnter()
    {
        CursorManager.main.SetCursor(CursorType.Pointer);
        spriteOutline.EnableOutline();
    }

    private void OnMouseExit()
    {
        CursorManager.main.SetCursor(CursorType.Default);
        spriteOutline.DisableOutline();
    }

    private void OnMouseUp()
    {

    }



}
