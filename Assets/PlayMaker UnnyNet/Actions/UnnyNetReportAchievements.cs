// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Report a progress to a UnnyNet achievement Id")]
    public class UnnyNetReportAchievements : FsmStateAction
    {
        [Tooltip("The achievement Id")]
        public FsmInt achievementId;

        [Tooltip("The progress")]
        public FsmInt progress;

        [Tooltip("The progress as a string, must be a valid int")]
        public FsmString progressAsString;

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
        int _progress;

        public override void Reset()
        {
            achievementId = null;
            progress = null;
            progressAsString = new FsmString() { UseVariable = true };
            success = null;
            errorMessage = null;
            errorEvent = null;
            successEvent = null;
        }

        public override void OnEnter()
        {
            if (!string.IsNullOrEmpty(progressAsString.Value))
            {
                if (!int.TryParse(progressAsString.Value, out _progress))
                {
                    LogError("Progress as string failed to parse as a int");
                }
            }else{
                _progress = progress.Value;
            }

            UnnyNet.UnnyNet.ReportAchievements(achievementId.Value, _progress, HandleReportAchievemets);
        }

        void HandleReportAchievemets(string error)
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