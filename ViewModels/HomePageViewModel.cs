using System;
using UIMock.Entities;

namespace UIMock
{
	public class HomePageViewModel : BaseViewModel
	{
		public string Name { get; set; }
		public HomePageViewModel()
		{
			Name = User.CurrentUser.Name;
		}
	}
}

