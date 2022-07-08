using System.Collections;
using UnityEngine;

    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private Store _store;
        [SerializeField] private Money _moneyTemplate;
        [SerializeField] private Transform _point;

        private const float Delay = 0.3f;

        private void OnEnable()
        {
            _store.Sold += Spawn;
        }

        private void OnDisable()
        {
            _store.Sold -= Spawn;            
        }

        private void Spawn(int count)
        {
            StartCoroutine(MoneyGenerator(count));   
        }

        private IEnumerator MoneyGenerator(int count)
        {
            var delay = new WaitForSeconds(Delay);

            for (int i = 0; i < count; i++)
            {
                yield return delay;
                Money money = Instantiate(_moneyTemplate, _point.position, transform.rotation, transform);
            }
        }
    }

