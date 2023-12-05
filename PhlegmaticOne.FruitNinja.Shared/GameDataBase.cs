using System;

namespace PhlegmaticOne.FruitNinja.Shared
{
    [Serializable]
    public abstract class GameDataBase
    {
        public GameModeType GameModeType { get; set; }
    }
}