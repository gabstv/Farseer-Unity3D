using UnityEngine;
using System.Collections;

public static class GizmosHelper
{
	public static void DrawString(Vector3 position, string val)
	{
		DrawString(position, Vector3.one, val);
	}
	public static void DrawString(Vector3 position, Vector3 scale, string val)
	{
		Vector3 offset = Vector3.zero;
		
		for(int i = 0; i < val.Length; i++)
		{
			switch(val[i])
			{
			case '0':
				DrawChar_0(position + offset, scale);
				break;
			case '1':
				DrawChar_1(position + offset, scale);
				break;
			case '2':
				DrawChar_2(position + offset, scale);
				break;
			case '3':
				DrawChar_3(position + offset, scale);
				break;
			case '4':
				DrawChar_4(position + offset, scale);
				break;
			case '5':
				DrawChar_5(position + offset, scale);
				break;
			case '6':
				DrawChar_6(position + offset, scale);
				break;
			case '7':
				DrawChar_7(position + offset, scale);
				break;
			case '8':
				DrawChar_8(position + offset, scale);
				break;
			case '9':
				DrawChar_9(position + offset, scale);
				break;
			}
			offset.x += 12f * 0.025f * scale.x;
		}
	}
	private static void DrawChar_0(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[4,2];
		points[0,0] = d0_0; points[0,1] = d1_0;
		points[1,0] = d1_0; points[1,1] = d1_2;
		points[2,0] = d1_2; points[2,1] = d0_2;
		points[3,0] = d0_2; points[3,1] = d0_0;
		DrawChar(position, ref points, 4, scale);
	}
	private static void DrawChar_1(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[2,2];
		points[0,0] = d0_1; points[0,1] = d1_0;
		points[1,0] = d1_0; points[1,1] = d1_2;
		DrawChar(position, ref points, 2, scale);
	}
	private static void DrawChar_2(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[5,2];
		points[0,0] = d0_0; points[0,1] = d1_0;
		points[1,0] = d1_0; points[1,1] = d1_1;
		points[2,0] = d1_1; points[2,1] = d0_1;
		points[3,0] = d0_1; points[3,1] = d0_2;
		points[4,0] = d0_2; points[4,1] = d1_2;
		DrawChar(position, ref points, 5, scale);
	}
	private static void DrawChar_3(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[4,2];
		points[0,0] = d0_0; points[0,1] = d1_0;
		points[1,0] = d1_0; points[1,1] = d1_2;
		points[2,0] = d0_1; points[2,1] = d1_1;
		points[3,0] = d0_2; points[3,1] = d1_2;
		DrawChar(position, ref points, 4, scale);
	}
	private static void DrawChar_4(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[3,2];
		points[0,0] = d1_1; points[0,1] = d0_1;
		points[1,0] = d0_1; points[1,1] = d1_0;
		points[2,0] = d1_0; points[2,1] = d1_2;
		DrawChar(position, ref points, 3, scale);
	}
	private static void DrawChar_5(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[5,2];
		points[0,0] = d0_0; points[0,1] = d1_0;
		points[1,0] = d0_0; points[1,1] = d0_1;
		points[2,0] = d1_1; points[2,1] = d0_1;
		points[3,0] = d1_1; points[3,1] = d1_2;
		points[4,0] = d0_2; points[4,1] = d1_2;
		DrawChar(position, ref points, 5, scale);
	}
	private static void DrawChar_6(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[6,2];
		points[0,0] = d0_0; points[0,1] = d1_0;
		points[1,0] = d0_0; points[1,1] = d0_1;
		points[2,0] = d1_1; points[2,1] = d0_1;
		points[3,0] = d1_1; points[3,1] = d1_2;
		points[4,0] = d0_2; points[4,1] = d1_2;
		points[5,0] = d0_2; points[5,1] = d0_1;
		DrawChar(position, ref points, 6, scale);
	}
	private static void DrawChar_7(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[2,2];
		points[0,0] = d0_0; points[0,1] = d1_0;
		points[1,0] = d1_0; points[1,1] = d1_2;
		DrawChar(position, ref points, 2, scale);
	}
	private static void DrawChar_8(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[7,2];
		points[0,0] = d0_0; points[0,1] = d1_0;
		points[1,0] = d0_1; points[1,1] = d1_1;
		points[2,0] = d0_2; points[2,1] = d1_2;
		points[3,0] = d0_0; points[3,1] = d0_1;
		points[4,0] = d1_0; points[4,1] = d1_1;
		points[5,0] = d0_1; points[5,1] = d0_2;
		points[6,0] = d1_1; points[6,1] = d1_2;
		DrawChar(position, ref points, 7, scale);
	}
	private static void DrawChar_9(Vector3 position, Vector3 scale)
	{
		Vector3[,] points = new Vector3[6,2];
		points[0,0] = d0_0; points[0,1] = d1_0;
		points[1,0] = d0_1; points[1,1] = d1_1;
		points[2,0] = d0_2; points[2,1] = d1_2;
		points[3,0] = d0_0; points[3,1] = d0_1;
		points[4,0] = d1_0; points[4,1] = d1_1;
		points[5,0] = d1_1; points[5,1] = d1_2;
		DrawChar(position, ref points, 6, scale);
	}
	private static void DrawChar(Vector3 position, ref Vector3[,] points, int cols, Vector3 scale)
	{
		for(int i = 0; i < cols; i++)
		{
			points[i, 0].Scale(scale);
			points[i, 1].Scale(scale);
			Gizmos.DrawLine(position + points[i, 0] * 0.025f, position + points[i, 1] * 0.025f);
		}
	}
	private static Vector3 d0_0 = new Vector3(-5f, 5f, 0f);
	private static Vector3 d1_0 = new Vector3(5f, 5f, 0f);
	private static Vector3 d0_1 = new Vector3(-5f, 0f, 0f);
	private static Vector3 d1_1 = new Vector3(5f, 0f, 0f);
	private static Vector3 d0_2 = new Vector3(-5f, -5f, 0f);
	private static Vector3 d1_2 = new Vector3(5f, -5f, 0f);
}
