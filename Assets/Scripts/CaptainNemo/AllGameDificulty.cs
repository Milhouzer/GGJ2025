using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllGameDificulty", menuName = "Game/AllGameDificulty")]
public class AllGameDificulty : ScriptableObject
{
	public List<GameDificulty> allGameDificulty = new List<GameDificulty>();
}
