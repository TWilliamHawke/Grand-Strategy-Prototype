using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

[System.Serializable]
public class PartialName
{
    [SerializeField] string namePrefix;
    [SerializeField] string nameCore;
    [SerializeField] string nameSuffix;

    public string prefix => namePrefix;
    public string main => nameCore;
    public string suffix => nameSuffix;

    public PartialName(string namePrefix, string nameCore, string nameSuffix)
    {
        this.namePrefix = namePrefix;
        this.nameCore = nameCore;
        this.nameSuffix = nameSuffix;
    }

    public string GetFullName()
    {
        var name = $"{namePrefix} {nameCore} {nameSuffix}".Trim();
        var capitalizedName = name.First().ToString().ToUpper() + name.Substring(1);

        var regex = new Regex(@" -");

        return regex.Replace(capitalizedName, "-");
    }

}
