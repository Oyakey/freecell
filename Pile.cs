interface Pile {
  public int CardsOnStack { get; }
  public void AddCard(int cardValue);
  public void RemoveCard(int cardValue);
}
