using System.Collections.Generic;

namespace PhlegmaticOne.FruitNinja.Shared
{
    public class GameDataByBlocks : GameDataBase
    {
        public Dictionary<BlockTypeShared, int> BlocksData { get; set; }
    }
}