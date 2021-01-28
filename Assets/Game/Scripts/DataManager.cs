namespace CarGo.Data
{
    public static class DataManager
    {
        private const string SaveKey = "stage";
        private const int DefaultValue = 1;
        
        private static int _stageData;
        
        public static int LoadData()
        {
            if (UnityEngine.PlayerPrefs.HasKey(SaveKey))
            {
                _stageData = UnityEngine.PlayerPrefs.GetInt(SaveKey);
            }
            else
            {
                UnityEngine.PlayerPrefs.SetInt(SaveKey, DefaultValue);
                _stageData = DefaultValue;
            }

            return _stageData;
        }

        public static void SaveData(int stage)
        {
            _stageData |= (int)UnityEngine.Mathf.Pow(2, stage);
            UnityEngine.PlayerPrefs.SetInt(SaveKey, _stageData);
        }
    }
}