using Godot;

namespace Freecell;

public partial class Outline : Sprite2D
{
    private Vector2 _defaultScale;

    public Vector2 DefaultScale { get => _defaultScale; }

    private void _ready()
    {
        _defaultScale = Scale;
    }

    public void PlayShowAnimation()
    {
        var tween = GetTree().CreateTween();

        GD.Print("playing show animation");
        tween.TweenMethod(
            Callable.From<float>(setShaderVisibilityProgress),
            0f,
            1f,
            .2
        ).SetEase(Tween.EaseType.InOut);
    }

    private void setShaderVisibilityProgress(float value)
    {
        ((ShaderMaterial)Material).SetShaderParameter("outline_visibility", value);
    }
}
