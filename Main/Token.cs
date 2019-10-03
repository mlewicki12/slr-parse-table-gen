
namespace ML.Main {
  public class Token {
    public string Symbol;

    public Token(string val) {
      Symbol = val;
    }

    public Token Copy() {
      return new Token(this.Symbol);
    }

    public bool Equals(Token other) {
      return (Symbol == other.Symbol);
    }
  }
}