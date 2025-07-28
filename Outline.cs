using Godot;

namespace Freecell;

public partial class Outline : Sprite2D
{
    public void PlayShowAnimation()
    {
        var tween = GetTree().CreateTween();

        GD.Print("playing show animation");
        tween.TweenMethod(
            Callable.From<float>(set_animation_progress),
            0f,
            1f,
            .2
        ).SetEase(Tween.EaseType.InOut);
    }

    private void set_animation_progress(float value)
    {
        ((ShaderMaterial)Material).SetShaderParameter("outline_visibility", value);
    }
}
