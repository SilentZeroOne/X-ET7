﻿using Unity.Mathematics;

namespace ET
{
    namespace EventType
    {
        public struct SceneChangeStart
        {
        }
        
        public struct SceneChangeFinish
        {
        }
        
        public struct AfterCreateClientScene
        {
        }
        
        public struct AfterCreateCurrentScene
        {
        }

        public struct AppStartInitFinish
        {
        }

        public struct LoginFinish
        {
        }

        public struct EnterMapFinish
        {
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }
        
        public struct AfterSweetCreate
        {
            public GameSweet Sweet;
        }
        
        public struct ClearSweet
        {
            public GameSweet Sweet;
        }
        
        public struct ReFillAll
        {
            
        }

        public struct ScreenResolutionChange
        {
            public int2 Resolution;
        }
    }
}