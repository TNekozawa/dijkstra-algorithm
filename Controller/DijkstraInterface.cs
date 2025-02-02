using Models;
using Models.Utils;
using System.Collections.Generic;

namespace Controller
{
    public class DijkstraInterface
    {
        private List<string[]>? DataArray;
        private CostType CostType;
        private DijkstraProcessor processor;
        public DijkstraInterface()
        {
            Initialize();
            CostType = CostType.Fare;
            processor = new();
        }
        private void Initialize()
        {
            DataArray = null;
            processor = new();
        }

        public void SetCsv(List<string[]> csv, CostType costType)
        {
            Initialize();
            CostType = costType;
            DataArray = csv;
        }

        public void GetPath(string fromLocation, string toLocation)
        {
            if (DataArray == null)
            {
                // 処理なし
            }
            else
            {
                processor.SetLocDict(DataArray, CostType);
                processor.Process(fromLocation, toLocation);
            }
        }
    }
}
