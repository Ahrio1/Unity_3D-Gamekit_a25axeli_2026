using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class DeathVolume : MonoBehaviour
    {
        public new GameObject audioObject;


        void OnTriggerEnter(Collider other)
        {
            var pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.Die(new Damageable.DamageMessage());
            }
            if (audioObject != null)
            {
                audioObject.transform.position = other.transform.position;
            }
            
            //Checks if the GameObject is an enemy and kills it!
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("Entered deathvolume: " + other.name);
                var dam = other.GetComponent<Damageable>();
                dam.OnDeath.Invoke();
            }
        }

        void Reset()
        {
            if (LayerMask.LayerToName(gameObject.layer) == "Default")
                gameObject.layer = LayerMask.NameToLayer("Environment");
            var c = GetComponent<Collider>();
            if (c != null)
                c.isTrigger = true;
        }

    }
}
