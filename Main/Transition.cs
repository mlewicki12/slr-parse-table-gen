
namespace ML.Main {
  public class Transition {
    public enum TransitionType {
      SHIFT, GOTO,
      REDUCE, ACCEPT,
      REJECT
    }

    public TransitionType Type;
    
    // SHIFT, GOTO
    public int NextState;

    public Transition(TransitionType type, int next) {
      Type = type;
      NextState = next;
    }
    
    // REDUCE
    public Production Production;
    public int ProductionNumber;

    public Transition(int numb, Production prod) {
      Type = TransitionType.REDUCE;
      Production = prod;
      ProductionNumber = numb;
    }

    public Token Run() {
      return Production.Result;
    }
    
    // ACCEPT, REJECT
    public Transition(TransitionType type) {
      Type = type;
    }

    public override string ToString() {
      switch (Type) {
        case TransitionType.SHIFT:
          return $"s{NextState}";
        
        case TransitionType.GOTO:
          return $"{NextState}";
        
        case TransitionType.REDUCE:
          return $"r{ProductionNumber}"; // workaround, because I can't get production number atm
        
        case TransitionType.ACCEPT:
          return "acc";
      }

      return "";
    }
  }
}