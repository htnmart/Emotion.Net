using System;
using System.IO;
using Azure;
using Azure.AI.TextAnalytics;

namespace htn.Azure
{
    public class TextAnalyzer
    {
        private static Uri endpoint = new Uri("https://htn-analyzer.cognitiveservices.azure.com/");
        private TextAnalyticsClient Client;
        public TextAnalyzer()
        {
            var credentials = new AzureKeyCredential(File.ReadAllText("Secret.txt"));
            Client = new TextAnalyticsClient(endpoint, credentials);
        }

        public float GetSentiment(string s)
        {
            DocumentSentiment documentSentiment = Client.AnalyzeSentiment(s);
            float score = 0;

            foreach (var sentence in documentSentiment.Sentences)
            {
                if (sentence.Sentiment == TextSentiment.Negative) score--;
                if (sentence.Sentiment == TextSentiment.Mixed) score-=0.5f;
                if (sentence.Sentiment == TextSentiment.Positive) score++;
            }

            return score / documentSentiment.Sentences.Count;
        }
    }
}