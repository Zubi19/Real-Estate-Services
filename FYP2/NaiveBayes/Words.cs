using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using FYP2.Controllers;

namespace FYP2.NaiveBayes
{
    public class Words
    {

        public const string TokenPattern = @"([a-zA-Z]\w+)\W*";

        public SortedDictionary<string, int> Tokens
        {
            get { return _tokens; }
        }
   
        //}
        private SortedDictionary<string, int> _tokens = new SortedDictionary<string, int>();

        Calculation c = new Calculation();
        public  double start(string comment)
        {
            if (Variables.naivebayesrun == false)
            {
                Variables.naivebayesrun = true;
                Words negative = new Words();
                Words positive = new Words();
                negative.LoadFromFile("C:\\Users\\zubair\\documents\\visual studio 2013\\Projects\\FYP2\\FYP2\\NaiveBayes\\negative.txt");
                positive.LoadFromFile("C:\\Users\\zubair\\documents\\visual studio 2013\\Projects\\FYP2\\FYP2\\NaiveBayes\\positive.txt");

                
                c.Load(positive, negative);
            }
            double probvalue =c.Test(comment);
            return probvalue;
        }
        

        public  void LoadFromFile(string filepath)
        {
          
                LoadFromReader(new StreamReader(filepath));
               
        }

        public void LoadFromReader(TextReader reader)
        {
            Regex re = new Regex(TokenPattern, RegexOptions.Compiled);
            string line = "";
            while (null != (line = reader.ReadLine()))
            {
                Match m = re.Match(line);
                while (m.Success)
                {
                    string token = m.Groups[1].Value;
                    AddToken(token);
                    m = m.NextMatch();
                }
            }
        }
        public void AddToken(string rawPhrase)
        {
            if (!_tokens.ContainsKey(rawPhrase))
            {
                _tokens.Add(rawPhrase, 1);
            }
            else
            {
                _tokens[rawPhrase]++;
            }
        }
       
    }
}