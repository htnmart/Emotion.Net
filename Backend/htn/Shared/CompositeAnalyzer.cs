using System;
using System.Text;
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
            if (string.IsNullOrEmpty(input)) return 0;
            StringBuilder sb = new StringBuilder();
            foreach (var c in input)
            {
                if (char.IsLetter(c) || char.IsDigit(c) || char.IsPunctuation(c) || char.IsSeparator(c))
                {
                    sb.Append(c);
                }
            }
            float azureScore = 0;
            int cnt =1;
            if (DateTime.Now - LastAzureCall >= TimeSpan.FromSeconds(5))
            {
                LastAzureCall = DateTime.Now;
                azureScore = Azure.GetSentiment(sb.ToString());
                cnt++;
            }

            float localScore = Local.GetScore(sb.ToString());
            return (azureScore + localScore) / cnt;
        }
    }
}