// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Opens UnnyNet LeaderBoard")]
    public class UnnyNetOpenLeaderBoard : FsmStateAction
    {
        [Tooltip("The error message is any")]
        [UIHint(UIHint.Variable)]
        public FsmString errorMessage;

        [Tooltip("Event sent if there is an error callback, likely because the window is already opened")]
        public FsmEvent errorEvent;

        [Tooltip("Event sent if the window opened")]
        public FsmEvent successEvent;

        public override void Reset()
        {
            errorMessage = null;
            errorEvent = null;
            successEvent = null;
        }

        public override void OnEnter()
        {
            UnnyNet.UnnyNet.OpenLeaderboards(LeaderBoardsWasOpenedCallback);
     
        }

        void LeaderBoardsWasOpenedCallback(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                errorMessage.Value = string.Empty;

                Fsm.Event(successEvent);
            }
            else
            {
                errorMessage.Value = error;
                Fsm.Event(errorEvent);
            }

            Finish();
        }
    }
}