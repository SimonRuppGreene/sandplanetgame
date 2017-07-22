using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseSampler {
	private static float scale = 5f;
	private static float startX = 0f;
	private static float startY = 0f;

	private static float sampleScale = 10.0f;

	public static float[] getPerlinSample (int sizeX, int sizeY) {
		float[] samples = new float[(sizeX + 1) * (sizeY + 1)];
		Debug.Log ("taking # of samples: " + sizeX * sizeY);

		for (int i = 0, y = 0; y <= sizeY; y++) {
			for (int x = 0; x <= sizeX; x++, i++) {
				float xCoord = (startX + (float)x) / sizeX * scale;
				float yCoord = (startY + (float)y) / sizeY * scale;

				float sample = Mathf.PerlinNoise (xCoord, yCoord) * sampleScale;
				Debug.Log ("doing sample " + i + ", y: " + y + " x: " + x);
				samples [i] = sample;
			}
		}

		return samples;
	}
}
