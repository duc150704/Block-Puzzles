using System.Collections.Generic;

public class GameConfigManager
{
    private static List<IConfigable> _configables = new();

    public void Collect(ConfigData data)
    {
        foreach (var item in _configables)
        {
            item.Save(data);
        }
    }

    public void Apply(ConfigData data)
    {
        foreach (var item in _configables) 
        { 
            item.Load(data);
        }
    }

    public static void Register(IConfigable configable) 
    {
        if (_configables.Contains(configable))
            return;
        _configables.Add(configable);
    }

    public static void Unregister(IConfigable configable) 
    { 
        _configables.Remove(configable);
    }

}
