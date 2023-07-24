using System.Collections.Generic;
using ET.Client;
using ET.EventType;
using Unity.Mathematics;

namespace ET
{
    public class GameSweetComponentAwakeSystem: AwakeSystem<GameSweetComponent, string>
    {
        protected override void Awake(GameSweetComponent self, string name)
        {
            var config = StageConfigCategory.Instance.GetByName(name);
            self.Sweets = new GameSweet[(int) config.Size.X, (int) config.Size.Y];
        }
    }

    public class GameSweetComponentDestroySystem: DestroySystem<GameSweetComponent>
    {
        protected override void Destroy(GameSweetComponent self)
        {
        }
    }

    [FriendOfAttribute(typeof (ET.GameSweetComponent))]
    public static class GameSweetComponentSystem
    {
        public static GameSweet Add(this GameSweetComponent self, int sweetId, float2 pos)
        {
            var sweet = self.AddChild<GameSweet, int>(sweetId);
            var newPos = pos.ToInt2();
            self.Sweets[newPos.x, newPos.y] = sweet;

            return sweet;
        }

        public static void ClearAll(this GameSweetComponent self)
        {
            foreach (var sweet in self.Sweets)
            {
                sweet.Dispose();
            }
        }

        public static void Match(this GameSweetComponent self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new StageStartMatch() { StartMatch = true });
            self.AddedMatchIndex.Clear();

            var config = StageConfigCategory.Instance.GetByName(self.DomainScene().Name);
            int xColumn = (int) config.Size.X;
            int yRow = (int) config.Size.Y;
            using var fetchList = ListComponent<GameSweet>.Create();

            using var allMatchList = ListComponent<List<GameSweet>>.Create();
            int mathListIndex = 0;

            //行遍历匹配
            for (int y = yRow - 1; y >= 0; y--)
            {
                var anchorSweet = self.Sweets[0, y];
                if (y != yRow - 1)
                    mathListIndex++;
                
                allMatchList.Add(new List<GameSweet>());
                allMatchList[mathListIndex].Add(self.Sweets[0, y]);

                for (int x = 1; x < xColumn; x++)
                {
                    if (self.Sweets[x, y].ConfigId == anchorSweet.ConfigId)
                    {
                        if (allMatchList.Count < mathListIndex + 1)
                        {
                            allMatchList.Add(new List<GameSweet>());
                        }

                        allMatchList[mathListIndex].Add(self.Sweets[x, y]);
                    }
                    else
                    {
                        mathListIndex++;
                        if (allMatchList.Count < mathListIndex + 1)
                        {
                            allMatchList.Add(new List<GameSweet>());
                        }

                        allMatchList[mathListIndex].Add(self.Sweets[x, y]);
                        anchorSweet = self.Sweets[x, y];
                    }
                }
            }

            // Log.Debug($"Total match count{allMatchList.Count}");
            //
            // for (int i = 0; i < allMatchList.Count; i++)
            // {
            //     Log.Debug($"Index {i} Count {allMatchList[i].Count} ConfigId{allMatchList[i][0].ConfigId}");
            // }

            var columnIndex = mathListIndex;
            //列遍历
            for (int x = 0; x < xColumn; x++)
            {
                var anchorSweet = self.Sweets[x, 5];
                mathListIndex++;
                
                allMatchList.Add(new List<GameSweet>());
                allMatchList[mathListIndex].Add(anchorSweet);

                for (int y = yRow - 2; y >= 0; y--)
                {
                    if (self.Sweets[x, y].ConfigId == anchorSweet.ConfigId)
                    {
                        if (allMatchList.Count < mathListIndex + 1)
                        {
                            allMatchList.Add(new List<GameSweet>());
                        }

                        allMatchList[mathListIndex].Add(self.Sweets[x, y]);
                    }
                    else
                    {
                        mathListIndex++;
                        if (allMatchList.Count < mathListIndex + 1)
                        {
                            allMatchList.Add(new List<GameSweet>());
                        }

                        allMatchList[mathListIndex].Add(self.Sweets[x, y]);
                        anchorSweet = self.Sweets[x, y];
                    }
                }
            }

            // Log.Debug($"Total match count{allMatchList.Count}");
            //
            // for (int i = columnIndex+1; i < allMatchList.Count; i++)
            // {
            //     Log.Debug($"Index {i} Count {allMatchList[i].Count} ConfigId{allMatchList[i][0].ConfigId}");
            // }

