
using System;
using System.Collections.Generic;
using ML.Main;

namespace ML.Driver {
  class Program {
    static void Main(string[] args) {
      // todo read from file
      var grammar = new Grammar("S -> a S b S\nS -> a");
      grammar.Print();
      Console.WriteLine();
      
      var slr = new StateGenerator(grammar);
      slr.PrintStates();
      
      PrintList("Grammar terminals", grammar.Terminals);
      PrintList("Grammar non-terminals", grammar.NonTerminals);
      PrintList("FIRST set for S", grammar.First("S"));
      PrintList("FOLLOW set for S", grammar.Follow("S"));
    }
    
    public static void PrintList<T>(string desc, List<T> val) {
      Console.Write("\n" + desc + ":");
      foreach (var tok in val) {
        Console.Write(" " + tok);
      }
    }
  }
}