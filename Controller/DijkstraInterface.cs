using Models;
using Models.Graph.Elements.Concrete;
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

        public List<string> GetPath(string fromLocation, string toLocation)
        {
            List<string> route = [];
            if (DataArray == null)
            {
                // 処理なし
            }
            else
            {
                processor.SetLocDict(DataArray, CostType);
                (List<Location>, List<Route>) calcResult = processor.Process(fromLocation, toLocation);
                route = GetRoute(calcResult);
            }
            return route;
        }

        private static List<string> GetRoute((List<Location>, List<Route>) calcResult)
        {
            List<Location> locationList = calcResult.Item1;
            List<Route> routeList = calcResult.Item2;

            List<string> resultList = [];
            if (routeList.Count == 0)
            {

            }
            else
            {
                int routes = routeList.Count;

                for (int i = 0; i < routes; i++)
                {
                    Location location = locationList[i];
                    resultList.Add(location.Name);

                    Route route = routeList[i];
                    resultList.Add($"↓{route.Transportation}, {route.Fare}円, {route.Time}分");
                }
                resultList.Add(locationList[^1].Name);
            }

            return resultList;
        }
    }
}
