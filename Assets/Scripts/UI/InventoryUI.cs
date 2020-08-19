using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public static bool gameIsPaused = false;
    Inventory inventory;
    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        PressedKeyEventManager.Instance.onInventoryKeyPress += TriggerInventory;
    }
    public void TriggerInventory()
    {
        if (gameIsPaused)
        {
            Resume();
        } else
        {
            Pause();
        }
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI ()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
    private void OnDestroy()
    {
        PressedKeyEventManager.Instance.onInventoryKeyPress -= TriggerInventory;
        inventory.onItemChangedCallback -= UpdateUI;
    }
}
