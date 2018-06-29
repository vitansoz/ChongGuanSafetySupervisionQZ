using SpeechLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public class SpRecognition
    {
        public delegate void StringEvent(string str);

        private static SpRecognition _Instance = null;

        private ISpeechRecoGrammar isrg;

        private SpSharedRecoContextClass ssrContex = null;

        public SpRecognition.StringEvent SetMessage;

        public SpRecognition()
        {
            this.ssrContex = new SpSharedRecoContextClass();
            this.isrg = this.ssrContex.CreateGrammar(1);
            this.ssrContex.Recognition += SsrContex_Recognition; ;
        }

        private void SsrContex_Recognition(int StreamNumber, object StreamPosition, SpeechRecognitionType RecognitionType, ISpeechRecoResult Result)
        {
            bool flag = this.SetMessage != null;
            if (flag)
            {
                this.SetMessage(Result.PhraseInfo.GetText(0, -1, true));
            }
        }

        public void BeginRec()
        {
            this.isrg.DictationSetState(SpeechRuleState.SGDSActive);
        }

        public static SpRecognition instance()
        {
            bool flag = SpRecognition._Instance == null;
            if (flag)
            {
                SpRecognition._Instance = new SpRecognition();
            }
            return SpRecognition._Instance;
        }

        public void CloseRec()
        {
            this.isrg.DictationSetState(SpeechRuleState.SGDSInactive);
        }

        private void ContexRecognition(int iIndex, object obj, SpeechRecognitionType type, ISpeechRecoResult result)
        {
            bool flag = this.SetMessage != null;
            if (flag)
            {
                this.SetMessage(result.PhraseInfo.GetText(0, -1, true));
            }
        }
    }
}
