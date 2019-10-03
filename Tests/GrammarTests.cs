
using System.Collections.Generic;
using ML.Main;
using NUnit.Framework;

namespace ML.Tests {
  public class Tests {
    public Grammar TestGrammar;
    public StateGenerator TestGenerator;
    
    [SetUp]
    public void Setup() {
      TestGrammar = new Grammar("S -> a S b S\nS -> a");
      TestGenerator = new StateGenerator(TestGrammar);
    }

    [Test]
    public void ProductionsLoadedCorrectly() {
      // I should have a test to check if productions Equals() works
      Assert.AreEqual("S\t-> .EB", $"{new Production("S -> E B")}");
    }
    
    [Test]
    public void GrammarProperlyReadsSymbols() {
      var compareList = new List<string>();
      compareList.Add("S");

      Assert.AreEqual(compareList, TestGrammar.NonTerminals);

      compareList.Clear();
      compareList.Add("a");
      compareList.Add("b");
      
      Assert.AreEqual(compareList, TestGrammar.Terminals);
      
      Assert.Pass();
    }

    [Test]
    public void GrammarProperlyLoadsProductions() {
      Assert.AreEqual(3, TestGrammar.Productions.Count);
      Assert.AreEqual(new Production("S -> a S b S"), TestGrammar.Productions[1]);
      Assert.AreEqual(new Production("S -> a"), TestGrammar.Productions[2]);
      
      Assert.Pass();
    }

    [Test]
    public void StateGeneratorMakesStates() {
      Assert.AreEqual(6, TestGenerator.States.Count);
    }
  }
}