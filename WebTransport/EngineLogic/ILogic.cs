using System.Collections.Generic;
using WebTransport.Dto;

namespace WebTransport.EngineLogic
{
    public interface ILogic
    {
        void SearchPairsOfRoutes();
        void SearchRepeatedStops();
    }
}
