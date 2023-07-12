namespace ET
{
    namespace EventType
    {
        public struct MoveStart
        {
            public Unit Unit;
        }

        public struct MoveStop
        {
            public Unit Unit;
        }
        
        public struct SweetMoveStart
        {
            public GameSweet Sweet;
        }

        public struct SweetMoveStop
        {
            public GameSweet Sweet;
        }
    }
}