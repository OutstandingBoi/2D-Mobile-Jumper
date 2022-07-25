using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Easing : MonoBehaviour
{
	static float Lerp(float start_value, float end_value, float pct)
	{
		return (start_value + (end_value - start_value) * pct);
	}

	public static float EaseIn(float t)
	{
		return t * t;
	}

	static float Flip(float x)
	{
		return 1 - x;
	}

	public static float EaseOut(float t)
	{
		return Flip(Mathf.Sqrt(Flip(t)));
	}

	public static float EaseInOut(float t)
	{
		return Lerp(EaseIn(t), EaseOut(t), t);
	}

	public static float Spike(float t)
	{
		if (t <= .5f)
			return EaseIn(t / .5f);

		return EaseIn(Flip(t) / .5f);
	}
}
