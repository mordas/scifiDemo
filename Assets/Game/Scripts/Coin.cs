using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
//       private AudioSource _pickUpSound;
    [SerializeField] private AudioClip _coinPickup;
//       private void Start()
//       {
//           _pickUpSound = GetComponent<AudioSource>();
//       }
//       

       private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.hasCoin = true;
                    AudioSource.PlayClipAtPoint(_coinPickup,transform.position,1f);
                    Destroy(this.gameObject);
                }
            } 
        }
    }
}