            self.FinalMatchList.Clear();
            int finalMatchIndex = 0;
            for (int i = 0; i < columnIndex; i++)
            {
                if (allMatchList[i].Count > 2)
                {
                    for (int j = columnIndex + 1; j < allMatchList.Count; j++)
                    {
                        if (allMatchList[j].Count > 2)
                        {
                            if (allMatchList[i][0].ConfigId == allMatchList[j][0].ConfigId)
                            {
                                bool near = false;
                                for (int k = 0; k < allMatchList[i].Count; k++)
                                {
                                    for (int l = 0; l < allMatchList[j].Count; l++)
                                    {
                                        near |= allMatchList[i][k].IsNear(allMatchList[j][l]);
                                        if (near)
                                        {
                                            break;
                                        }
                                    }
                                }

                                if (near)
                                {
                                    if (!self.AddedMatchIndex.ContainsKey(i) && !self.AddedMatchIndex.ContainsKey(j))
                                    {
                                        self.FinalMatchList.Add(new HashSet<GameSweet>());
                                        foreach (var sweet in allMatchList[i])
                                        {
                                            self.FinalMatchList[finalMatchIndex].Add(sweet);
                                        }

                                        foreach (var sweet in allMatchList[j])
                                        {
                                            self.FinalMatchList[finalMatchIndex].Add(sweet);
                                        }

                                        self.AddedMatchIndex.Add(i, finalMatchIndex);
                                        self.AddedMatchIndex.Add(j, finalMatchIndex);

                                        finalMatchIndex++;
                                    }
                                    else
                                    {
                                        if (!self.AddedMatchIndex.ContainsKey(i))
                                        {
                                            foreach (var sweet in allMatchList[i])
                                            {
                                                self.FinalMatchList[self.AddedMatchIndex[j]].Add(sweet);
                                            }

                                            self.AddedMatchIndex.Add(i, self.AddedMatchIndex[j]);
                                        }

                                        if (!self.AddedMatchIndex.ContainsKey(j))
                                        {
                                            foreach (var sweet in allMatchList[j])
                                            {
                                                self.FinalMatchList[self.AddedMatchIndex[i]].Add(sweet);
                                            }

                                            self.AddedMatchIndex.Add(j, self.AddedMatchIndex[i]);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!self.AddedMatchIndex.ContainsKey(i))
                                    {
                                        self.AddToFinalMatch(allMatchList[i], true, ref finalMatchIndex);
                                        self.AddedMatchIndex.Add(i, finalMatchIndex - 1);
                                    }

                                    if (!self.AddedMatchIndex.ContainsKey(j))
                                    {
                                        self.AddToFinalMatch(allMatchList[j], true, ref finalMatchIndex);
                                        self.AddedMatchIndex.Add(j, finalMatchIndex - 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //将剩余没加入的全加入
            for (int i = 0; i < allMatchList.Count; i++)
            {
                if (allMatchList[i].Count > 2)
                {
                    if (!self.AddedMatchIndex.ContainsKey(i))
                    {
                        self.AddToFinalMatch(allMatchList[i], true, ref finalMatchIndex);
                        self.AddedMatchIndex.Add(i, finalMatchIndex - 1);
                    }
                }
            }

            Log.Debug($"Final match count{self.FinalMatchList.Count}");

            for (int i = 0; i < self.FinalMatchList.Count; i++)
            {
                Log.Debug($"Index {i} Count{self.FinalMatchList[i].Count}");
            }
            
            self.ClearMatchedSweets().Coroutine();
        }

        private static void AddToFinalMatch(this GameSweetComponent self, List<GameSweet> addList, bool needAddIndex, ref int finalMatchIndex)
        {
            self.FinalMatchList.Add(new HashSet<GameSweet>());
            foreach (var sweet in addList)
            {
                self.FinalMatchList[finalMatchIndex].Add(sweet);
            }

            if (needAddIndex)
                finalMatchIndex++;
        }

        public static async ETTask ClearMatchedSweets(this GameSweetComponent self)
        {
            var combo = self.FinalMatchList.Count;
            
            if (combo <= 0)
            {
                EventSystem.Instance.Publish(self.DomainScene(), new StageStartMatch() { StartMatch = false });
                self.Combo = 0;
                return;
            }
            
            var taskList = ListComponent<ETTask>.Create();
            for (int i = 0; i < combo; i++)
            {
                foreach (var sweet in self.FinalMatchList[i])
                {
                    EventSystem.Instance.PublishAsync(self.DomainScene(), new ClearSweet() { Sweet = sweet }).Coroutine();
                    taskList.Add(SweetFactory.CreateAsyncNoReturn(self.DomainScene(), 1007, sweet.PosInGrid));
                }

                EventSystem.Instance.PublishAsync(self.DomainScene(),
                    new Combo() { ComboCount = self.Combo + i + 1, CurrentClearSweetCount = self.FinalMatchList[i].Count }).Coroutine();
                
                await TimerComponent.Instance.WaitAsync(200);
            }
            self.Combo += combo;
            await ETTaskHelper.WaitAll(taskList);

            await TimerComponent.Instance.WaitAsync(1100);//等待Sweet消失的动画
            
            await EventSystem.Instance.PublishAsync(self.DomainScene(), new ReFillAll());
            
            self.Match();
        }
    }
}