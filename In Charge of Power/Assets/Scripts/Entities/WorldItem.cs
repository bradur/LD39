// Date   : 29.07.2017 11:53
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
    public GameItem GameItem { get { return gameItem; } }

    [SerializeField]
    private Color inactiveColor;

    private Color originalColor;

    private bool active = true;
    public bool CanProduce { get { return active; } }

    private int cost = 0;

    private string itemName = "";

    private MeshCollisionHandler placementTarget;

    public void Init(GameItem item, int inputCount, int outputCount, int cost, string itemName)
    {
        originalColor = Color.white;
        gameItem = item;
        this.cost = cost;
        this.itemName = itemName;
        spriteRenderer.sprite = gameItem.sprite;
        spriteRendererInputIcon.sprite = ResourceManager.main.GetResource(gameItem.inputType).sprite;
        spriteRendererOutputIcon.sprite = ResourceManager.main.GetResource(gameItem.outputType).sprite;
        inputCostValue = inputCount;
        outputGenerationValue = outputCount;
        textMeshInputCount.text = "" + inputCount;
        textMeshOutputCount.text = "" + outputCount;
        boxCollider2D.enabled = false;
    }

    public void Place(Vector3 position, MeshCollisionHandler placementTarget)
    {
        this.placementTarget = placementTarget;
        gameObject.SetActive(true);
        transform.localPosition = position;
        placed = true;
        boxCollider2D.enabled = true;
        Produce();
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

    [SerializeField]
    private Transform outPutMessagePosition;

    [SerializeField]
    private Transform inputMessagePosition;

    private bool Produce()
    {
        if (placed && ResourceManager.main.WithdrawResource(inputCostValue, gameItem.inputType))
        {
            ResourceManager.main.AddResource(outputGenerationValue, gameItem.outputType);
            UIManager.main.ShowResourceMessage(
                outPutMessagePosition.position,
                outputGenerationValue,
                gameItem.outputType
            );
            return true;
        }
        else
        {
            ItemInactive();
        }
        return false;
    }

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
                if (Produce())
                {
                    outputGenerationTimer = 0f;
                }
            }
        }
        else if (placed && !active)
        {
            if (Produce())
            {
                outputGenerationTimer = 0f;
                ItemActive();
            }
        }
    }

    public float GetOutputRate()
    {
        return outputGenerationValue / outputGenerationInterval;
    }

    public float GetInputRate()
    {
        return -inputCostValue / outputGenerationInterval;
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseEnter()
    {
        if (!GameManager.main.GameIsOver)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !PlacementManager.main.IsPlacing)
            {
                UIManager.main.ShowMouseMessage(string.Format("Sell {0} for ${1}", itemName, cost / 2));
                CursorManager.main.SetCursor(CursorType.Pointer);
                spriteOutline.EnableOutline();
            }
        }
    }

    private void OnMouseExit()
    {
        CursorManager.main.SetCursor(CursorType.Default);
        UIManager.main.ClearStaticMessage();
        spriteOutline.DisableOutline();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnMouseUp()
    {
        if (!GameManager.main.GameIsOver)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && placed && !PlacementManager.main.IsPlacing)
            {
                ResourceManager.main.AddResource(cost / 2, ResourceType.Money);
                PlacementManager.main.RemovePlacedItem(this);
                placementTarget.Clear();
                Kill();
            }
        }
    }



}
