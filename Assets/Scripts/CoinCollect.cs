using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public int coinCount = 0; // количество монет
    [SerializeField] TextMeshProUGUI coinText;  // ссылка на UI-текст

    private void Start()
    {
        UpdateCoinUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin")) // если столкнулись с монеткой
        {
            AudioManager audioManager = Object.FindFirstObjectByType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlaySound("Coin");
            }

            coinCount++;
            UpdateCoinUI();

            Destroy(other.gameObject); // уничтожаем монету
        }
    }

    private void UpdateCoinUI()
    {
        coinText.text = "Coins: " + coinCount;
    }
}
