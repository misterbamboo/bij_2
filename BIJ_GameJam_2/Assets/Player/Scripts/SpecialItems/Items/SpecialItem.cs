using Assets.SharedKernel.Models;
using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public abstract class SpecialItem : MonoBehaviour, IHasRarity
    {
        public abstract int Rarity { get; }

        public SpecialItemSpawner Spawner;

        protected void OnDestroy()
        {
            Spawner.InitiateSpawn();
        }
    }
}
