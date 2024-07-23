using System.Collections.Generic;

[System.Serializable]

public class EnvironmentData
{
    public List<string> pickedUpItems;

    public EnvironmentData(List<string> _pickedupItems) 
    {
        pickedUpItems = _pickedupItems;
    }

}
