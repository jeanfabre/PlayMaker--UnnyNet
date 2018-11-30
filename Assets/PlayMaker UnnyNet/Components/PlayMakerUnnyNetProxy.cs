// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections.Generic;
using UnityEngine;

public class PlayMakerUnnyNetProxy : MonoBehaviour
{

    static string playerId;
    static string playerEmail;

#if UNITY_EDITOR
    bool _eventadded;
#endif

    void OnEnable()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            CreateGlobalEventIfNecessary();
        }
#endif

        UnnyNet.UnnyNet.m_OnPlayerAuthorized += OnPlayerAuthorized;
    }

    void OnDisable()
    {
        UnnyNet.UnnyNet.m_OnPlayerAuthorized -= OnPlayerAuthorized;
    }

    void OnPlayerAuthorized(Dictionary<string, string> obj)
    {
        playerEmail = string.Empty; playerId = string.Empty;

        obj.TryGetValue("unny_id", out playerId);

        obj.TryGetValue("email", out playerEmail);

        PlayMakerFSM.BroadcastEvent("UNNYNET / PLAYER / ON AUTHORIZED");
    }

    public static bool IsAuthorized
    {
        get
        {
            return !string.IsNullOrEmpty(PlayerId);
        }
    }

    public static string PlayerId
    {
        get
        {
            return playerId;
        }
    }

    public static string PlayerEmail
    {
        get
        {
            return playerId;
        }
    }

#if UNITY_EDITOR
    void CreateGlobalEventIfNecessary()
    {
        if (!_eventadded)
        {
            _eventadded = PlayMakerUtils.CreateIfNeededGlobalEvent("CINEMACHINE / COLLIDER / ON CAMERA DISPLACED ENDED");
        }
    }
#endif
}