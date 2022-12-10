namespace _Scripts.Utility.Serialization
{
    public interface IPersistentData
    {
        public void SaveData(ref GameData data); // can modify
        public void LoadData(GameData data); // read only
    }
}