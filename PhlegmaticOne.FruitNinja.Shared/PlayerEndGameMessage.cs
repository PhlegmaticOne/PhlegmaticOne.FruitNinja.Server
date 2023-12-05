using System;

namespace PhlegmaticOne.FruitNinja.Shared
{
    [Serializable]
    public class PlayerEndGameMessage
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public bool IsWin { get; set; }
        public bool IsBreakGame { get; set; }
    }
}