using System;

[Serializable]
public class HistoryElement
{
    public string missionName;
    public string completedDate;
    public HistoryElement(string missionName, string completedDate)
    {
        this.missionName = missionName;
        this.completedDate = completedDate;
    }
}
