
using System;
using System.Collections.Generic;

namespace ML.Main {
  public class Production : IEquatable<Production> {
    public readonly Token Result;         // The left side of the grammar definition
    public readonly List<Token> Content;  // The right side of the grammar definition
    public readonly int Position;         // The position of the dot

    public int NextState;

    public Production(string val) {   // Get the state from a string
      Content = new List<Token>();
      Position = 0;
      NextState = -1;
      
      var spl = val.Split("->");
      if (spl.Length != 2) {
        throw new Exception("Invalid grammar string format");
      }
      
      Result = new Token(spl[0].Trim());
      spl = spl[1].Trim().Split(" ");

      foreach (var tok in spl) {
        Content.Add(new Token(tok));
      }
    }

    public Production(Production basis) {
      Result = basis.Result.Copy();
      Position = basis.Position + 1;
      NextState = -1;

      Content = new List<Token>();
      foreach (var tok in basis.Content) {
        Content.Add(tok.Copy());
      }
    }

    public Production Next() {
      if (Position == Content.Count || Content[Position].Symbol == "$") { // hardcoded no read on end symbol
        return null;
      }
      
      return new Production(this);
    }

    public Token NextToken() {
      if (Position == Content.Count) {
        return null;
      }

      return Content[Position];
    }

    public bool Equals(Production other) {
      if (!Result.Equals(other.Result) || Content.Count != other.Content.Count) {
        return false;
      }

      for (int i = 0; i < Content.Count; ++i) {
        if (!Content[i].Equals(other.Content[i])) {
          return false;
        }
      }

      return true;
    }

    public bool EqualsBeforeDot(Production other) {
      if (!Result.Equals(other.Result) || Position != other.Position) {
        return false;
      }

      for (int i = 0; i < Position; ++i) {
        if (!Content[i].Equals(other.Content[i])) {
          return false;
        }
      }

      return true;
    }

    public override string ToString() {
      var ret = $"{Result.Symbol}\t-> ";
      for (int i = 0; i < Content.Count; ++i) {
        if (i == Position) {
          ret += ".";
        }

        ret += $"{Content[i].Symbol}";
      }

      if (Position == Content.Count) {
        ret += ".";
      }

      return ret;
    }
  }
}