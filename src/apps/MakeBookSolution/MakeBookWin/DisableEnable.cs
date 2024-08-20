namespace MakeBookWin;
internal class DisableEnable : IDisposable
{
    private readonly Control[] controls;

    public DisableEnable(params Control[] controls)
    {
        this.controls = controls;
        foreach (var control in controls)
        {
            control.Enabled = false;
        }
    }
    public void Dispose()
    {
        foreach (var control in controls)
        {
            control.Enabled = true;
        }
    }
}
