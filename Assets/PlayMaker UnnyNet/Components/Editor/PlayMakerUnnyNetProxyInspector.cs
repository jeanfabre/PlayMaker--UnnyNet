// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
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

    public override void OnInspectorGUI()
    {
        // TODO: better help formating
        GUILayout.Space(2f);
        GUILayout.Label("This proxy will broadcast One Playmaker event:");
        GUILayout.Label(PlayMakerUnnyNetProxy.UNNYNET_PLAYER_ONAUTHORIZED_EVENT);
        GUILayout.Label("Use UnnyNetGetPlayerInfos to get:");
        GUILayout.Label("Email, Id, and status");

        if (Application.isPlaying)
        {
            GUILayout.Space(2f);
            EditorGUILayout.LabelField("Authorized", PlayMakerUnnyNetProxy.IsAuthorized.ToString());
            EditorGUILayout.LabelField("Id", PlayMakerUnnyNetProxy.PlayerId);
            EditorGUILayout.LabelField("Email", PlayMakerUnnyNetProxy.PlayerEmail);
        }
    }
}
