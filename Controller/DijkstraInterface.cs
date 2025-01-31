using Controller.Utils;
using Models;
using System.Collections.Generic;

namespace Controller
{
    public class DijkstraInterface
    {
        public string? csvPath { get; private set; }
        private List<string[]>? csv;
        private DijkstraProcessor processor;
        public DijkstraInterface()
        {
            Initialize();
            processor = new();
        }
        private void Initialize()
        {
            csvPath = null;
            csv = null;
            processor = new();
        }

        public void SetCsv(string csvPath)
        {
            Initialize();
            this.csvPath = csvPath;
            csv = CsvReader.GetCSV(csvPath, false);
        }

        public void GetPath(int startId)
        {
            if (csv == null)
            {
                // 処理なし
            }
            else
            {
                processor.SetLocDict(csv);
                processor.Process(startId);
            }
        }
    }
}
