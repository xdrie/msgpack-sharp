﻿using NUnit.Framework;
using System;
using scopely.msgpacksharp.Extensions;

namespace scopely.msgpacksharp.tests
{
	[TestFixture]
	public class SerializationTests
	{
		[Test]
		public void TestRoundTrip()
		{
			AnimalMessage msg = new AnimalMessage();
			msg.HeightInches = 7;
			msg.AnimalKind = "Cat";
			msg.AnimalName = "Lunchbox";
			msg.AnimalColor = new AnimalColor() { Red = 1.0f, Green = 0.1f, Blue = 0.1f };
			msg.BirthDay = new DateTime(1974, 1, 4);
			msg.SomeNumbers = new int[5];
			for (int i = 0; i < msg.SomeNumbers.Length; i++)
				msg.SomeNumbers[i] = i * 2;
			msg.SpotColors = new AnimalColor[3];
			for (int i = 0; i < msg.SpotColors.Length; i++)
			{
				msg.SpotColors[i] = new AnimalColor() { Red = 1.0f, Green = 1.0f, Blue = 0.0f };
			}

			byte[] payload = msg.ToMsgPack();
			Assert.IsNotNull(payload);
			Assert.AreNotEqual(0, payload.Length);
			Console.Out.WriteLine("Payload is " + payload.Length + " bytes!");

			AnimalMessage restored = MsgPackSerializer.Deserialize<AnimalMessage>(payload);
			Assert.IsNotNull(restored);
			Assert.AreEqual(msg.HeightInches, restored.HeightInches);
			Assert.AreEqual(msg.AnimalKind, restored.AnimalKind);
			Assert.AreEqual(msg.AnimalName, restored.AnimalName);
			Assert.IsNotNull(msg.AnimalColor);
			Assert.AreEqual(msg.AnimalColor.Red, restored.AnimalColor.Red);
			Assert.AreEqual(msg.AnimalColor.Green, restored.AnimalColor.Green);
			Assert.AreEqual(msg.AnimalColor.Blue, restored.AnimalColor.Blue);
			Assert.AreEqual(msg.BirthDay, restored.BirthDay);
			Assert.AreEqual(msg.SomeNumbers.Length, restored.SomeNumbers.Length);
			for (int i = 0; i < msg.SomeNumbers.Length; i++)
			{
				Assert.AreEqual(msg.SomeNumbers[i], restored.SomeNumbers[i]);
			}
			Assert.AreEqual(msg.SpotColors.Length, restored.SpotColors.Length);
			for (int i = 0; i < msg.SpotColors.Length; i++)
			{
				Assert.AreEqual(msg.SpotColors[i], restored.SpotColors[i]);
			}
		}
	}
}

