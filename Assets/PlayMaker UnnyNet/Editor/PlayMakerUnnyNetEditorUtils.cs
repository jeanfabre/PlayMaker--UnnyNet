// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PlayMakerUnnyNetEditorUtils
{

    static PlayMakerUnnyNetEditorUtils()
    {
        Actions.AddCategoryIcon("UnnyNet",CategoryIcon);

        if (!EditorApplication.isPlaying)
        {
            CreateGlobalEventIfNecessary();
        }
    }

   static bool _eventadded;

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
        if (!_eventadded)
        {
            _eventadded = PlayMakerUtils.CreateIfNeededGlobalEvent(PlayMakerUnnyNetProxy.UNNYNET_PLAYER_ONAUTHORIZED_EVENT);
        }
    }

}