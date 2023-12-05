using System;

namespace PhlegmaticOne.FruitNinja.Shared
{
    [Serializable]
    public class PlayersSyncMessage
    {
        private static readonly object Lock = new object();
        public PlayerEndGameMessage First { get; set; }
        public PlayerEndGameMessage Second { get; set; }

        public void Add(PlayerEndGameMessage message)
        {
            lock (Lock)
            {
                if (First == null)
                {
                    First = message;
                    return;
                }

                Second = message;
            }
        }

        public bool IsFull() => First != null && Second != null;
    }
}