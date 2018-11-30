// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414

public class PlayMakerUnnyNetProxy : MonoBehaviour
{
    private static string playerId = "";
    private static string playerEmail = "";

#if UNITY_EDITOR
    // Declare the method signature of the delegate to call.
    // For a void method with no parameters you could just use System.Action.
    public delegate void RepaintAction();

    // Declare the event to which editor code will hook itself.
    public event RepaintAction WantRepaint;

    // This private method will invoke the event and thus the attached editor code.
    private void Repaint()
    {
        // If no handlers are attached to the event then don't invoke it.
        if (WantRepaint != null)
        {
            WantRepaint();
        }
    }
#endif

    public const string UNNYNET_PLAYER_ONAUTHORIZED_EVENT = "UNNYNET / PLAYER / ON AUTHORIZED";

    void OnEnable()
    {
        UnnyNet.UnnyNet.m_OnPlayerAuthorized += OnPlayerAuthorized;
    }

    void OnDisable()
    {
        UnnyNet.UnnyNet.m_OnPlayerAuthorized -= OnPlayerAuthorized;
    }

    void OnPlayerAuthorized(Dictionary<string, string> obj)
    {
        playerEmail = string.Empty;
        playerId = string.Empty;

        obj.TryGetValue("unny_id", out playerId);

        obj.TryGetValue("email", out playerEmail);

        //foreach(KeyValuePair<string,string> data in obj)
        //{
        //    Debug.Log(data.Key + ":" + data.Value);
        //}

#if UNITY_EDITOR
        Repaint();
#endif
        PlayMakerFSM.BroadcastEvent(UNNYNET_PLAYER_ONAUTHORIZED_EVENT);
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
            return playerEmail;
        }
    }

}