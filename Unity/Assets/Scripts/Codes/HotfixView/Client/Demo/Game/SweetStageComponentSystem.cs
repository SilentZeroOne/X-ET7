using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public class SweetStageComponentAwakeSystem: AwakeSystem<SweetStageComponent,string>
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

    [FriendOfAttribute(typeof(ET.SweetStageComponent))]
    public static class SweetStageComponentSystem
    {
        public static void Awake(this SweetStageComponent self, string name)
        {
            self.Config = StageConfigCategory.Instance.GetByName(name);
            
            self.InitGrid();
        }

        public static void InitGrid(this SweetStageComponent self)
        {
            int xHalf = (int) self.Config.Size.X / 2;
            int yHalf = (int) self.Config.Size.Y / 2;
            
            var grid = ResComponent.Instance.LoadAsset<GameObject>("Grid");
            for (int i = -xHalf; i < xHalf; i++)
            {
                for (int j = -yHalf; j < yHalf; j++)
                {
                    var obj = UnityEngine.Object.Instantiate(grid, GlobalComponent.Instance.Grid);
                    obj.transform.position = new Vector3(i + 0.5f, j + 0.5f);

                    SweetFactory.Create(self.ClientScene().CurrentScene(), 1001, new float2(i + 0.5f, j + 0.5f));
                }
            }
        }

        public static void Test(this SweetStageComponent self)
        {

        }
    }
}

