// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayMakerUnnyNetProxy))]
public class PlayMakerUnnyNetProxyInspector : Editor
{

    private PlayMakerUnnyNetProxy Target
    {
        get { return (PlayMakerUnnyNetProxy)target; }
    }

    // When you enable the inspector, attach to the game code that wants to call it.
    void OnEnable()
    {
        Target.WantRepaint += this.Repaint;
    }

    // And then detach on disable.
    void OnDisable()
    {
        Target.WantRepaint -= this.Repaint;
    }

    bool _infosToggle;

    public override void OnInspectorGUI()
    {
        // TODO: better help formating

        _infosToggle = EditorGUILayout.Foldout(_infosToggle,"Infos");

        if (_infosToggle)
        {
            GUILayout.Space(6f);
            GUILayout.Label("This proxy will broadcast several Playmaker event:");


            GUILayout.Space(6f);
            GUILayout.Label(PlayMakerUnnyNetProxy.UNNYNET_PLAYER_NAME_CHANGED_EVENT);
            GUILayout.Label("Use GetEventProperties or getEventData to get:");
            GUILayout.Label("name[string]");


            GUILayout.Space(6f);
            GUILayout.Label(PlayMakerUnnyNetProxy.UNNYNET_PLAYER_ON_AUTHORIZED_EVENT);
            GUILayout.Label("Use UnnyNetGetPlayerInfos or GetEventProperties to get:");
            GUILayout.Label("name[string]\n" +
                "email[string]\n" +
                "unny_id[string]");

            GUILayout.Space(6f);
            GUILayout.Label(PlayMakerUnnyNetProxy.UNNYNET_ACHIEVEMENT_COMPLETED_EVENT);
            GUILayout.Label("Use GetEventProperties or GetEventData to get:");
            GUILayout.Label("ach_id[int]");

            GUILayout.Space(6f);
            GUILayout.Label(PlayMakerUnnyNetProxy.UNNYNET_GUILD_NEW_REQUEST_EVENT);
            GUILayout.Label("Use GetEventProperties to get:");
            GUILayout.Label("name[string]\n" +
                "description[string]\n" +
                "type[string]\n" +
                "RejectionMessage[string]");

            GUILayout.Space(6f);
            GUILayout.Label(PlayMakerUnnyNetProxy.UNNYNET_GUILD_NEW_EVENT);
            GUILayout.Label("Use GetEventProperties to get:");
            GUILayout.Label("name[string]\n" +
                "description[string]\n" +
                "type[string]");

            GUILayout.Space(6f);
            GUILayout.Label(PlayMakerUnnyNetProxy.UNNYNET_GUILD_RANKED_CHANGED_EVENT);
            GUILayout.Label("Use GetEventProperties to get:");
            GUILayout.Label("prev_index[string]\n" +
                "curr_index[string]\n" +
                "curr_rank[string]");

        }

        if (Application.isPlaying)
        {
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Authorized", PlayMakerUnnyNetProxy.IsAuthorized.ToString());
            EditorGUILayout.LabelField("Name", PlayMakerUnnyNetProxy.PlayerName);
            EditorGUILayout.LabelField("Id", PlayMakerUnnyNetProxy.PlayerId);
            EditorGUILayout.LabelField("Email", PlayMakerUnnyNetProxy.PlayerEmail);
        }
    }
}
