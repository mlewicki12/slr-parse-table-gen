
using System;
using System.Collections.Generic;

namespace ML.Main {
  public class State {
    public List<Production> Productions;

    public State() {
      Productions = new List<Production>();
    }

    public State Add(Production prod) {
      Productions.Add(prod);
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
    
    public State Print() {
      foreach (var prod in Productions) {
        Console.WriteLine($"\t{prod}");
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