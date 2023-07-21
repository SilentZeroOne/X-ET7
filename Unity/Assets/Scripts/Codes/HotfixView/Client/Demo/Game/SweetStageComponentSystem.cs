using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public class SweetStageComponentAwakeSystem: AwakeSystem<SweetStageComponent, string>
    {
        protected override void Awake(SweetStageComponent self, string name)
        {
            self.Awake(name);
        }
    }

    public class SweetStageComponentDestroySystem: DestroySystem<SweetStageComponent>
    {
        protected override void Destroy(SweetStageComponent self)
        {
        }
    }

    [FriendOfAttribute(typeof (ET.SweetStageComponent))]
    [FriendOfAttribute(typeof (ET.GameSweetComponent))]
    public static class SweetStageComponentSystem
    {
        public static void Awake(this SweetStageComponent self, string name)
        {
            self.Config = StageConfigCategory.Instance.GetByName(name);
            self.FillSpeed = 6f;
            self.CurrentTurn = self.Config.Turn;
            self.CurrentDragTime = self.Config.HoldTime;

            self.InitGrid();
            self.AllFill().Coroutine();
        }

        public static void InitGrid(this SweetStageComponent self)
        {
            int x = (int) self.Config.Size.X;
            int y = (int) self.Config.Size.Y;

            var grid = ResComponent.Instance.LoadAsset<GameObject>("Grid");
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var obj = UnityEngine.Object.Instantiate(grid, GlobalComponent.Instance.Grid);
                    var pos = SweetPosHelper.CorrectPosition(self.ClientScene().CurrentScene(), new float2(i, j));
                    obj.transform.position = pos.ToVector2();

                    SweetFactory.Create(self.ClientScene().CurrentScene(), 1007, new float2(i, j)); //先全生成Empty的糖果
                }
            }
        }

        public static async ETTask AllFill(this SweetStageComponent self)
        {
            self.CanOperate = false;
            var count = 0;
            while (await self.FillSweet())
            {
                Log.Debug($"Count {count++}");
                await TimerComponent.Instance.WaitAsync(150);
            }

            self.CanOperate = true;
            await ETTask.CompletedTask;
        }

        public static async ETTask<bool> FillSweet(this SweetStageComponent self)
        {
            bool filledNotFinished = false;
            int x = (int) self.Config.Size.X;
            int y = (int) self.Config.Size.Y;
            GameSweetComponent sweetComponent = self.GetParent<Scene>().GetComponent<GameSweetComponent>();
            
            using var list = ListComponent<ETTask>.Create();
            
            for (int i = y - 2; i >= 0; i--)
            {
                for (int j = 0; j < x; j++)
                {
                    GameSweet sweet = sweetComponent.Sweets[j, i];
                    if (sweet.CanMoveDown)
                    {
                        GameSweet sweetBlow = sweetComponent.Sweets[j, i + 1];
                        var moveComponent = sweet.GetComponent<SweetMoveComponent>();
                        if (sweetBlow.Config.Type == (int) SweetType.Empty)
                        {
                            moveComponent.MoveToAsync(j, i + 1, self.FillSpeed).Coroutine();
                            sweetComponent.Sweets[j, i + 1] = sweet;
                            sweet.PosInGrid = new float2(j, i + 1);

                            var aboveIndex = i - 1;
                            while (aboveIndex >= 0)
                            {
                                GameSweet sweetAbove = sweetComponent.Sweets[j, aboveIndex];
                                sweetComponent.Sweets[j, aboveIndex + 1] = sweetAbove;
                                sweetAbove.GetComponent<SweetMoveComponent>().MoveToAsync(j, aboveIndex + 1, self.FillSpeed).Coroutine();
                                sweetAbove.PosInGrid = new float2(j, aboveIndex + 1);
                                aboveIndex--;
                            }
                            
                            list.Add(self.CreateAndMove(j,0));
                            
                            sweetBlow.Dispose();

                            filledNotFinished = true;
                        }
                    }
                }
            }

            await ETTaskHelper.WaitAll(list);
            list.Clear();
            
            
            //最上层
            for (int i = 0; i < x; i++)
            {
                GameSweet sweet = sweetComponent.Sweets[i, 0];
                if (sweet.Config.Type == (int) SweetType.Empty)
                {
                    list.Add(self.CreateAndMove(i, 0));
                    
                    sweet.Dispose();
                    filledNotFinished = true;
                }
            }
            await ETTaskHelper.WaitAll(list);
            
            return filledNotFinished;
        }

        private static async ETTask CreateAndMove(this SweetStageComponent self,int x,int y)
        {
            var newSweet = await self.CreateAndReturn(x, y);
            var moveComponent = newSweet.GetComponent<SweetMoveComponent>();
            moveComponent?.MoveToAsync(x, y, self.FillSpeed).Coroutine();
        }

        private static async ETTask<GameSweet> CreateAndReturn(this SweetStageComponent self, int x, int y)
        {
            GameSweetComponent sweetComponent = self.GetParent<Scene>().GetComponent<GameSweetComponent>();
            var newSweet = await SweetFactory.CreateAsync(self.ClientScene().CurrentScene(), RandomGenerator.RandomNumber(1001, 1007), new float2(x, y));
            sweetComponent.Sweets[x, y] = newSweet;
            newSweet.RealPos = new float2(x, -1);
            Debug.Log($"Create {x} {y}");
            return newSweet;
        }
        
        private static async ETTask CreateSweet(this SweetStageComponent self, int x, int y)
        {
            await self.CreateAndReturn(x, y);
        }

        public static void Test(this SweetStageComponent self)
        {
        }
    }
}