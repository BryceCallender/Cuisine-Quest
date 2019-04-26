using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public Image weaponImage;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI potionCountText;
    public CiscoTesting player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CiscoTesting>();
        descriptionText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () 
    {
        potionCountText.SetText(string.Format("x{0}", player.potions.Count));
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
