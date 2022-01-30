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
            print("I have spawned!!!!");
            Spawn();
        }

        private void Spawn()
        {
            var item = SpecialItems.PickRandom();

            var arrow = Instantiate(item, transform.position, transform.rotation, transform);
        }
    }
}
