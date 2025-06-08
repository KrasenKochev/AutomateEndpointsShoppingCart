using System.Text.Json;

namespace TestProject1;

using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;


public abstract class BaseTest
{
	protected readonly HttpClient _client;

	protected BaseTest()
	{
		_client = new HttpClient
		{
			BaseAddress = new Uri("http://localhost:4080")

		};
	}
}