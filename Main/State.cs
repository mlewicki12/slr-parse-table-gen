
using System;
using System.Collections.Generic;

namespace ML.Main {
  public class State {
    public List<Production> Productions;
    public Dictionary<string, Transition> Transitions;

    public State() {
      Productions = new List<Production>();
    }

    public State Add(Production prod) {
      Productions.Add(prod);
      return this;
    }

    public State AddTransition(string value, Transition tran) {
      Transitions.Add(value, tran);
      return this;
    }

    public bool Contains(Production prod) {
      foreach (var check in Productions) {
        if (check.Equals(prod)) {
          return true;
        }
      }

      return false;
    }

    public bool ContainsStem(Production prod) {
      foreach (var check in Productions) {
        if (check.EqualsBeforeDot(prod)) {
          return true;
        }
      }

      return false;
    }
    
    public State Print(string pref) {
      foreach (var prod in Productions) {
        Console.Write($"{pref}{prod}");

        if (prod.NextState != -1) {
          Console.WriteLine($" -> {prod.NextState}");
        } else Console.WriteLine();
      }

      return this;
    }

    public override string ToString() {
      var ret = "{ ";
      foreach (var prod in Productions) {
        ret += prod + " ";
      }

      ret += "}";
      return ret;
    }
  }
}