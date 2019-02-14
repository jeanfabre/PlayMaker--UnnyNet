// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

#pragma warning disable 0414

public class PlayMakerUnnyNetProxy : MonoBehaviour
{
    private static string playerId = "";
    private static string playerEmail = "";
    private static string playerName = "";

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

    public const string UNNYNET_PLAYER_ON_AUTHORIZED_EVENT       = "UNNYNET / PLAYER / ON AUTHORIZED";
    public const string UNNYNET_PLAYER_NAME_CHANGED_EVENT       = "UNNYNET / PLAYER / ON NAME CHANGED";
    public const string UNNYNET_ACHIEVEMENT_COMPLETED_EVENT     = "UNNYNET / ACHIEVEMENT / ON COMPLETED";
    public const string UNNYNET_GUILD_NEW_REQUEST_EVENT         = "UNNYNET / GUILD / ON NEW REQUEST";
    public const string UNNYNET_GUILD_NEW_EVENT                 = "UNNYNET / GUILD / ON NEW";
    public const string UNNYNET_GUILD_RANKED_CHANGED_EVENT      = "UNNYNET / GUILD / ON RANKED CHANGED";

    public string GuildRequestRejectionMessage = string.Empty;

    void OnEnable()
    {
        UnnyNet.UnnyNetBase.m_OnPlayerAuthorized += OnPlayerAuthorized;
        UnnyNet.UnnyNetBase.m_OnPlayerNameChanged += OnPlayerNameChanged;
        UnnyNet.UnnyNetBase.m_OnAchievementCompleted += OnAchievementCompleted;
        UnnyNet.UnnyNetBase.m_OnNewGuildRequest += OnNewGuildRequest;
        UnnyNet.UnnyNetBase.m_OnNewGuild += OnNewGuild;
        UnnyNet.UnnyNetBase.m_OnRankChanged += OnRankChanged;

    }

    void OnDisable()
    {
        UnnyNet.UnnyNetBase.m_OnPlayerAuthorized -= OnPlayerAuthorized;
        UnnyNet.UnnyNetBase.m_OnPlayerNameChanged -= OnPlayerNameChanged;
        UnnyNet.UnnyNetBase.m_OnAchievementCompleted += OnAchievementCompleted;
        UnnyNet.UnnyNetBase.m_OnNewGuild -= OnNewGuild;
        UnnyNet.UnnyNetBase.m_OnRankChanged -= OnRankChanged;
    }



    void OnPlayerAuthorized(Dictionary<string, string> prms)
    {
        playerEmail = string.Empty;
        playerId = string.Empty;

        prms.TryGetValue("unny_id", out playerId);

        prms.TryGetValue("email", out playerEmail);


        prms.TryGetValue("name", out playerName);
        //foreach(KeyValuePair<string,string> data in obj)
        //{
        //    Debug.Log(data.Key + ":" + data.Value);
        //}

        #if UNITY_EDITOR
            Repaint();
        #endif

        SetEventProperties.properties.Clear();
        SetEventProperties.properties = prms.ToDictionary(x => x.Key, x => x.Value as object);

        PlayMakerFSM.BroadcastEvent(UNNYNET_PLAYER_ON_AUTHORIZED_EVENT);
    }

    void OnPlayerNameChanged(string newName)
    {
        playerName = newName;

        #if UNITY_EDITOR
                Repaint();
        #endif

        SetEventProperties.properties.Clear();
        SetEventProperties.properties["Name"] = newName;

        PlayMakerFSM.BroadcastEvent(UNNYNET_PLAYER_NAME_CHANGED_EVENT);
    }

    void OnAchievementCompleted(Dictionary<string, string> prms)
    {
        SetEventProperties.properties.Clear();
        SetEventProperties.properties = prms.ToDictionary(x => x.Key, x => x.Value as object);

        PlayMakerFSM.BroadcastEvent(UNNYNET_ACHIEVEMENT_COMPLETED_EVENT);
    }

    string OnNewGuildRequest(Dictionary<string, string> prms)
    {
        SetEventProperties.properties.Clear();
        SetEventProperties.properties = prms.ToDictionary(x => x.Key, x => x.Value as object);

        PlayMakerFSM.BroadcastEvent(UNNYNET_GUILD_NEW_REQUEST_EVENT);

        return string.IsNullOrEmpty(GuildRequestRejectionMessage) ? null : GuildRequestRejectionMessage;
    }

    void OnNewGuild(Dictionary<string, string> prms)
    {
        SetEventProperties.properties.Clear();
        SetEventProperties.properties = prms.ToDictionary(x => x.Key, x => x.Value as object);

        PlayMakerFSM.BroadcastEvent(UNNYNET_GUILD_NEW_EVENT);
    }

    void OnRankChanged(Dictionary<string, string> prms)
    {
        SetEventProperties.properties.Clear();
        SetEventProperties.properties = prms.ToDictionary(x => x.Key, x => x.Value as object);

        PlayMakerFSM.BroadcastEvent(UNNYNET_GUILD_RANKED_CHANGED_EVENT);
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

    public static string PlayerName
    {
        get
        {
            return playerName;
        }
    }
}