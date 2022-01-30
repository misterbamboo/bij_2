using System.Collections;
using System.Linq;
using Assets.Player.Scripts.SpecialItems.Items;
using Assets.SharedKernel.Models;
using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems
{
    public class SpecialItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] SpecialItemsPrefab;

        private RarityCollection<SpecialItem> SpecialItems;
        
        private void Awake()
        {
            SpecialItems = new RarityCollection<SpecialItem>(SpecialItemsPrefab.Select(x => x.GetComponent<SpecialItem>()));
        }

        private void Start()
        {
            StartCoroutine(Spawn(0));
        }
        
        public void InitiateSpawn()
        {
            StartCoroutine(Spawn(10));
        }

        private IEnumerator Spawn(int waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            var item = SpecialItems.PickRandom();

            var specialItem = Instantiate(item, transform.position, transform.rotation, transform);

            if (specialItem != null)
            {
                specialItem.Spawner = this;
            }
        }
    }
}
