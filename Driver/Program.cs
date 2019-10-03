
using System;
using ML.Main;

namespace ML.Driver {
  class Program {
    static void Main(string[] args) {
      var slr = new StateGenerator("S -> a S b S\nS -> a");
      slr.Print();
      slr.PrintStates();
    }
  }
}