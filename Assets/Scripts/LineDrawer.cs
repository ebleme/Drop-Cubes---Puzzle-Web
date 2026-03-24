using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour
{
    /*
     * Line renderer
     *  Çizgi çiz. Başarılıda çizmeye devam et. başarısızda çizgiyi kapat. Mouse - ButtonDown, Mouse Drag, MouseButtonUp
     * En az 3 item
     * başarılı Durumları
     *  Aynı türden en az 3 item seçili ise
     *  Seçilen Item lar yok olacak
     * Başarısızı Durumları
     *  Farklı Item seçilirse veya mesafe uzaksa
     */

    [SerializeField] private int minItemCountToSuccess = 3;
    [SerializeField] private float maxDistance = 3;

    private LineRenderer lineRenderer;

    List<Item> selectedItems = new List<Item>();
    ItemTypes selectedItemType;

    
    int totalScore = 0;

    [SerializeField] private Text textScore;
    
    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButton(0))
        {
            UpdateDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrawing();
        }
    }

    private void StartDrawing()
    {
        selectedItems.Clear();

        var mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;


        var hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Item"))
        {
            Item item = hit.collider.transform.parent.GetComponent<Item>();
            selectedItemType = item.ItemType;

            selectedItems.Add(item);

            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
        }

        // çizgiyi Sürükleme başlat
    }

    private void UpdateDrawing()
    {
        var mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;


        var hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Item"))
        {
            Item item = hit.collider.transform.parent.GetComponent<Item>();
            if (item.ItemType == selectedItemType)
            {
                // Aynı item üzerine ne olacak

                if (selectedItems.Contains(item))
                    return;


                // Eğer mesafe fazlaysa yine iptal edilecek
                if (selectedItems.Count > 0)
                {
                    var lastItem = selectedItems[selectedItems.Count - 1];

                    float distance = Vector2.Distance(lastItem.transform.position, item.transform.position);
                    if (distance > maxDistance)
                    {
                        Debug.Log("Mesafe çok uzundu");
                        selectedItems.Clear();
                        EndDrawing();
                        return;
                    }
                }

                selectedItems.Add(item);

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, item.transform.position);
            }
            else
            {
                selectedItems.Clear();

                EndDrawing();
            }
        }
    }

    private void EndDrawing()
    {
        lineRenderer.positionCount = 0;

        // Çizgi çekme işlemini bitir
        // Başarılı mı başarısız mı test et

        if (selectedItems.Count >= minItemCountToSuccess)
        {
            ScoreWrite();
            Debug.Log("Başarılı");
            foreach (var item in selectedItems)
            {
                item.Kill();
            }

            selectedItems.Clear();
        }
        else
        {
            Debug.Log("Başarısız");
        }

        lineRenderer.positionCount = 0;
    }

    private void ScoreWrite()
    {
        int factor = selectedItems.Count - 2; 
        
        int score = selectedItems.Count * factor;
        totalScore += score;
        Debug.Log("+" + score + " puan!");
        
        textScore.text = "Score: " + totalScore;
    }
}