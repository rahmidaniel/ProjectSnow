namespace _Scripts.Utility
{
    public struct HouseInfo
    {
        public HouseState State;
        public float Integrity; // 0f - 1f based on fuel levels
    }

    public enum HouseState
    {
        Inside,
        InsideOpen,
        Outside,
        OutsideOpen
    }
}