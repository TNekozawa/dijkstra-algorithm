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

            // 各拠点間のエッジを作成し, 各拠点に紐づける
            foreach (string[] csvRow in csv)
            {
                // 出発地点のオブジェクトを取得する
                string fromName = csvRow[0];
                int fromId = NameIdDict[fromName];
                Location fromLocation = NodeDictionary[fromId];

                // 到着地点のオブジェクトを取得する
                string toName = csvRow[1];
                int toId = NameIdDict[toName];
                Location toLocation = NodeDictionary[toId];

                // 交通手段を取得する
                string transType = csvRow[2];

                // 運賃の生データを取得し, intに変換する
                string rawFare = csvRow[3];
                if (int.TryParse(rawFare, out int fare))
                {

                }
                else
                {
                    // intに変換できないので例外
                    throw new Exception($"Fare is invalid: {rawFare}");
                }

                // 時間の生データを取得し, intに変換する
                string rawTime = csvRow[4];
                if (int.TryParse(rawTime, out int time))
                {

                }
                else
                {
                    // intに変換できないので例外
                    throw new Exception($"Time is invalid: {rawTime}");
                }

                // 計算ターゲットのコストをセットする
                // 時間で計算するor運賃で計算する
                int cost = costType switch
                {
                    CostType.Time => time,
                    CostType.Fare => fare,
                    _ => throw new Exception($"Invalid value{costType}"),
                };

                // エッジのオブジェクトを生成し, fromのNodeにエッジをセットする
                Route route = new(toLocation, cost, transType, fare, time);
                fromLocation.SetEdge(route);
                // 無向グラフにするため, 反対方向のEdgeを生成してToNodeにセットする
                Route invRoute = new Route(fromLocation, cost, transType, fare, time);
                toLocation.SetEdge(invRoute);
            }

            return NodeDictionary;
        }
    }
}
