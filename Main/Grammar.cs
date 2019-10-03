
using System;
using System.Collections.Generic;

namespace ML.Main {
    public class Grammar {
        public readonly string GrammarString;
        
        public List<Production> Productions;
        
        public List<string> Terminals;
        public List<string> NonTerminals;

        public Grammar(string grammar) {
            GrammarString = grammar;
            Productions = new List<Production>();
            NonTerminals = new List<string>();
            Terminals = new List<string>();

            foreach (var line in GrammarString.Split("\n")) {
                var add = new Production(line);
                if (!NonTerminals.Contains(add.Result.Symbol)) {
                    NonTerminals.Add(add.Result.Symbol);
                }

                foreach (var tok in add.Content) {
                    if (!Terminals.Contains(tok.Symbol)) {
                        Terminals.Add(tok.Symbol);
                    }
                }
                
                Productions.Add(add);
            }

            var startProd = $"{Productions[0].Result.Symbol}' -> {Productions[0].Result.Symbol} $";
            Productions.Insert(0, new Production(startProd));

            foreach (var tok in NonTerminals) {
                Terminals.Remove(tok);
            }
        }

        public List<Token> First(string symbol) {
            if (!NonTerminals.Contains(symbol)) {
                throw new Exception("Requested FIRST set for terminal/non-existent symbol");
            }

            var ret = new List<Token>();
            foreach (var prod in Productions) {
                if (prod.Result.Symbol == symbol && !ret.Contains(prod.Content[0])) {
                    ret.Add(prod.Content[0]);
                }
            }

            return ret;
        }

        public List<Token> Follow(string symbol) {
            if (!NonTerminals.Contains(symbol)) {
                throw new Exception("Requested FOLLOW set for terminal/non-existent symbol");
            }

            var ret = new List<Token>();
            foreach (var prod in Productions) {
                for (int i = 0; i < prod.Content.Count - 1; ++i) {
                    if (prod.Content[i].Symbol == symbol && !ret.Contains(prod.Content[i+1])) {
                        ret.Add(prod.Content[i+1]);
                    }
                }
            }

            return ret;
        }

        public Grammar Print() {
            foreach (var prod in Productions) {
                Console.WriteLine(prod);
            }

            return this;
        }
    }
}