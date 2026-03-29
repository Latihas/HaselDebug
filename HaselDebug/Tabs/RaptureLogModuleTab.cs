using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using HaselDebug.Abstracts;
using HaselDebug.Interfaces;

namespace HaselDebug.Tabs;

[RegisterSingleton<IDebugTab>(Duplicate = DuplicateStrategy.Append)]
public unsafe class RaptureLogModuleTab : DebugTab
{
    private string _input = "";
    private readonly List<string> _messages = [];

    public override void Draw()
    {
        var raptureLogModule = RaptureLogModule.Instance();

        ImGui.Text($"当前日志索引: {raptureLogModule->LogModule.LogMessageIndex}");
        ImGui.Text($"日志消息数量: {raptureLogModule->LogModule.LogMessageCount}");

        if (ImGui.Button("清空"))
        {
            _messages.Clear();
        }

        if (ImGui.Button("读取消息"))
        {
            _messages.Clear();
            for (var i = 0; i < raptureLogModule->LogModule.LogMessageCount; i++)
            {
                raptureLogModule->GetLogMessage(i, out var message);
                _messages.Add(((ReadOnlySeStringSpan)message).ToMacroString());
            }
        }

        if (ImGui.InputText("打印字符串", ref _input, 255, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            raptureLogModule->PrintString(_input);
        }

        if (ImGui.InputText("打印消息", ref _input, 255, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            using var sender = new Utf8String("me");
            using var message = new Utf8String(_input);
            raptureLogModule->PrintMessage(27, &sender, &message, (int)DateTimeOffset.Now.ToUnixTimeMilliseconds());
        }

        var index = 0;
        foreach (var message in _messages)
        {
            ImGui.Text($"[{index++}] {message}");
        }
    }
}
