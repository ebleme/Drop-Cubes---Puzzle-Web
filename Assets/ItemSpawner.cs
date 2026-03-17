using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    // Pozisyon aralığı
    // Birçok Item listesi
    // Belirli bir zaman aralığında
    // Pozisyon aralığında rastgele b ir pozisyon seçmeli
    // Item listesinden rastgele bir tane seçmeli
    // Seçilen Item'ı seçilen pozisyonda spawn etmeli. (Item kendi kendine düşecek)

    // Seçtiği pozisyon doluysa boş olan bir pozisyonda spawn etmesi gerekir.


    [SerializeField] private float xMin = -9f;
    [SerializeField] private float xMax = 9f;

    [SerializeField] private List<Item> itemPrefabs;
    [SerializeField] private float spawnTime = 1f;


    // Spawn Döngüsel sürekli çalışacak
    // Her 1 saniyede 1 spawn edecek

    private float spawnTimer = 0;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {
            // Spawn yapma zamanı geldi

            // Spawn yap
            SpawnItem();
            spawnTimer = 0;
        }
    }

    private void SpawnItem()
    {
        float randomX = Random.Range(xMin, xMax);

        int randomIndex = Random.Range(0, itemPrefabs.Count);
        Item randomPrefab = itemPrefabs[randomIndex];

        Item instantiatedItem = Instantiate(randomPrefab, new Vector3(randomX, 6f, 0f), Quaternion.identity);
    }
}