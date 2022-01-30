using Assets.SharedKernel.Models;
using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public abstract class SpecialItem : MonoBehaviour, IHasRarity
    {
        public const int Delay = 10;

        public abstract int Rarity { get; }

        public abstract int RespawnDelay { get; }

        public SpecialItemSpawner Spawner;

        protected void OnDestroy()
        {
            Spawner.InitiateSpawn(RespawnDelay);
        }
    }
}
