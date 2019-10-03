
using System;
using System.Collections.Generic;

namespace ML.Main {
  public class StateGenerator {
    public Grammar Grammar;
    public List<State> States;

    public StateGenerator(Grammar grammar) {
      Grammar = grammar;
      States = new List<State>();
      
      Start();
      for (int i = 0; i < States.Count; ++i) {
        Complete(i).Read(i);
      }
    }

    public StateGenerator Start() {
      States = new List<State>();
      
      States.Add(new State());
      States[0].Add(Grammar.Productions[0]);

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
          prod.NextState = newState;
          
          States[newState].Add(newProd);
        }
        else if (!States[newState].Contains(newProd)) {
          prod.NextState = newState;
          
          States[newState].Add(newProd);
        }
      }

      return this;
    }

    public StateGenerator Complete(int state) {
      var productions = States[state].Productions;
      for (int i = 0; i < productions.Count; ++i) {
        var tok = productions[i].NextToken();
        if (tok != null && Grammar.NonTerminals.Contains(tok.Symbol)) {
          foreach (var ntProd in Grammar.Productions) {
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
        States[i].Print("\t");
      }

      return this;
    }
  }
}