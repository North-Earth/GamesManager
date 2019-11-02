namespace GamesManager.Launcher.Models.Enums
{
    public enum ProcessStatus
    {
        Checking = 0,
        Downloading = 1,
        Installing = 2,
        Complete = 3,
        Done = 4,
        Error = 5,
        Waiting = 6,
        Playing = 7
    }

    public enum ProcessButtonStatus
    {
        Play = 0,
        Install = 1,
        Update = 2,
        Cancel = 3,
        Check = 4
    }
}
