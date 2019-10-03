
using System;
using System.Collections.Generic;

namespace ML.Main {
  public class StateGenerator {
    public List<Production> Productions;
    public List<string> NonTerminals;
    public List<State> States;

    public Production StartProduction;

    public StateGenerator(string grammar) {
      Productions = new List<Production>();
      NonTerminals = new List<string>();
      
      foreach (var line in grammar.Split("\n")) {
        var add = new Production(line);
        if (!NonTerminals.Contains(add.Result.Symbol)) {
          NonTerminals.Add(add.Result.Symbol);
        }
        
        Productions.Add(add);
      }

      var startProd = $"{Productions[0].Result.Symbol}' -> {Productions[0].Result.Symbol} $";
      StartProduction = new Production(startProd);

      // todo: make this its own function
      this.Start();
      for (int i = 0; i < States.Count; ++i) {
        this.Complete(i);
        this.Read(i);
      }
    }

    public StateGenerator Print() {
      Console.WriteLine(StartProduction);
      
      foreach (var prod in Productions) {
        Console.WriteLine(prod);
      }

      return this;
    }

    public StateGenerator Start() {
      States = new List<State>();
      
      States.Add(new State());
      States[0].Add(StartProduction);

      return this;
    }

    public StateGenerator Read(int state) {
      foreach (var prod in States[state].Productions) {
        var newProd = prod.Next();
        if (newProd == null) {
          continue;
        }
        
        int newState = -1;
        for (int i = 0; i < States.Count; ++i) {
          if (States[i].ContainsStem(newProd)) {
            newState = i;
            break;
          }
        }

        if (newState == -1) {
          newState = States.Count;
          States.Add(new State());
          States[newState].Add(newProd);
        }
        else if (!States[newState].Contains(newProd)) {
          States[newState].Add(newProd);
        }
      }

      return this;
    }

    public StateGenerator Complete(int state) {
      var productions = States[state].Productions;
      for (int i = 0; i < productions.Count; ++i) {
        var tok = productions[i].NextToken();
        if (tok != null && NonTerminals.Contains(tok.Symbol)) {
          foreach (var ntProd in Productions) {
            if (ntProd.Result.Equals(tok)) {
              productions.Add(ntProd);
            }
          }
        }
      }

      return this;
    }

    public StateGenerator PrintStates() {
      for (int i = 0; i < States.Count; ++i) {
        Console.WriteLine($"{i}:");
        States[i].Print();
      }

      return this;
    }
  }
}