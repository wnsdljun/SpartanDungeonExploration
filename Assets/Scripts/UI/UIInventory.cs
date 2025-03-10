using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public InventorySlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;

    private InventorySlot selectedSlot;
    private int selectedSlotIndex;

    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;

    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int currentEquipItemIndex;

    private PlayerController controller;
    private PlayerConditions conditions;

    private Vector3 GetDropPos()
    {
        Transform playerPos = CharacterManager.Instance.Player.transform;
        Vector3 dropPos = playerPos.position + (playerPos.forward * 0.5f) + (Vector3.up * 0.5f);
        return dropPos;
    }

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        conditions = CharacterManager.Instance.Player.conditions;

        controller.playerInventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new InventorySlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<InventorySlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }

        ClearSelectedItemWindow();
    }

    void ClearSelectedItemWindow()
    {
        selectedSlot = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        if (data.isStackable)
        {
            InventorySlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        InventorySlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    InventorySlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackCount)
            {
                return slots[i];
            }
        }
        return null;
    }

    InventorySlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void ThrowItem(ItemData data)
    {

        Instantiate(data.dropPrefab, GetDropPos(), Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedSlot = slots[index];
        selectedSlotIndex = index;

        selectedItemName.text = selectedSlot.item.displayName;
        selectedItemDescription.text = selectedSlot.item.description;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for (int i = 0; i < selectedSlot.item.consumables.Length; i++)
        {
            selectedItemStatName.text += selectedSlot.item.consumables[i].type.ToString() + "\n";
            selectedItemStatValue.text += selectedSlot.item.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedSlot.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedSlot.item.type == ItemType.Equippable && !slots[index].equipped);
        unEquipButton.SetActive(selectedSlot.item.type == ItemType.Equippable && slots[index].equipped);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if (selectedSlot.item.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedSlot.item.consumables.Length; i++)
            {
                switch (selectedSlot.item.consumables[i].type)
                {
                    case ConsumableType.Health:
                        conditions.Heal(selectedSlot.item.consumables[i].value); break;
                    case ConsumableType.Hunger:
                        conditions.Eat(selectedSlot.item.consumables[i].value); break;
                }
            }
            RemoveSelctedItem();
        }
    }

    public void OnDropButton()
    {
        ThrowItem(selectedSlot.item);
        RemoveSelctedItem();
    }

    void RemoveSelctedItem()
    {
        selectedSlot.quantity--;

        if (selectedSlot.quantity <= 0)
        {
            if (slots[selectedSlotIndex].equipped)
            {
                //
                //UnEquip(selectedSlotIndex);
            }

            selectedSlot.item = null;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public bool HasItem(ItemData item, int quantity)
    {
        return false;
    }
}
