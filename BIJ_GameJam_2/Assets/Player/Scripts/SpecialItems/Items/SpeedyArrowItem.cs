﻿using System.Collections;
using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public class SpeedyArrowItem : SpecialItem
    {
        public override int Rarity => 5;
        public override int RespawnDelay => 0;

        [SerializeField] private float BoosterTime;

        private Bow bowInstance;

        [SerializeField] private Collider collider;
        [SerializeField] private Renderer renderer;

        private void OnTriggerEnter(Collider other)
        {
            BoosterTime += Delay;

            bowInstance = other.GetComponentInChildren<Bow>();
            bowInstance.UseSpeedyArrows = true;

            StartCoroutine(CheckSpecialItemTime());

            collider.enabled = false;
            renderer.enabled = false;
        }

        private IEnumerator CheckSpecialItemTime()
        {
            yield return new WaitForSeconds(BoosterTime);

            bowInstance.UseSpeedyArrows = false;
            Destroy(gameObject);
        }
    }
}