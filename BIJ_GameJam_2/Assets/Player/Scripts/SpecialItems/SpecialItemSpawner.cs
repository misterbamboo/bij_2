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

        [SerializeField] private float height = 3.0f;

        private RarityCollection<SpecialItem> SpecialItems;

        private void Awake()
        {
            SpecialItems = new RarityCollection<SpecialItem>(SpecialItemsPrefab.Select(x => x.GetComponent<SpecialItem>()));
        }

        private void Start()
        {
            StartCoroutine(Spawn(0));
            FixHeight();
        }

        public void InitiateSpawn(int waitTime)
        {
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(Spawn(waitTime));
            }
        }

        private IEnumerator Spawn(int waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            if (gameObject.activeInHierarchy)
            {
                var item = SpecialItems.PickRandom();

                var specialItem = Instantiate(item, transform.position, transform.rotation, transform);

                if (specialItem != null)
                {
                    specialItem.Spawner = this;
                }
            }
        }

        private void FixHeight()
        {
            var origin = transform.position;

            var targetHight = height;

            var layerMask = LayerMask.GetMask("Floor");
            if (Physics.Raycast(origin, -Vector3.up, out RaycastHit rayHit, 100f, layerMask /* terrain layer*/))
            {
                targetHight = rayHit.point.y + height;
            }

            var destination = origin;
            destination.y = targetHight;
            Debug.DrawLine(origin, destination, Color.green);

            var targetPos = transform.position;
            targetPos.y = targetHight;

            transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}
