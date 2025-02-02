using Models.Graph.Elements.Concrete;
using Models.Utils;
using System.Collections.Generic;
using System;
namespace Models.Graph
{
    public static class GraphCreator
    {
        public static Dictionary<int, Location> GetNodeDictionary(List<string[]> csv, CostType costType)
        {
            // 拠点名をかぶりなしで取得する
            HashSet<string> locationNameList = [];
            foreach (string[] row in csv)
            {
                string fLocName = row[0];
                string tLocName = row[1];
                _ = locationNameList.Add(fLocName);
                _ = locationNameList.Add(tLocName);
            }

            // 各拠点名にノードIDを割り振り, ノードのディクショナリを作成する
            Dictionary<string, int> NameIdDict = [];
            Dictionary<int, Location> NodeDictionary = [];
            int counter = 0;
            foreach (string locName in locationNameList)
            {
                Location location = new(counter, locName);
                NodeDictionary.Add(counter, location);
                NameIdDict.Add(locName, counter);
                counter++;
            }

            foreach (string[] csvRow in csv)
            {
                string fromName = csvRow[0];
                int fromId = NameIdDict[fromName];
                Location fromLocation = NodeDictionary[fromId];

                string toName = csvRow[1];
                int toId = NameIdDict[fromName];
                Location toLocation = NodeDictionary[toId];

                string transType = csvRow[2];

                string rawFare = csvRow[3];
                if (int.TryParse(rawFare, out int fare))
                {

                }
                else
                {
                    throw new Exception($"Fare is invalid: {rawFare}");
                }

                string rawTime = csvRow[4];
                if (int.TryParse(rawTime, out int time))
                {

                }
                else
                {
                    throw new Exception($"Time is invalid: {rawTime}");
                }

                int cost = costType switch
                {
                    CostType.Time => time,
                    CostType.Fare => fare,
                    _ => throw new Exception($"Invalid value{costType}"),
                };

                Route route = new(toLocation, cost, transType, fare, time);
                fromLocation.SetEdge(route);
            }

            return NodeDictionary;
        }
    }
}
