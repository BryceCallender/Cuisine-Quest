using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public Image weaponImage;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI potionCountText;
    public CiscoTesting player;

    private int potionCount = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CiscoTesting>();
        descriptionText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () 
    {
        if(player.items != null && player.items.Count > 0)
        {
            if (player.items.ContainsKey(player.potion))
            {
                potionCount = player.items[player.potion];
            }
        }

        potionCountText.SetText(string.Format("x{0}", potionCount));
        //The true in this statement allows this to get components in children
        //even if they are disabled on disabled gameobjects
        weaponImage.sprite = player.CurrentWeapon.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite;

        switch(weaponImage.sprite.name)
        {
            case "Sword":
            case "Trident":
                descriptionText.gameObject.SetActive(false);
                break;
            case "Knife":
                descriptionText.gameObject.SetActive(true);
                descriptionText.SetText(string.Format("x{0}", player.CurrentWeapon.GetComponent<Knife>().KnifeCount));
                break;
        }
	}
}
