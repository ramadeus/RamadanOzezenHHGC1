using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdentity : MonoBehaviour
{
    // ıı
    [SerializeField] GameObject localMeshes;
    [SerializeField] GameObject remoteMeshes;
    public CharType charType;
    public int responsiblePlayer;
    PhotonView pw;
    private void Start()
    {
        pw = GetComponent<PhotonView>();
        if(!pw.IsMine)
        {
            localMeshes.SetActive(false);
            remoteMeshes.SetActive(true);
        }
    }
    public void InitializeCharacter(int playerId)
    {
        responsiblePlayer = playerId;
    }
}
