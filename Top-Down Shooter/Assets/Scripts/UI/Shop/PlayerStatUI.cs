using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatUI : MonoBehaviour
{
    public int maxLevel = 10;
    public int level;
    public TMP_Text levelText;
    public TMP_Text description;
    
    public UnityEvent<PlayerStat> OnUpgrade;
    PlayerStat stat;

    private void Start()
    {
        stat = FindObjectOfType<PlayerStats>().GetStatOfName(description.text);
    }

    public void AddLevel()
    {
        if(FindObjectOfType<Shop>().ValidateUpgradePurchase() && level < maxLevel)
        {
            level++;
            levelText.text = $"{level}";
            OnUpgrade?.Invoke(stat);
        }
    }
}
