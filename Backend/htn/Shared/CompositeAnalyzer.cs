using System;
using htn.Azure;
using htn.ML;

namespace htn.Shared
{
    public class CompositeAnalyzer
    {
        private TextAnalyzer Azure;
        private PyBridge Local;
        private DateTime LastAzureCall = DateTime.Now;
        public CompositeAnalyzer(TextAnalyzer azure, PyBridge local)
        {
            Azure = azure;
            Local = local;
        }

        public float AnalyzeComposite(string input)
        {
            float azureScore = 0;
            int cnt =1;
            if (DateTime.Now - LastAzureCall >= TimeSpan.FromSeconds(5))
            {
                LastAzureCall = DateTime.Now;
                azureScore = Azure.GetSentiment(input);
                cnt++;
            }

            float localScore = Local.GetScore(input);
            return (azureScore + localScore) / cnt;
        }
    }
}