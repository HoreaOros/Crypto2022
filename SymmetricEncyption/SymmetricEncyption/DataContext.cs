using System;
using System.Security.Cryptography;
public class Context
{
	public string[] CipherMode => Enum.GetNames(typeof(CipherMode)); 
	public string[] PaddingMode => Enum.GetNames(typeof(PaddingMode));
}
