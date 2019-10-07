
using System;
using System.Collections.Generic;
using ML.Main;

namespace ML.Driver {
  class Program {
    static void Main(string[] args) {
      // todo read from file
      var grammar = new Grammar("S -> a S b S\nS -> a");
      var states  = new StateGenerator(grammar);

      states.PrintStates();
      
      var table   = new ParseTable(grammar, states.States);

      Console.WriteLine();
      table.Print();
    }
  }
}