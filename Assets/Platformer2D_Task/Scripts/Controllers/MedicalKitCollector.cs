using System;
using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MedicalKitCollector : MonoBehaviour
    {
        private const CollectableTypes TypeToCollect = CollectableTypes.MedicalKit;

        [SerializeField] private int _startAmountOfKits;
        [SerializeField] private int _maxAmountOfKits;

        private readonly Wallet _wallet = new Wallet();

        public Action<int> NumberOfKitsChanged;            

        public int MedicalKits => _wallet.NumberOfCoins;

        void Awake()
        {
            _wallet.Initialize(_startAmountOfKits);
            _wallet.NumberOfCoinsChanged += (boxes) => NumberOfKitsChanged?.Invoke(boxes);
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
            if (CheckIsCollectable(gameObject, out ICollectable collectable) &&  (MedicalKits < _maxAmountOfKits))
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
            if (_startAmountOfKits < 0)
            {
                _startAmountOfKits = 0;
            }

            if (_maxAmountOfKits < _startAmountOfKits)
            {
                _maxAmountOfKits = _startAmountOfKits;
            }

            if (_startAmountOfKits > _maxAmountOfKits)
            {
                _maxAmountOfKits = _startAmountOfKits;
            }
        }
    }
}