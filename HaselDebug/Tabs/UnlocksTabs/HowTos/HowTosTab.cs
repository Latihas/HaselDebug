using FFXIVClientStructs.FFXIV.Client.Game.UI;
using HaselDebug.Abstracts;
using HaselDebug.Interfaces;

namespace HaselDebug.Tabs.UnlocksTabs.HowTos;

[RegisterSingleton<IUnlockTab>(Duplicate = DuplicateStrategy.Append)]
public unsafe class HowTosTab(HowTosTable table) : DebugTab, IUnlockTab
{
    public override string Title => "教学";
    public override bool DrawInChild => false;

    public UnlockProgress GetUnlockProgress()
    {
        if (table.Rows.Count == 0)
            table.LoadRows();

        return new UnlockProgress()
        {
            TotalUnlocks = table.Rows.Count,
            NumUnlocked = table.Rows.Count(row => UIState.Instance()->IsHowToUnlocked(row.RowId)),
        };
    }

    public override void Draw()
    {
        table.Draw();
    }
}
