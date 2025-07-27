shader_type canvas_item;

uniform vec4 main_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform vec4 accent_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
// uniform vec2 spritesheet_size = vec2(944.0 / 48.0, 385.0 / 64.0);

void fragment() {
    vec4 tex = texture(TEXTURE, UV);
    vec4 col;

    vec2 spritesheet_size = vec2(944.0 / 48.0, 385.0 / 64.0);
    vec2 fixed_uv = UV * spritesheet_size;
    float stripes_angle = 0.50;
    float stripes_spacing = .3;
    float stripes_size = .15;
    float animation_speed = 0.5;

    if (mod(fixed_uv.x - (fixed_uv.y / stripes_angle + TIME * animation_speed), stripes_spacing) > stripes_size) {
        col = main_color;
    }
    else {
        col = accent_color;
    }

    COLOR = vec4(col.xyz, min(tex.a, col.a));
    // COLOR = vec4(fixed_uv, 0.0, 1.0);
}
