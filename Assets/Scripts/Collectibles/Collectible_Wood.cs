using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Wood: MonoBehaviour, ICollectible {
    // ıı
    [PunRPC]
    public void Collect(int playerId)
    {
        EventsManager.OnCollectedCollectible?.Invoke(playerId);
        if(PhotonNetwork.IsMasterClient)
        {
            GameManager.instance.SpawnNewWood();
            AudioManager.Instance.PlaySFX("Collect");
            PhotonNetwork.Destroy(gameObject);
        }
  
    }
}
