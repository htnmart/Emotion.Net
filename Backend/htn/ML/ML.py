from nltk.sentiment.vader import SentimentIntensityAnalyzer
from nltk import tokenize
import nltk
import sys
nltk.download('punkt')
for ln in sys.stdin:
    tokens = tokenize.sent_tokenize(ln.rstrip('\n'))
    analyzer = SentimentIntensityAnalyzer()
    score = 0
    for token in tokens:
        tscore = analyzer.polarity_scores(token)
        score += tscore["compound"]
    score /= len(tokens)
    print(score, flush=True)
