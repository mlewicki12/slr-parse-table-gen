
using System;
using System.Collections.Generic;

namespace ML.Main {
  public class ParseTable {
    public Grammar Grammar;
    public List<State> States;

    private Dictionary<string, State> _table;
    
    public ParseTable(Grammar grammar, List<State> states) {
      Grammar = grammar;
      States = states;

      // fill each state with reject transitions
      foreach (var state in States) {
        state.Transitions = new Dictionary<string, Transition>();
        foreach (var terminal in Grammar.Terminals) {
          state.Transitions.Add(terminal, new Transition(Transition.TransitionType.REJECT));
        }

        foreach (var nonterminal in Grammar.NonTerminals) {
          state.Transitions.Add(nonterminal, new Transition(Transition.TransitionType.REJECT));
        }
        
        // now run through all of the generated states
        foreach (var production in state.Productions) {
          var nextToken = production.NextToken();
            
          // dot not at end
          if (production.Position < production.Content.Count && nextToken.Symbol != "$") {
            state.Transitions.Remove(nextToken.Symbol);
            if (Grammar.NonTerminals.Contains(nextToken.Symbol)) {
              state.Transitions.Add(
                nextToken.Symbol, 
                new Transition(Transition.TransitionType.GOTO, production.NextState));
            }
            else {
              state.Transitions.Add(
                nextToken.Symbol,
                new Transition(Transition.TransitionType.SHIFT, production.NextState));
            }
          }
          else {
            if (nextToken != null) {
              state.Transitions.Remove(nextToken.Symbol);
              state.Transitions.Add(
                nextToken.Symbol,
                new Transition(Transition.TransitionType.ACCEPT));
            }
            else {
              var follow = Grammar.Follow(production.Result.Symbol);
              foreach (var token in follow) {
                state.Transitions.Remove(token.Symbol);
                state.Transitions.Add(
                  token.Symbol,
                  new Transition(Grammar.GetProductionNumber(production), production));
              }
            }
          }
        }
      }
    }

    public ParseTable Print() {
      Console.Write("\t");
      // this code needs to be made nicer lol
      foreach (var term in Grammar.Terminals) {
        Console.Write(term + "\t");
      }
      
      Console.Write("||\t");

      foreach (var non in Grammar.NonTerminals) {
        Console.Write(non + "\t");
      }

      Console.WriteLine();
      for (int i = 0; i < States.Count; ++i) {
        Console.Write($"{i}:\t");
        foreach (var term in Grammar.Terminals) {
          Console.Write(States[i].Transitions[term] + "\t");
        }

        Console.Write("||\t");
        foreach (var non in Grammar.NonTerminals) {
          Console.Write(States[i].Transitions[non] + "\t");
        }
        
        Console.WriteLine();
      }

      return this;
    }
  }
}