using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQMTerry3D
{
	internal class Memory
	{
		private readonly uint mAddress;

		public ObservableCollection<Monster> Monsters { get; private set; } = new ObservableCollection<Monster>();

		public Memory(uint address)
		{
			mAddress = address;
			Monsters.Add(new Monster(address + 70));
		}
	}
}
