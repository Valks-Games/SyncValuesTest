global using Godot;
global using System;
global using System.Collections.Generic;
global using System.Collections.Concurrent;
global using System.Diagnostics;
global using System.Runtime.CompilerServices;
global using System.Threading;
global using System.Text.RegularExpressions;
global using System.Threading.Tasks;
global using System.Linq;

using Netcode;

namespace Test;

public partial class Main : Node
{
	public static Logger Logger { get; set; }

	public override void _Ready()
	{
		Logger = new Logger();
		NetCodeLib.Logger = Logger;
		NetCodeLib.Init();
	}

	public override void _PhysicsProcess(double delta)
	{
		Logger.Update();
	}
}
