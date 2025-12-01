using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int coins = 0;

    private void Awake()
    {
        
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            Debug.Log("Coins: " + coins);
            return true;
        }

        Debug.Log("Not enough coins!");
        return false;
    }

    public class SellItem : MonoBehaviour
    {
        public int value = 10; 

        public void Sell()
        {
            CoinManager.Instance.AddCoins(value);
        }
    }
}
