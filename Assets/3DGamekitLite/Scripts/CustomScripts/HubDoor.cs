
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit3D
{
    public class HubDoor : MonoBehaviour
    {
        [Header("Inventory Requirements")]
        public string[] requiredInventoryItemKeys;
        public InventoryController characterInventory;

        [Header("Door Visuals")]
        public Sprite[] unlockStateSprites;
        public UnityEvent onUnlocked;

        private SpriteRenderer spriteRenderer;
        private bool isUnlocked;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (characterInventory == null)
            {
                characterInventory = FindFirstObjectByType<InventoryController>();
            }

            CheckInventory();
        }
        
        private void CheckInventory()
        {
            if (characterInventory == null)
            {
                characterInventory = FindFirstObjectByType<InventoryController>();
            }

            if (characterInventory == null)
            {
                Debug.LogWarning("HubDoor: No InventoryController found!");
                return;
            }

            int keysCollected = 0;

            foreach (string key in requiredInventoryItemKeys)
            {
                bool hasKey = characterInventory.HasItem(key);

                //Debug.Log("HubDoor checking key: " + key + " | Has key: " + hasKey);

                if (hasKey)
                    keysCollected++;
            }

            //Debug.Log("HubDoor: " + keysCollected + " / " + requiredInventoryItemKeys.Length + " keys collected.");

            if (keysCollected >= requiredInventoryItemKeys.Length)
            {
                UnlockDoor();
            }
        }

        private void UnlockDoor()
        {
            if (isUnlocked)
                return;

            isUnlocked = true;
            onUnlocked?.Invoke();

            Debug.Log("Boss door unlocked!");
        }
    }
}
