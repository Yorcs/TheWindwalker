float calc_gradient(float3 p, int gradient_type)
{
    float x, y, z;
    x = p.x;
    y = p.y;
    z = p.z;
    if (gradient_type == 0) { /* linear */
        return x;
    }
    else if (gradient_type == 1) { /* quadratic */
        float r = max(x, 0.0);
        return r * r;
    }
    else if (gradient_type == 2) { /* easing */
        float r = min(max(x, 0.0), 1.0);
        float t = r * r;
        return (3.0 * t - 2.0 * t * r);
    }
    else if (gradient_type == 3) { /* diagonal */
        return (x + y) * 0.5;
    }
    else if (gradient_type == 4) { /* radial */
        return atan2(x, y) / (3.14159265358979323846 * 2) + 0.5;
    }
    else {
        /* Bias a little bit for the case where p is a unit length vector,
         * to get exactly zero instead of a small random value depending
         * on float precision. */
        float r = max(0.999999 - sqrt(x * x + y * y + z * z), 0.0);
        if (gradient_type == 5) { /* quadratic sphere */
            return r * r;
        }
        else if (gradient_type == 6) { /* sphere */
            return r;
        }
    }
    return 0.0;
}

void node_tex_gradient(float3 co, float gradient_type, out float fac_out, out float4 col_out)
{
    float f = calc_gradient(co, int(gradient_type));
    f = clamp(f, 0.0, 1.0);

    fac_out = f;
    col_out = float4(f, f, f, 1.0);
}