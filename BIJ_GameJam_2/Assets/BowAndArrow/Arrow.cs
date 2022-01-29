using System;
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
        private float Amount = 0.0f;

        private void OnTriggerEnter(Collider other)
        {
            var loverMeter = other.GetComponent<LoveMeter>();

            if (loverMeter != null)
            {
                loverMeter.ModifyLove(Amount);
            }
        }
    }
}
