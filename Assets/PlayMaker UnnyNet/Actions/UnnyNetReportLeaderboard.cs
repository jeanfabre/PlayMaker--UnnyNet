// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Report a score to a UnnyNet LeaderBoard")]
    public class UnnyNetReportLeaderboard: FsmStateAction
    {
        [Tooltip("The Leaderboard Name")]
        public FsmString leaderboardName;

        [Tooltip("The Score")]
        public FsmFloat score;

        [Tooltip("The Score as a string, must be a valid float")]
        public FsmString scoreAsString;

        [ActionSection("Result")]
        [Tooltip("true if report succeeded")]
        [UIHint(UIHint.Variable)]
        public FsmBool success;

        [Tooltip("The error message if report failed")]
        [UIHint(UIHint.Variable)]
        public FsmString errorMessage;

        [Tooltip("event sent if report failed")]
        public FsmEvent successEvent;

        [Tooltip("event sent if report succeeded")]
        public FsmEvent errorEvent;

        bool _success;
        float _score;

        public override void Reset()
        {
            leaderboardName = null;
            score = null;
            scoreAsString = new FsmString() { UseVariable = true };
            success = null;
            errorMessage = null;
            errorEvent = null;
            successEvent = null;
        }

        public override void OnEnter()
        {
            if (!string.IsNullOrEmpty(scoreAsString.Value))
            {
                if (!float.TryParse(scoreAsString.Value, out _score))
                {
                    LogError("Score as string failed to parse as a float");
                }
            }else{
                _score = score.Value;
            }

            UnnyNet.UnnyNet.ReportLeaderboards(leaderboardName.Value, _score, HandleReportLeaderboards);
        }

        void HandleReportLeaderboards(string error)
        {
            if (!errorMessage.IsNone)
            {
                errorMessage.Value = error;
            }

            _success = string.IsNullOrEmpty(error);

            if (!success.IsNone)
            {
                success.Value = _success;
            }

            if (_success)
            {
                if (successEvent!=null)
                {
                    Fsm.Event(successEvent);
                }
            }else{
                if (errorEvent != null)
                {
                    Fsm.Event(errorEvent);
                }
            }

            Finish();
        }
    }
}