using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace htn.Shared
{
    public class JokeService
    {
        private List<string> Jokes = new List<string>();
        public JokeService()
        {
            Regex regex = new Regex("\\d+,\"(.+)\"");
            var k = File.ReadAllLines("reddit-cleanjokes.csv");
            foreach (var p in k)
            {
                var o = regex.Match(p);
                string cs = o.Groups[1].Value;
                if (cs.Length < 100)
                {
                    Jokes.Add(cs);
                }
            }
        }

        public string GetRandomJoke()
        {
            int idx = new Random().Next(0, Jokes.Count);
            return Jokes[idx];
        }
    }
}