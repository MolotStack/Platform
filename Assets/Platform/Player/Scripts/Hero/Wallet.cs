
public class Wallet
{
    public Wallet(int startCoin = 0)
    {
        _coin = startCoin;
    }

    public int Coin => _coin;

    private int _coin;

    public void AddCoin(int coins)
    {
        _coin += coins;
    }

    public void RemoveCoin(int coins)
    {
        if (coins > _coin)
        {
            return;
        }

        _coin -= coins;
    }
}
