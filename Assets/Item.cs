using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private bool isSelected = false;

    public void Select()
    {
        isSelected = true;

        spriteRenderer.color = Color.gray;

    }

    public void Deselect()
    {
        isSelected = false;
        spriteRenderer.color = Color.white;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

}
