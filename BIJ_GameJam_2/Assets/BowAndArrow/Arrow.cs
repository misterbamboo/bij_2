using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.BowAndArrow
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField]
        private float Duration = 10.0f;

        [SerializeField]
        private float AmountOfLove = 0.0f;

        [SerializeField]
        private GameObject prefabParticleExplosion;

        private void Awake()
        {
            StartCoroutine(DestroyTimer(gameObject, Duration));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Lover") && other.gameObject.layer != LayerMask.NameToLayer("Floor"))
            {
                return;
            }

            GameManager.Instance.GameEvent(GameProgression.GameEvents.LoverHitByArrow);
       
            var newParticle = Instantiate(prefabParticleExplosion);
            newParticle.gameObject.transform.position = gameObject.transform.position;

            StartCoroutine(DestroyTimer(newParticle.gameObject, Duration / 2));

            var loverMeter = other.GetComponentInChildren<LoveMeter>();
            loverMeter?.ModifyLove(AmountOfLove);
        }

        private IEnumerator DestroyTimer(GameObject _gameObject, float duration)
        {
            yield return new WaitForSeconds(duration);
            Destroy(_gameObject);
        }
    }
}
