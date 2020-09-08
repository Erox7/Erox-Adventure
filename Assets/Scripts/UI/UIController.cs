using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject gameOverScreen;

    public Slider manaBar;
    public Slider hpBar;
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
        GlobalEventManager.Instance.onManaDecrease += DecreaseMana;
        GlobalEventManager.Instance.onManaIncrease += IncreaseMana;
        GlobalEventManager.Instance.onHpDecrease += DecreaseHp;
        GlobalEventManager.Instance.onHpIncrease += IncreaseHp;
        GlobalEventManager.Instance.onGameOver += ActivateGOMenu;
    }
    public void ActivateGOMenu()
    {
        gameOverScreen.SetActive(true);
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
    void IncreaseMana(float mana)
    {
        manaBar.value += mana;
    }
    void DecreaseMana(float mana)
    {
        manaBar.value -= mana;
    }

    void IncreaseHp(float hp)
    {
        hpBar.value += hp;
    }
    void DecreaseHp(float hp)
    {
        hpBar.value -= hp;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("InitialMenuScene");
    }
    private void OnDestroy()
    {
        PressedKeyEventManager.Instance.onInventoryKeyPress -= TriggerInventory;
        inventory.onItemChangedCallback -= UpdateUI;
        GlobalEventManager.Instance.onManaDecrease -= DecreaseMana;
        GlobalEventManager.Instance.onManaIncrease -= IncreaseMana;
        GlobalEventManager.Instance.onHpDecrease -= DecreaseHp;
        GlobalEventManager.Instance.onHpIncrease -= IncreaseHp;
    }
}
