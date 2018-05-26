using System;

namespace TestProject.Proof
{
	public class ProofGenerator : IProofGenerator
	{
		private readonly Random _random;

		public ProofGenerator()
		{
			_random = new Random();
		}

		public string GetRandomString()
		{
			return _random.Next().ToString().PadLeft(Int32.MaxValue.ToString().Length, '0');
		}
	}
}