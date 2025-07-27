shader_type canvas_item;

uniform vec4 main_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform vec4 accent_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);

void fragment() {
    vec4 tex = texture(TEXTURE, UV);
    vec4 col;

    float stripes_angle = 0.50;
    float stripes_spacing = .3;
    float stripes_size = .15;
    float animation_speed = 0.5;

    if (mod(UV.x - (UV.y / stripes_angle + TIME * animation_speed), stripes_spacing) > stripes_size) {
        col = main_color;
    }
    else {
        col = accent_color;
    }

    COLOR = vec4(col.xyz, min(tex.a, col.a));
}
