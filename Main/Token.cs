
using System;

namespace ML.Main {
  public class Token : IEquatable<Token> {
    public string Symbol;

    public Token(string val) {
      Symbol = val;
    }

    public Token Copy() {
      return new Token(Symbol);
    }

    public bool Equals(Token other) {
      if (other != null) {
        return (Symbol == other.Symbol);
      }

      return false;
    }

    public override string ToString() {
      return $"{Symbol}";
    }
  }
}