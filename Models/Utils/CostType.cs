using System;

namespace Models.Utils
{
    public enum CostType
    {
        Fare,
        Time
    }

    public static class CostTypeHandler
    {
        public static CostType GetCostType(string searchMode)
        {
            var costType = searchMode switch
            {
                "0" => CostType.Time,
                "1" => CostType.Fare,
                _ => throw new Exception($"Invalid value{searchMode}"),
            };
            return costType;
        }
    }
}
