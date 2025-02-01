using Controller.Utils;
using Models;
using System.Collections.Generic;

namespace Controller
{
    public class DijkstraInterface
    {
        private List<string[]>? DataArray;
        private DijkstraProcessor processor;
        public DijkstraInterface()
        {
            Initialize();
            processor = new();
        }
        private void Initialize()
        {
            DataArray = null;
            processor = new();
        }

        public void SetCsv(List<string[]> csv)
        {
            Initialize();
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
                processor.SetLocDict(DataArray);
                processor.Process(fromLocation, toLocation);
            }
        }
    }
}
