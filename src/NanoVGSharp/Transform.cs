using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;

namespace NanoVGSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Transform
	{
		public float t1, t2, t3, t4, t5, t6;

		public void Zero()
		{
			t1 = t2 = t3 = t4 = t5 = t6 = 0;
		}

		public void Set(Transform src)
		{
			t1 = src.t1;
			t2 = src.t2;
			t3 = src.t3;
			t4 = src.t4;
			t5 = src.t5;
			t6 = src.t6;
		}

		public void SetIdentity()
		{
			t1 = (float)(1.0f);
			t2 = (float)(0.0f);
			t3 = (float)(0.0f);
			t4 = (float)(1.0f);
			t5 = (float)(0.0f);
			t6 = (float)(0.0f);
		}

		public void SetTranslate(float tx, float ty)
		{
			t1 = (float)(1.0f);
			t2 = (float)(0.0f);
			t3 = (float)(0.0f);
			t4 = (float)(1.0f);
			t5 = (float)(tx);
			t6 = (float)(ty);
		}

		public void SetScale(float sx, float sy)
		{
			t1 = (float)(sx);
			t2 = (float)(0.0f);
			t3 = (float)(0.0f);
			t4 = (float)(sy);
			t5 = (float)(0.0f);
			t6 = (float)(0.0f);
		}

		public void SetRotate(float a)
		{
			float cs = (float)(NanoVGContext.cosf((float)(a)));
			float sn = (float)(NanoVGContext.sinf((float)(a)));
			t1 = (float)(cs);
			t2 = (float)(sn);
			t3 = (float)(-sn);
			t4 = (float)(cs);
			t5 = (float)(0.0f);
			t6 = (float)(0.0f);
		}

		public void SetSkewX(float a)
		{
			t1 = (float)(1.0f);
			t2 = (float)(0.0f);
			t3 = (float)(NanoVGContext.tanf((float)(a)));
			t4 = (float)(1.0f);
			t5 = (float)(0.0f);
			t6 = (float)(0.0f);
		}

		public void SetSkewY(float a)
		{
			t1 = (float)(1.0f);
			t2 = (float)(NanoVGContext.tanf((float)(a)));
			t3 = (float)(0.0f);
			t4 = (float)(1.0f);
			t5 = (float)(0.0f);
			t6 = (float)(0.0f);
		}

		public void Multiply(ref Transform s)
		{
			float _t0 = (float)(t1 * s.t1 + t2 * s.t3);
			float _t2 = (float)(t3 * s.t1 + t4 * s.t3);
			float _t4 = (float)(t5 * s.t1 + t6 * s.t3 + s.t5);
			t2 = (float)(t1 * s.t2 + t2 * s.t4);
			t4 = (float)(t3 * s.t2 + t4 * s.t4);
			t6 = (float)(t5 * s.t2 + t6 * s.t4 + s.t6);
			t1 = (float)(_t0);
			t3 = (float)(_t2);
			t5 = (float)(_t4);
		}

		public void Premultiply(ref Transform s)
		{
			Transform s2 = s;
			s2.Multiply(ref this);
			Set(s2);
		}

		public Transform BuildInverse()
		{
			double invdet = 0;
			double det = (double)((double)(t1) * t4 - (double)(t3) * t2);
			Transform inv = new Transform();
			if (((det) > (-1e-6)) && ((det) < (1e-6)))
			{
				inv.SetIdentity();
				return inv;
			}

			invdet = (double)(1.0 / det);
			inv.t1 = ((float)(t4 * invdet));
			inv.t3 = ((float)(-t3 * invdet));
			inv.t5 = ((float)(((double)(t3) * t6 - (double)(t4) * t5) * invdet));
			inv.t2 = ((float)(-t2 * invdet));
			inv.t4 = ((float)(t1 * invdet));
			inv.t6 = ((float)(((double)(t2) * t5 - (double)(t1) * t6) * invdet));

			return inv;
		}

		public void TransformPoint(out float dx, out float dy, float sx, float sy)
		{
			dx = (float)(sx * t1 + sy * t3 + t5);
			dy = (float)(sx * t2 + sy * t4 + t6);
		}

		public void TransformVector(out Vector2 v, Vector2 s)
		{
			TransformPoint(out v.X, out v.Y, s.X, s.Y);
		}
	}
}