using UnityEngine;
using TMPro;

public class TowerUIButton : MonoBehaviour, ITappable {

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected TextMeshPro priceText;
    [SerializeField]
    protected Sprite icon;

    public void SetPrice(int price)
    {
        priceText.SetText("{0}", price);
    }

    public void ResetIcon()
    {
        if (icon != null) spriteRenderer.sprite = icon;
    }

    public void Select()
    {
        spriteRenderer.sprite = TowerUI.Instance.ConfirmCheckSprite;
    }

    public virtual void OnTap(bool inBounds)
    {

    }

    public virtual void OnTouch()
    {

    }
}
