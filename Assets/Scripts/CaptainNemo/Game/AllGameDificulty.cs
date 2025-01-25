using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainNemo.Game
{
	[CreateAssetMenu(fileName = "AllGameDificulty", menuName = "Game/AllGameDificulty")]
	public class AllGameDificulty : ScriptableObject
	{
		public List<GameDificulty> allGameDificulty = new List<GameDificulty>();
	}
}
