// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Get pLayer Infos. Requires PlayMakerUnnyNetProxy component to be in the scene, listen to 'UNNYNET / PLAYER / ON AUTHORIZED' before using this action")]
    public class UnnyNetGetPlayerInfos : FsmStateAction
    {
        [Tooltip("The Player Id")]
        [UIHint(UIHint.Variable)]
        public FsmString playerId;

        [Tooltip("The Player Email. May not be set")]
        [UIHint(UIHint.Variable)]
        public FsmString playerEmail;

        [Tooltip("The Player Name.")]
        [UIHint(UIHint.Variable)]
        public FsmString playerName;


        [ActionSection("Result")]
        [Tooltip("true if player is authorized")]
        [UIHint(UIHint.Variable)]
        public FsmBool authorized;

        [Tooltip("event sent if player authorized")]
        public FsmEvent authorizedEvent ;

        [Tooltip("event sent if player is not authorized")]
        public FsmEvent notAuthorizedEvent;

        bool _success;
        float _score;

        public override void Reset()
        {
            playerId = null;
            playerEmail = null;
            playerName = null;

            authorized = null;
            authorizedEvent = null;
            notAuthorizedEvent = null;
        }

        public override void OnEnter()
        {
            if (!playerId.IsNone)
            {
                playerId.Value = PlayMakerUnnyNetProxy.PlayerId;
            }

            if (!playerEmail.IsNone)
            {
                playerEmail.Value = PlayMakerUnnyNetProxy.PlayerEmail;
            }

            if (!playerName.IsNone)
            {
                playerName.Value = PlayMakerUnnyNetProxy.PlayerName;
            }

            if (!authorized.IsNone)
            {
                authorized.Value = PlayMakerUnnyNetProxy.IsAuthorized;
            }

            if (PlayMakerUnnyNetProxy.IsAuthorized)
            {
                if (authorizedEvent != null)
                {
                    Fsm.Event(authorizedEvent);
                }
            }
            else
            {
                if (notAuthorizedEvent != null)
                {
                    Fsm.Event(notAuthorizedEvent);
                }
            }

            Finish();

        }

    }
}