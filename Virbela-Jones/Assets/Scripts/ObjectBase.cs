using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    //made base class for sharded functionality between the item and bot 
    public Vector3 position;
    public ObjectManager objectManager;

    protected Renderer objectRenderer;
    protected Color baseColor;
    protected Color highlightColor;

    private bool isHighlighted;

    public void Start()
    {
        objectRenderer = this.gameObject.GetComponent<Renderer>();
    }

    /// <summary>
    /// Initialize a bot or item
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="baseColor"></param>
    /// <param name="highlightColor"></param>
    public virtual void Initialize(ObjectManager manager, Color baseColor, Color highlightColor)
    {
        objectManager = manager;
        objectRenderer = GetComponent<Renderer>();
        this.baseColor = baseColor;
        this.highlightColor = highlightColor;
        objectRenderer.material.color = baseColor;
        position = this.gameObject.transform.position;
    }

    /// <summary>
    /// hightlights the closets object
    /// </summary>
    /// <param name="isHighlighted"></param>
    public void SetHighlight(bool isHighlighted)
    {
        this.isHighlighted = isHighlighted;
        objectRenderer.material.color = isHighlighted ? highlightColor : baseColor;
    }
}
