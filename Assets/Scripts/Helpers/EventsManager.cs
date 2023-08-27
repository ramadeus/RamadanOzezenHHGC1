using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventsManager
{
    // ıı

    public static Action OnCreateRoomRequested;
    public static Action OnJoinRoomRequested;
    public static Action<bool> OnCharSelectionSuccess;
    public static Action OnCharSelectionCheck;
    public static Action OnReadyToJoin;
    public static Action<int> OnCollectedCollectible;
    public static Action OnGameFinished;
}
