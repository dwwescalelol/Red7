namespace Red7
{
    internal interface IPlayer
    {

        public void DrawFromDeck(CardCollection deck);

        public void ReturnCardsToDeck(CardCollection deck);


        public void PlayToPalette(int i);

        public void PlayToCanvas(int i, CardCollection canvas);
    }
}
