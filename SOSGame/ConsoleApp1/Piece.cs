namespace BoardGamesFramework
{
    public class Piece
    {
        public char Symbol { get; }
        public bool IsOwned { get; private set; }

        public Piece(char symbol)
        {
            Symbol = symbol;
            IsOwned = false;
        }

        public void SetOwnership()
        {
            IsOwned = true;
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}
