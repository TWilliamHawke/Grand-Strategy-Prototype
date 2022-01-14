using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field)]
public class SpritePreviewAttribute : PropertyAttribute
{
    public float width = 64f;
    public float height = 64f;

    public SpritePreviewAttribute()
    {
    }

    public SpritePreviewAttribute(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public SpritePreviewAttribute(float width, float height)
    {
        this.width = width;
        this.height = height;
    }
}

public interface ISpriteGetter
{
    Sprite sprite { get; }
}

public interface ISpriteProperty
{
    Sprite sprite { get; set; }
}
