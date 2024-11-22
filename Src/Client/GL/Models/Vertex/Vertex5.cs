using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace USharpLibs.Engine2.Client.GL.Models.Vertex {
	[PublicAPI]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public readonly record struct Vertex5 : IVertex {
		public static byte[] Arrangement { get; } = new byte[] { 3, 2, };
		public static byte Length => 5;

		public float X { get; } // 0-4
		public float Y { get; } // 4-8
		public float Z { get; } // 8-12
		public float U { get; } // 12-16
		public float V { get; } // 16-20

		public Vertex5(float x, float y, float z, float u, float v) {
			X = x;
			Y = y;
			Z = z;
			U = u;
			V = v;
		}

		public static implicit operator Vertex5((float x, float y, float z, float u, float v) vert) => new(vert.x, vert.y, vert.z, vert.u, vert.v);

		public void Deconstruct(out float x, out float y, out float z, out float u, out float v) {
			x = X;
			y = Y;
			z = Z;
			u = U;
			v = V;
		}

		public float this[byte i] =>
				i switch {
						0 => X,
						1 => Y,
						2 => Z,
						3 => U,
						4 => V,
						_ => throw new ArgumentOutOfRangeException(nameof(i), i, null),
				};

		public override string ToString() => this.GenerateString();
	}
}