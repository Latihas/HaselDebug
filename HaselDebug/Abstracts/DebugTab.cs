using System.Collections.Immutable;
using System.Text.RegularExpressions;
using HaselDebug.Interfaces;

namespace HaselDebug.Abstracts;

public abstract partial class DebugTab : IDebugTab
{
    private string? _title = null;
    public virtual string Title
    {
        get
        {
            if (_title == null)
            {
                var nm = NameRegex().Replace(TabRegex().Replace(GetType().Name, ""), "$1 $2");
                _title = nm switch
                {
                    "Unlocks" => "收集",
                    "Addon Config" => "Addon 设置",
                    "Addon Factories" => "Addon 构造",
                    "Addon Inspector" => "Addon 分析",
                    "Addon Names" => "Addon 名称",
                    "Address Inspector" => "地址 分析(有bug)",
                    "Agent Map Event Markers" => "地图事件标记",
                    "Atk Array Data" => "Atk 数据",
                    "Atk Events" => "Atk 事件",
                    "Atk Handler Calls" => "Atk 函数调用",
                    "Beast Tribe" => "友好部族",
                    "Chat" => "聊天",
                    "Config" => "设置",
                    "Excel" => "表格",
                    "Furniture Catalog" => "家具",
                    "Game Window" => "游戏窗口",
                    "Aetherytes" => "传送水晶",
                    "Territory Type" => "地图",
                    "Territory Intended Use" => "地图 可用",
                    "Special Shops" => "商店 特殊",
                    "Shop" => "商店",
                    "Sound Manager" => "音频",
                    "Satisfaction Supply" => "老主顾",
                    "Target" => "目标",
                    "Inventory" => "物品栏",
                    "Inventory Operations" => "物品栏 操作",
                    "Instances" => "副本",
                    "Instance Content Director" => "副本 Director",
                    "Contents Finder Duty List" => "副本 任务搜索器",
                    "Input" => "输入",
                    "Inclusion Shop" => "商店 Inclusion(?)",
                    "Housing" => "房子",
                    "Packet Logs" => "抓包",
                    "Object Tables" => "物体",
                    "Main Commands" => "快捷指令",
                    "Local Player" => "玩家",
                    "Completion" => "定型文",
                    "Unlock Span Length Test" => "收集数据测试",
                    "Rapture Hotbar Module" => "Rapture热键栏",
                    "Rapture Log Module" => "Rapture日志",
                    "Rapture Text Module" => "Rapture文本",
                    "Lua Debug" => "Lua调试",
                    _ => nm
                };
            }
            return _title;
        }
    }
    public ImmutableArray<IDebugTab>? SubTabs { get; protected set; }
    public virtual bool IsEnabled => true;
    public virtual bool IsPinnable => true;
    public virtual bool CanPopOut => true;
    public virtual bool DrawInChild => true;
    public virtual string InternalName => GetType().Name;

    [GeneratedRegex("Tab$")]
    private static partial Regex TabRegex();

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex NameRegex();

    public virtual void Draw() { }

    public bool Equals(IDebugTab? other)
    {
        return other?.Title == _title;
    }
}
