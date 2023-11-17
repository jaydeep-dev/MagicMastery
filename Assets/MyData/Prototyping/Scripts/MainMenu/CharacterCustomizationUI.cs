using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizationUI : MonoBehaviour
{
    [SerializeField] private GameObject characterCustomizationUI;

    [Header("Character Toggles")]
    [SerializeField] private ToggleGroup characterToggles;

    [SerializeField] private Toggle helmetToggle;
    [SerializeField] private Toggle armorToggle;
    [SerializeField] private Toggle gaugletToggle;
    [SerializeField] private Toggle bootsToggle;
    [SerializeField] private Toggle ringToggle;
    [SerializeField] private Toggle necklaceToggle;

    [Header("Items List")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<Sprite> helmetSprites;
    [SerializeField] private List<Sprite> armorSprite;
    [SerializeField] private List<Sprite> gaungletSprites;
    [SerializeField] private List<Sprite> bootsSprite;
    [SerializeField] private List<Sprite> ringSprites;
    [SerializeField] private List<Sprite> necklaceSprites;

    [Header("Item View")]
    [SerializeField] private Transform itemContainer;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in itemContainer.transform)
        {
            Destroy(child.gameObject);
        }
        characterCustomizationUI.SetActive(false);
    }

    public void OnCharacterToggleChanged()
    {
        var selectedToggle = characterToggles.GetFirstActiveToggle();
        var itemList = new List<Sprite>();
        if(selectedToggle == helmetToggle)
        {
            itemList.AddRange(helmetSprites);
        }
        else if(selectedToggle == armorToggle)
        {
            itemList.AddRange(armorSprite);
        }
        else if(selectedToggle == gaugletToggle)
        {
            itemList.AddRange(gaungletSprites);
        }
        else if (selectedToggle == bootsToggle)
        {
            itemList.AddRange(bootsSprite);
        }
        else if (selectedToggle == ringToggle)
        {
            itemList.AddRange(ringSprites);
        }
        else if (selectedToggle ==  necklaceToggle)
        {
            itemList.AddRange(necklaceSprites);
        }
        else
        {
            Debug.LogError("Undefined Character Toggle");
        }

        foreach (Transform child in itemContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var itemSprite in itemList)
        {
            var itemObj = Instantiate(itemPrefab, itemContainer);
            itemObj.transform.GetChild(0).GetComponent<Image>().sprite = itemSprite;
            itemObj.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            itemObj.GetComponentInChildren<Button>().onClick.AddListener(() => EquipItem(itemObj));
        }
    }

    public void EquipItem(GameObject item)
    {
        var selectedToggle = characterToggles.GetFirstActiveToggle();
        var itemSprite = item.transform.GetChild(0).GetComponent<Image>().sprite;
        selectedToggle.GetComponent<Image>().sprite = itemSprite;
    }
}
