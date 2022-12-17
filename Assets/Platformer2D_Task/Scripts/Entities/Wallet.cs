using System;

namespace Platformer2D_Task
{
    public class Wallet
    {
        private const int MinNumberOfCoins = 0;

        public Action<int> NumberOfCoinsChanged;

        private int _numberOfCoins;

        public int NumberOfCoins => _numberOfCoins;

        public void Initialize (int startNumberOfCoins)
        {
            _numberOfCoins = startNumberOfCoins > MinNumberOfCoins ? 
                startNumberOfCoins : 
                MinNumberOfCoins;
        }

        public void PutCoins(int coins = 1)
        {
            if (coins > 0)
            {
                _numberOfCoins += coins;

                NumberOfCoinsChanged?.Invoke(_numberOfCoins);
            }
        }
    }
}
