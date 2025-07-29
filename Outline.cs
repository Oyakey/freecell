using Godot;

namespace Freecell;

public partial class Outline : Sprite2D
{
	private Vector2 _defaultScale;

	private void _ready()
	{
		_defaultScale = Scale;
	}

	public void PlayShowAnimation()
	{
		var tween = GetTree().CreateTween();

		GD.Print("playing show animation");
		tween.TweenMethod(
			Callable.From<float>(setAnimationProgress),
			0f,
			1f,
			.2
		).SetEase(Tween.EaseType.InOut);
	}

	public void LerpDragging(float value)
	{
		Scale = _defaultScale.Lerp(_defaultScale * 1.05f, value);
	}

	private void setAnimationProgress(float value)
	{
		((ShaderMaterial)Material).SetShaderParameter("outline_visibility", value);
	}
}
