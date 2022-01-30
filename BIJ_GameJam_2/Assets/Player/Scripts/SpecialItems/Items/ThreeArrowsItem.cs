using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public class ThreeArrowsItem : SpecialItem
    {
        [SerializeField] public GameObject textPrefab;

        public override int Rarity => 5;
        public override int RespawnDelay => 0;

        [SerializeField] private float BoosterTime;

        private Bow bowInstance;

        [SerializeField] private Collider collider;
        [SerializeField] private Renderer renderer;
        
        void Start()
        {
            textPrefab = GameObject.FindWithTag("ThreeArrowsItem");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player")
            {
                return;
            }

            BoosterTime += Delay;

            bowInstance = other.GetComponentInChildren<Bow>();
            bowInstance.UseThreeArrows = true;

            StartCoroutine(CheckSpecialItemTime());

            collider.enabled = false;
            renderer.enabled = false;
        }

        private IEnumerator CheckSpecialItemTime()
        {
            var timeTextDelay = 2.0f;
            var textMeshPro = textPrefab.GetComponent<TextMeshProUGUI>();
            textMeshPro.text = "+3 arrows";
            yield return new WaitForSeconds(timeTextDelay);
            textMeshPro.text = "";
            yield return new WaitForSeconds(BoosterTime - timeTextDelay);

            bowInstance.UseThreeArrows = false;
            Destroy(gameObject);
        }
    }
}
