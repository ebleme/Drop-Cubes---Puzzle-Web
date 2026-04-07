using System;
using DefaultNamespace;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemTypes itemType;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isSelected = false;

    public ItemTypes ItemType => itemType;

    bool touchSFXPlayed = false;
    
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

    // Item ların birşeylere temas ettiğini algıla
    // Bir kere çalsın diyebiliriz veya her temas sonrası sesi azalt
    // Zemine ve diğer Item lara temas edebilir.
    // Temastan sonra ses çalmamız gerekir


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (touchSFXPlayed)
        {
            return;
        }
        
        if (other.transform.CompareTag("Item") || other.transform.CompareTag("Floor"))
        {
            Debug.Log("Item a temas etti: " + other.transform.name);
            
            touchSFXPlayed = true;
            // SoundManager'ı reference alma meselemiz var
           var soundManager = FindAnyObjectByType<SoundManager>();
           soundManager.PlayItemTouchSFX();
        }
        
     
    }
}