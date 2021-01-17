## Emotion.Net - Computers have feelings too!

**Visit our live demo [here!](https://emot.ga/ "Click to view demo")

### Inspiration

People matter. Emotions matter. The COVID-19 pandemic has put people's mental health at-risk. Research has shown that the pandemic has nearly tripled the rate of depression. We created EmotionNet to be able to assess the feelings of individuals during hard times.

### Features

EmotionNet asks users to type sentences and words that describe their day, or current state of mind— and based on this description, it scores the user’s positivity level on a scale of `-100%` (being the most negative) to `100%` (being the most positive). The interface updates in real-time, providing a responsive experience. Based on that, EmotionNet can give the user a joke to cheer them up if they are extremely negative.

### How we built it

The front-end of EmotionNet was written with pure JavaScript/HTML along with ASP.NET/Blazor for both the front and back-end. The site uses a combination of NLTK - Vader sentiment analysis and Azure Cognitive Toolkit. The reason for this is explained in the “Challenges” section.

### Challenges we faced

**While making EmotionNet, we encountered numerous challenges. Here is an unordered list of some obstacles:**

- Since this is the first time the team attended a hackathon, we were not accustomed to the limited time we had!
- EmotionNet was built with a large range of languages, including but not limited to: C#, JavaScript, Razor, HTML, Python and CSS. Coordinating these languages together was a challenging task.
- The Azure Cognitive Service had a strict 5000 requests per month limit, but since we wanted to update in real-time, we had to improvise. Our team came up with a compound text analyzer that combined a local instance of NLTK that offered real-time performance, with the cloud to offer the most accurate predictions.
- We had limited knowledge in web-development, and had issues properly aligning items when the page got complex.

### What we learned

Building EmotionNet was an excellent team-building experience. Through the limited time we had, we not only learned more about developing websites and services, but more importantly we learned more about each other!

### Opportunities for expansion

EmotionNet has plenty of potential for further development. We hope to increase the accuracy of our prediction software to provide for a better experience with our users. Other potential opportunities include adding multiple emotions, an emotion detection API, and improved performance.