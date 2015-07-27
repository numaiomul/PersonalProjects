/*
using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
[Category("Simple Tests")]
internal class BasicTests
{
	[Test]
	public void StunTest([NUnit.Framework.Range(0,7)] int x) {
		Unit test = new Unit();
		test.SetCanFire(((x & 4) == 4)?true:false);
		test.SetCanMove(((x & 2) == 2)?true:false);
		test.SetIsStun(( (x & 1) == 1)?true:false);

		if (test.IsStun == ((!test.canMove && !test.canFire) || test.isStun)){
			Assert.Pass("Conditions:" +
			            "\nCanFire:" + (((x & 4) == 4)?(true):(false)).ToString() +
			            "\nCanMove:" + (((x & 2) == 2)?(true):(false)).ToString() +
			    		"\nIsStun: " + (((x & 1) == 1)?(true):(false)).ToString() +
			            "\n---------------------" +
			            "\nResult: " + test.IsStun.ToString());
		}
		else {
			Assert.Fail();
		}
	}
}
*/