using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager: MonoBehaviourPun {
    // ıı
    public static GameManager instance;
    public ScriptableObjectGameStatus gameStatus;
    public GameObject wood;
    public Material playerMaterial;
    public float gameDuration = 60;
    public LayerMask unSpawnableLayer;
    [SerializeField] ScriptableObjectCharSelection charSelection;
    [SerializeField] GameObject[] characters;
    [HideInInspector] public PhotonView pw;
     int readyCount;
     int charSelectedCount;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            pw = photonView;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        EventsManager.OnCollectedCollectible += UpdateGameStatus;
        EventsManager.OnGameFinished += FinishTheGame;
    }
    private void OnDisable()
    {
        EventsManager.OnCollectedCollectible -= UpdateGameStatus;
        EventsManager.OnGameFinished -= FinishTheGame;
    }
    void StartTheGame(int playerId)
    {
        InitializeCharacters(playerId);
        InGameSceneManager.instance.timer.InitializeTimer(gameDuration);
        SpawnNewWood(5);

    }
    private void InitializeGameStatus()
    {
        int totalPlayerCount = PhotonNetwork.PlayerList.Length;
        gameStatus.players = new ScriptableObjectGameStatus.PlayerStat[totalPlayerCount];
        for(int i = 0; i < totalPlayerCount; i++)
        {
            gameStatus.players[i] = new ScriptableObjectGameStatus.PlayerStat();
            gameStatus.players[i].playerId = PhotonNetwork.PlayerList[i].ActorNumber;
            gameStatus.players[i].playerName = PhotonNetwork.PlayerList[i].NickName;
            gameStatus.players[i].collectedWood = 0;
        }
    }
    private void UpdateGameStatus(int playerId)
    {
        for(int i = 0; i < gameStatus.players.Length; i++)
        {
            if(gameStatus.players[i].playerId == playerId)
            {
                gameStatus.players[i].collectedWood++;
            }
        }
    }
    private void FinishTheGame()
    {
        readyCount = 0;
        charSelectedCount = 0;
        LevelLoader.LoadNextLevel();
    }
    public void InitializePlayer()
    {
        GameObject player = PhotonNetwork.Instantiate("PlayerPersistent", Vector3.zero, Quaternion.identity, 0, null);
        player.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("nickname");
    }
    public void InitializeCharacters(int playerId)
    {
        GameObject[] charactersWillBeSpawned = GetCharactersWillBeSpawned();
        for(int i = 0; i < charactersWillBeSpawned.Length; i++)
        {
            Transform[] spawnPoses;
            if(PhotonNetwork.IsMasterClient)
            {
                spawnPoses = InGameSceneManager.instance.spawnPointsLocal;
            } else
            {
                spawnPoses = InGameSceneManager.instance.spawnPointsRemote;
            }
            GameObject character = PhotonNetwork.Instantiate(charactersWillBeSpawned[i].name, spawnPoses[i].position, Quaternion.identity, 0, null);
            character.GetComponent<CharacterIdentity>().InitializeCharacter(playerId);
        }
    }
    private GameObject[] GetCharactersWillBeSpawned()
    {
        List<GameObject> chars = new List<GameObject>();
        for(int i = 0; i < characters.Length; i++)
        {
            CharacterIdentity identity = characters[i].GetComponent<CharacterIdentity>();
            for(int a = 0; a < charSelection.selectedChars.Count; a++)
            {
                if(identity.charType == charSelection.selectedChars[a])
                {
                    chars.Add(characters[i]);
                }
            }
        }
        return chars.ToArray();
    }  
    public void SpawnNewWood(int spawnCount = 1)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < spawnCount; i++)
            {

                GameObject wood = PhotonNetwork.InstantiateRoomObject("Wood", GenerateNewCollectibleSpawnPosition(GameManager.instance.wood.transform), Quaternion.identity, default, null);
            }
        }
    }

    private Vector3 GenerateNewCollectibleSpawnPosition(Transform referenceObject)
    {
        Vector3 targetPos = Vector3.zero;
        Transform ground = InGameSceneManager.instance.ground;
        float spacing = 10;
        float minX = (ground.transform.right * (ground.transform.localScale.x / 2f) * -1).x * spacing + referenceObject.localScale.x;
        float maxX = (ground.transform.right * (ground.transform.localScale.x / 2f)).x * spacing - referenceObject.localScale.x;
        float minZ = (ground.transform.forward * (ground.transform.localScale.z / 2f) * -1).z * spacing + referenceObject.localScale.z;
        float maxZ = (ground.transform.forward * (ground.transform.localScale.z / 2f)).z * spacing - referenceObject.localScale.z;

        bool isAvailableToGenerate = false;

        while(!isAvailableToGenerate)
        {
            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(minX, maxX), 1, UnityEngine.Random.Range(minZ, maxZ));

            Collider[] colliders = Physics.OverlapBox(randomPos, referenceObject.localScale, referenceObject.rotation, unSpawnableLayer);

            bool isUnwalkableObjectNearby = colliders.Length > 0; 

            if(!isUnwalkableObjectNearby)
            {
                isAvailableToGenerate = true;
                targetPos = randomPos;
            }
        }
        return targetPos;
    }
    [PunRPC]
    public void PlayerReady(int playerId)
    {
        readyCount++;
        if(readyCount == PhotonNetwork.PlayerList.Length)
        {
            InitializeGameStatus();

            StartTheGame(playerId);
        }
    }
    [PunRPC]
    public void PlayerSelectedChar()
    {
        charSelectedCount++;
        if(charSelectedCount == PhotonNetwork.PlayerList.Length)
        {

            LevelLoader.LoadNextLevel();
        }
    }

}
