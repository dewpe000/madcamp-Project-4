using UnityEngine;
using System.Collections;

public class ColorHelper
{
	//red == 1 , green == 2, blue == 4
	public static int getColorValue(Color color) {
		if (color.r == Color.red.r
			&& color.g == Color.red.g
			&& color.b == Color.red.b) {
			return 1;
		} else if (color.r == Color.green.r
			&& color.g == Color.green.g
			&& color.b == Color.green.b) {
			return 2;
		} else if (color.r == Color.blue.r
			&& color.g == Color.blue.g
			&& color.b == Color.blue.b) {
			return 4;
		} else {
			return 0;
		}
	}

	public static int getTargetColor(bool isRequiredRed, bool isRequiredGreen, bool isRequiredBlue) {
		int result = 0;
		result += isRequiredRed ? ColorHelper.getColorValue(Color.red) : 0;
		result += isRequiredGreen ? ColorHelper.getColorValue(Color.green) : 0;
		result += isRequiredBlue ? ColorHelper.getColorValue(Color.blue) : 0;
		return result;
	}
}

