// using System.Runtime.InteropServices;
// using OpenTK.Graphics.OpenGL4;
// // TODO impl
// namespace USharpLibs.Engine2.Client.Models.VertexNew {
// 	[PublicAPI]
// 	[StructLayout(LayoutKind.Explicit, Size = Size)]
// 	public readonly record struct VertexXyzI : IVertexNew {
// 		private const byte Size = 3 * sizeof(float) + 1 * sizeof(uint);
//
// 		public static VertexLayout[] VertexLayout { get; } = { new(VertexAttribPointerType.Float, 3), new(VertexAttribPointerType.UnsignedInt, 1), };
// 		public static byte StructSizeInBytes => Size;
//
// 		[field: FieldOffset(0)] public byte[] Elements { get; } = new byte[Size];
// 		[field: FieldOffset(0)] public float X { get; init; }
// 		[field: FieldOffset(4)] public float Y { get; init; }
// 		[field: FieldOffset(8)] public float Z { get; init; }
// 		[field: FieldOffset(12)] public uint Rgba { get; init; }
//
// 		public VertexXyzI(float x, float y, float z, uint rgba) {
// 			X = x;
// 			Y = y;
// 			Z = z;
// 			Rgba = rgba;
// 		}
// 	}
// }