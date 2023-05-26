using FYP2.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FYP2.NaiveBayes
{
    public class Calculation
    {
        
        double PositivePriorProbability = 0.0;
        double NegativePriorProbability = 0.0;
        private SortedDictionary<String, double> _plikelihood = new SortedDictionary<string, double>();

        private SortedDictionary<String, double> _nlikelihood = new SortedDictionary<string, double>();
        public SortedDictionary<string, double> Plikelihood
        {
            get { return _plikelihood; }
        }
        public SortedDictionary<string, double> Nlikelihood
        {
            get { return _nlikelihood; }
        }
        

        private Words _positive;
        private Words _negative;
        private SortedDictionary<string, double> _prob;
        private int _npositive;
        private int _nnegative;
        public void Load(Words positive, Words negative)
        {
            _positive = positive;
            _negative = negative;

            CalculateProbabilities();
        }
        private void CalculateProbabilities()
        {
            

            _npositive = _positive.Tokens.Count;
            _nnegative = _negative.Tokens.Count;
            foreach (string token in _positive.Tokens.Keys)
            {
                PLikelihood(token);
                NLikelihood(token);
                
            }
            foreach (string token in _negative.Tokens.Keys)
            {
                PLikelihood(token);
                NLikelihood(token);
               
            }
            PriorProbability();
        }
        public void PLikelihood(string token)
        {
            double p = _positive.Tokens.ContainsKey(token) ? _positive.Tokens[token] : 0;
            
            
            double np = _npositive;
            //if (!_plikelihood.ContainsKey(token))
            if(!Variables.Plikelihood.ContainsKey(token))
            {
                Variables.Plikelihood.Add(token,(p+1)/np);
                //_plikelihood.Add(token, (p + 1) / np);
            }
        }
        public void NLikelihood(string token)
        {
            
            double n = _negative.Tokens.ContainsKey(token) ? _negative.Tokens[token] : 0;

            //if (!_nlikelihood.ContainsKey(token))
            if(!Variables.Nlikelihood.ContainsKey(token))
            {
                Variables.Nlikelihood.Add(token, (n + 1) / _nnegative);
                //_nlikelihood.Add(token, (n + 1) / _nnegative);
            }

        }
        public void PriorProbability()
        {
            int value = _npositive + _nnegative;
            //PositivePriorProbability = (double)_npositive  / (double)value;
            Variables.PositivePriorProbability = (double)_npositive / (double)value;

            //NegativePriorProbability = (double)_nnegative / (double)value;
            Variables.NegativePriorProbability = (double)_nnegative / (double)value;
        }
        double likelihood=0;
       
            
        

      
       

        public  double Test(string body)
        {
            SortedList probs = new SortedList();

            Regex re = new Regex(Words.TokenPattern, RegexOptions.Compiled);
            Match m = re.Match(body);
            //int index = 0;
            while (m.Success)
            {
                string token = m.Groups[1].Value;
                //if (_plikelihood.ContainsKey(token))
                if(Variables.Plikelihood.ContainsKey(token))
                {

                    likelihood += Math.Log(Variables.Plikelihood[token] / Variables.Nlikelihood[token]);
                     //likelihood+=Math.Log(_plikelihood[token]/_nlikelihood[token]);
                }
              
                m = m.NextMatch();
            }
            Double prob = Math.Log(Variables.PositivePriorProbability / Variables.NegativePriorProbability) + likelihood;
            //Double prob = Math.Log(PositivePriorProbability / NegativePriorProbability) + likelihood;
            //Math.Exp(prob);
          

          
            return prob;
        }
    }
}