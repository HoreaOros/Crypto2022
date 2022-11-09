using System;
using System.Security.Cryptography;
namespace SymmetricEncyption
{
	public static class Context
	{
		public static string[] CipherMode => Enum.GetNames(typeof(CipherMode));
		public static string[] PaddingMode => Enum.GetNames(typeof(PaddingMode));
		public static string[] Algos => new string[] { "DES", "AES", "TripleDES", "Rijndael", "RC2" };
	}
}
