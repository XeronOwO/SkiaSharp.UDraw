using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharp.UDraw
{
	/// <summary>
	/// Matrix 2x2
	/// </summary>
	public struct Matrix2x2
	{
		#region Constructor

		/// <summary>
		/// Create a matrix 2x2 initialized with zero
		/// </summary>
		public Matrix2x2()
		{
			m00 = 0f;
			m01 = 0f;
			m10 = 0f;
			m11 = 0f;
		}

		/// <summary>
		/// Create a matrix 2x2 initialized with source
		/// </summary>
		public Matrix2x2(Matrix2x2 source)
		{
			m00 = source.m00;
			m01 = source.m01;
			m10 = source.m10;
			m11 = source.m11;
		}

		/// <summary>
		/// Create a matrix 2x2 initialized with columns
		/// </summary>
		/// <param name="column0">Column 0</param>
		/// <param name="column1">Column 1</param>
		public Matrix2x2(Vector2 column0, Vector2 column1)
		{
			m00 = column0.x;
			m01 = column1.x;
			m10 = column0.y;
			m11 = column1.y;
		}

		/// <summary>
		/// Create a matrix 2x2 initialized with values
		/// </summary>
		/// <param name="m00">x _<br/>_ _</param>
		/// <param name="m01">_ x<br/>_ _</param>
		/// <param name="m10">_ _<br/>x _</param>
		/// <param name="m11">_ _<br/>_ x</param>
		public Matrix2x2(float m00, float m01, float m10, float m11)
		{
			this.m00 = m00;
			this.m01 = m01;
			this.m10 = m10;
			this.m11 = m11;
		}

		#endregion

		#region Properties

		/// <summary>
		/// x _<br/>_ _
		/// </summary>
		public float m00;

		/// <summary>
		/// _ x<br/>_ _
		/// </summary>
		public float m01;

		/// <summary>
		/// _ _<br/>x _
		/// </summary>
		public float m10;

		/// <summary>
		/// _ _<br/>_ x
		/// </summary>
		public float m11;

		private static readonly Matrix2x2 _zeroMatrix = new();

		/// <summary>
		/// Get a zero matrix
		/// </summary>
		public static Matrix2x2 zero => _zeroMatrix;

		private static readonly Matrix2x2 _unitMatrix = new(1, 0, 0, 1);

		/// <summary>
		/// Get a unix matrix
		/// </summary>
		public static Matrix2x2 unit => _unitMatrix;

		/// <summary>
		/// Get the determinant of the matrix
		/// </summary>
		public float determinant => m00 * m11 - m01 * m10;

		/// <summary>
		/// Whether the matrix has inverse
		/// </summary>
		public bool hasInverse => !Mathf.Approximately(0, determinant);

		/// <summary>
		/// Get the adjoint matrix
		/// </summary>
		public Matrix2x2 adjoint
		{
			get
			{
				return new(m11, -m01, -m10, -m11);
			}
		}

		/// <summary>
		/// Get the inverse matrix
		/// </summary>
		public Matrix2x2 inverse
		{
			get
			{
				var determinant = this.determinant;
				return adjoint / determinant;
			}
		}

		#endregion

		#region Functions

		/// <summary>
		/// Get a matrix 2x2 from rotation in radians
		/// </summary>
		/// <param name="rotation">Rotation in radians</param>
		/// <returns>Rotation matrix</returns>
		public static Matrix2x2 FromRotation(float rotation)
		{
			return new Matrix2x2(
				Mathf.Cos(rotation),
				-Mathf.Sin(rotation),
				Mathf.Sin(rotation),
				Mathf.Cos(rotation)
				);
		}

		/// <summary>
		/// Matrix2x2 + Matrix2x2
		/// </summary>
		/// <param name="m1">Matrix 1</param>
		/// <param name="m2">Matrix 2</param>
		/// <returns>Matrix2x2 + Matrix2x2</returns>
		public static Matrix2x2 operator+(Matrix2x2 m1, Matrix2x2 m2)
		{
			return new(
				m1.m00 + m2.m00,
				m1.m01 + m2.m01,
				m1.m10 + m2.m10,
				m1.m11 + m2.m11
				);
		}

		/// <summary>
		/// Matrix2x2 - Matrix2x2
		/// </summary>
		/// <param name="m1">Matrix 1</param>
		/// <param name="m2">Matrix 2</param>
		/// <returns>Matrix2x2 - Matrix2x2</returns>
		public static Matrix2x2 operator-(Matrix2x2 m1, Matrix2x2 m2)
		{
			return new(
				m1.m00 - m2.m00,
				m1.m01 - m2.m01,
				m1.m10 - m2.m10,
				m1.m11 - m2.m11
				);
		}

		/// <summary>
		/// Matrix2x2 * float
		/// </summary>
		/// <param name="mat">Matrix</param>
		/// <param name="v">float</param>
		/// <returns>Matrix2x2 * float</returns>
		public static Matrix2x2 operator*(Matrix2x2 mat, float v)
		{
			return new(
				mat.m00 * v,
				mat.m01 * v,
				mat.m10 * v,
				mat.m11 * v
				);
		}

		/// <summary>
		/// float * Matrix2x2
		/// </summary>
		/// <param name="v">float</param>
		/// <param name="mat">Matrix</param>
		/// <returns>float * Matrix2x2</returns>
		public static Matrix2x2 operator*(float v, Matrix2x2 mat)
		{
			return new(
				mat.m00 * v,
				mat.m01 * v,
				mat.m10 * v,
				mat.m11 * v
				);
		}

		/// <summary>
		/// Matrix2x2 x Vector2
		/// </summary>
		/// <param name="mat">Matrix</param>
		/// <param name="vec">Vector</param>
		/// <returns>Matrix2x2 x Vector2</returns>
		public static Vector2 operator*(Matrix2x2 mat, Vector2 vec)
		{
			return new(
				vec.x * mat.m00 + vec.y * mat.m01,
				vec.x * mat.m10 + vec.y * mat.m11
				);
		}

		/// <summary>
		/// Matrix2x2 x Matrix2x2
		/// </summary>
		/// <param name="mat1">Matrix 1</param>
		/// <param name="mat2">Matrix 2</param>
		/// <returns>Matrix2x2 x Matrix2x2</returns>
		public static Matrix2x2 operator*(Matrix2x2 mat1, Matrix2x2 mat2)
		{
			return new(
				mat1.m00 * mat2.m00 + mat1.m01 * mat2.m10,
				mat1.m00 * mat2.m01 + mat1.m01 * mat2.m11,
				mat1.m10 * mat2.m00 + mat1.m11 * mat2.m10,
				mat1.m10 * mat2.m01 + mat1.m11 * mat2.m11
				);
		}

		/// <summary>
		/// Matrix2x2 / float
		/// </summary>
		/// <param name="mat">Matrix</param>
		/// <param name="v">float</param>
		/// <returns>Matrix2x2 / float</returns>
		public static Matrix2x2 operator/(Matrix2x2 mat, float v)
		{
			return new(
				mat.m00 / v,
				mat.m01 / v,
				mat.m10 / v,
				mat.m11 / v
				);
		}

		#endregion
	}
}
