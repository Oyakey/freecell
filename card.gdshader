shader_type canvas_item;

uniform vec4 main_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform vec4 accent_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform float outline_visibility : hint_range(0.0, 1.0) = 1.0;

bool diagonal_transition(vec2 uv, float lerp) {
  float progression = uv.y - (uv.y - uv.x) / 2.;
  return progression < outline_visibility;
}

bool angular_transition(vec2 uv, float lerp) {
    vec2 coords = (uv - vec2(0.5)) * 2.;
    float visible_angle = 2. * (lerp - 0.5) * PI;
    return atan(coords.y, coords.x) < visible_angle;
}

void fragment() {
    vec4 tex = texture(TEXTURE, UV);
    vec4 col;

    float stripes_angle = 0.50;
    float stripes_spacing = .3;
    float stripes_size = .15;
    float animation_speed = 0.5;

    // Diagonal stripes
    if (mod(UV.x - (UV.y / stripes_angle + TIME * animation_speed), stripes_spacing) > stripes_size) {
        col = main_color;
    }
    else {
        col = accent_color;
    }

    // Transition
    bool should_draw = diagonal_transition(UV, outline_visibility);

    if (should_draw)
    {
        COLOR = vec4(col.xyz, min(tex.a, col.a));
    }
    else
    {
        COLOR = vec4(col.xyz, 0.0);
    }
}
