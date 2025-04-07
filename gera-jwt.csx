using System;
using System.Security.Cryptography;

var key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
Console.WriteLine("Chave JWT segura:");
Console.WriteLine(key);
