using System;
using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BoxCollector : MonoBehaviour
    {
        private const CollectableTypes TypeToCollect = CollectableTypes.Box;

        [SerializeField] private int _startAmountOfCoins;

        private readonly Wallet _wallet = new Wallet();

        public Action<int> NumberOfCoinsChanged;
            

        public int GearBoxes => _wallet.NumberOfCoins;

        void Awake()
        {
            _wallet.Initialize(_startAmountOfCoins);
            _wallet.NumberOfCoinsChanged += (boxes) => NumberOfCoinsChanged?.Invoke(boxes);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryCollectItem(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            TryCollectItem(collider.gameObject);
        }

        private void TryCollectItem(GameObject gameObject)
        {
            if (CheckIsCollectable(gameObject, out ICollectable collectable))
            {
                Collect(collectable);
            }
        }

        private bool CheckIsCollectable(GameObject gameObject, out ICollectable collectable)
        {
            if (gameObject.TryGetComponent(out collectable))
            {
                return true;
            }

            return false;
        }

        private void Collect(ICollectable collectable)
        {
            if (collectable.CollectableType != TypeToCollect)
            {
                return;
            }

            var coins = collectable.NumberOfObjects;
            _wallet.PutCoins(coins);

            collectable.Take();
        }

        private void OnValidate()
        {
            if (_startAmountOfCoins < 0)
            {
                _startAmountOfCoins = 0;
            }
        }
    }
}