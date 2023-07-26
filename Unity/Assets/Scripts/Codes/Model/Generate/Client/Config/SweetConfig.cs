//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace ET
{

public sealed partial class SweetConfig: Bright.Config.BeanBase
{
    public SweetConfig(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Name = _buf.ReadString();
        Desc = _buf.ReadString();
        PrefabName = _buf.ReadString();
        Type = _buf.ReadInt();
        SpriteName = _buf.ReadString();
        PostInit();
    }

    public static SweetConfig DeserializeSweetConfig(ByteBuf _buf)
    {
        return new SweetConfig(_buf);
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 糖果名称
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; private set; }
    /// <summary>
    /// 预制体名称
    /// </summary>
    public string PrefabName { get; private set; }
    /// <summary>
    /// 类型
    /// </summary>
    public int Type { get; private set; }
    /// <summary>
    /// 图片名称
    /// </summary>
    public string SpriteName { get; private set; }

    public const int __ID__ = -1072213454;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Desc:" + Desc + ","
        + "PrefabName:" + PrefabName + ","
        + "Type:" + Type + ","
        + "SpriteName:" + SpriteName + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}