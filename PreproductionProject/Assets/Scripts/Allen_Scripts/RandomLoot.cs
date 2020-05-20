using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    [System.Serializable]
    public class DropCurrency
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }

    public List<DropCurrency> LootTable = new List<DropCurrency> ();
    public int dropChance;
    public void calculateLoot(Vector3 dropPosition)
    {
        Debug.Log("Loot");

        int calc_dropChance = Random.Range(0, 101);

        if (calc_dropChance > dropChance)
        {
            Debug.Log("No Loot");
            return;
        }

        if (calc_dropChance <= dropChance)
        {
            int itemWeight = 0;

            for (int i = 0; i < LootTable.Count; i++)
            {
                itemWeight += LootTable[i].dropRarity;
            }
            Debug.Log("ItemWeight= " + itemWeight);

            int randomValue = Random.Range(0, itemWeight);

            for (int j = 0; j < LootTable.Count; j++)
            {
                if (randomValue <= LootTable[j].dropRarity)
                {
                    Instantiate(LootTable[j].item, dropPosition, Quaternion.identity);
                    return;
                }
                randomValue -= LootTable[j].dropRarity;
                Debug.Log("Random Value decreased" + randomValue);
            }
        }
    }
}
