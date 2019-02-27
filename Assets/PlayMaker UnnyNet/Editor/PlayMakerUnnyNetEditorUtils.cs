// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PlayMakerUnnyNetEditorUtils
{

    static PlayMakerUnnyNetEditorUtils()
    {
        Actions.AddCategoryIcon("UnnyNet",CategoryIcon);

        #if UNITY_EDITOR
        if (!EditorApplication.isPlayingOrWillChangePlaymode)
        {
            CreateGlobalEventIfNecessary();
        }
        #endif
    }

    static Texture _CategoryIcon = null;
    internal static Texture CategoryIcon
    {
        get
        {
            if (_CategoryIcon == null)
                _CategoryIcon = Resources.Load<Texture>("UnnyNet_category_icon");

            if (_CategoryIcon != null)
                _CategoryIcon.hideFlags = HideFlags.DontSaveInEditor;

            return _CategoryIcon;
        }
    }


   static void CreateGlobalEventIfNecessary()
    {
      //  Debug.Log("Create global events If needed for UnnyNet");
        PlayMakerUtils.CreateIfNeededGlobalEvent(PlayMakerUnnyNetProxy.UNNYNET_PLAYER_ON_AUTHORIZED_EVENT);

        PlayMakerUtils.CreateIfNeededGlobalEvent(PlayMakerUnnyNetProxy.UNNYNET_PLAYER_NAME_CHANGED_EVENT);

        PlayMakerUtils.CreateIfNeededGlobalEvent(PlayMakerUnnyNetProxy.UNNYNET_GUILD_NEW_EVENT);
        PlayMakerUtils.CreateIfNeededGlobalEvent(PlayMakerUnnyNetProxy.UNNYNET_GUILD_NEW_REQUEST_EVENT);
        PlayMakerUtils.CreateIfNeededGlobalEvent(PlayMakerUnnyNetProxy.UNNYNET_GUILD_RANKED_CHANGED_EVENT);
        PlayMakerUtils.CreateIfNeededGlobalEvent(PlayMakerUnnyNetProxy.UNNYNET_GUILD_NEW_REQUEST_EVENT);

        PlayMakerUtils.CreateIfNeededGlobalEvent(PlayMakerUnnyNetProxy.UNNYNET_ACHIEVEMENT_COMPLETED_EVENT);
    }
}