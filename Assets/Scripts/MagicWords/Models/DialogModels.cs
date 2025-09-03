using System;
using System.Collections.Generic;

[Serializable]
public class DialogModels
{
    public List<DialogData> dialogue;
    public List<AvatarData> avatars;
}

[Serializable]
public class DialogData
{
    public string name;
    public string text;
}

[Serializable]
public class AvatarData
{
    public string name;
    public string url;
    public string position;
}