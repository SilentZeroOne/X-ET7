using System.Collections.Generic;

namespace ET
{
    public partial class StageConfigCategory
    {
        private Dictionary<string, StageConfig> _nameMap;

        partial void PostInit()
        {
            _nameMap = new Dictionary<string, StageConfig>();
            foreach (var data in this._dataList)
            {
                this._nameMap[data.Name] = data;
            }
        }

        public StageConfig GetByName(string name)
        {
            return this._nameMap[name];
        }
    }
}

